using System;
using System.Linq;
using SnakeGame.Domain.Admin.Helpers;
using SnakeGame.Domain.Admin.Models;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Domain.Admin
{
    public class UserManagement
    {
        private readonly AdminModels.AvailableUsers _availableUsers;
        private AdminModels.ConnectedUser _connectedUser;
        private readonly PasswordEncryptor _passwordEncryptor;
        public UserManagement(
            AdminModels.AvailableUsers availableUsers,
            AdminModels.PasswordEncrypt passwordEncrypt
            )
        {
            _availableUsers = availableUsers;
            _connectedUser = null;
            _passwordEncryptor = new PasswordEncryptor(passwordEncrypt);
        }
        public ResponseModel Login(string username, string password)
        {
            if (!HasAvailableUsers())
                return ResponseHelper.DefaultInvalidUsernamePassword;

            var existingUser = GetUser(username);

            if (existingUser == null)
                return ResponseHelper.DefaultInvalidUsernamePassword;

            var encryptedPassword = _passwordEncryptor.EncrypPassword(password);
            if (!CheckPassword(encryptedPassword, existingUser))
                return ResponseHelper.DefaultInvalidUsernamePassword;

            SetConnectedUser(username, encryptedPassword);
            return ResponseHelper.DefaultLoggedUser;
        }

        public ResponseModel RegisterUser(string username, string password)
        {

            if (UserExists(username))
                return ResponseHelper.DefaultRegisteredUser;

            AddAvailableUser(username,password);
            return ResponseHelper.DefaultUserAlreadyExists;
        }

        public void AddAvailableUser(string username,string password)=>_availableUsers.Users.Add(new UserModel
        {
            Username = username, 
            Password = _passwordEncryptor.EncrypPassword(password)
        });
        public void DisconnectUser()
        {
            _connectedUser = null;
        }

        public void SetConnectedUser(string username, string encryptedPassword)
        {
            _connectedUser = new AdminModels.ConnectedUser
            {
                User = new UserModel
                {
                    Username = username,
                    Password = encryptedPassword

                }
            };
        }

        public bool IsUserConnected() => _connectedUser != null;
        public bool CheckPassword(string encryptedPassword, UserModel existingUser) =>
            existingUser.Password.Equals(encryptedPassword);
        public bool HasAvailableUsers() => _availableUsers.Users.Any();
        public bool UserExists(string username) =>HasAvailableUsers()  && _availableUsers.Users.Any(p =>
            p.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        public UserModel GetUser(string username) => _availableUsers.Users.FirstOrDefault(p =>
              p.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));


    }
}
