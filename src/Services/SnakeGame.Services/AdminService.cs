using SnakeGame.Domain.Admin;
using SnakeGame.Infrastructure.Data.Models;

namespace SnakeGame.Services
{
    public class AdminService
    {
        private readonly UserManagement _userManagement;

        public AdminService(UserManagement userManagement)
        {
            _userManagement = userManagement;
        }
        public ResponseModel Login(string username, string password)
        {
            return _userManagement.Login(username, password);
        }

        public ResponseModel Register(string username, string password)
        {
            return _userManagement.RegisterUser(username, password);
        }
    }
}
