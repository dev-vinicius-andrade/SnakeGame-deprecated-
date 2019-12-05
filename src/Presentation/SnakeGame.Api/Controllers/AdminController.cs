using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SnakeGame.Api.Configurations;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ConfigurationFilesEntities.AvailableUsers _availableUsers;
        private readonly ConfigurationFilesEntities.ConnectedUsers _connectedUsers;
        private readonly ConfigurationFilesEntities.PasswordEncrypt _passwordEncrypt;

        public AdminController(
            ConfigurationFilesEntities.AvailableUsers availableUsers,
            ConfigurationFilesEntities.ConnectedUsers connectedUsers,
            ConfigurationFilesEntities.PasswordEncrypt passwordEncrypt)
        {
            
            _availableUsers = availableUsers;
            _connectedUsers = connectedUsers;
            _passwordEncrypt = passwordEncrypt;
        }

        [HttpPost("login")]
        public JsonResult Login(string username, string password)
        {
            if(!_availableUsers.Users.Any())
                return new JsonResult(new {Message="Invalid User"}){StatusCode =(int)HttpStatusCode.Unauthorized};

            var existingUser = _availableUsers.Users.FirstOrDefault(p =>
                p.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));

            if(existingUser==null)
                return new JsonResult(new {Message="Invalid Username/Password"}){StatusCode =(int)HttpStatusCode.Unauthorized};


            if(!existingUser.Password.Equals(EncrypPassword(password,_passwordEncrypt.GuidKey)))
                return new JsonResult(new {Message="Invalid Username/Password"}){StatusCode =(int)HttpStatusCode.Unauthorized};
            return Json("");
        }


        public static string EncrypPassword(string password,Guid guidKey)
        {
            var passwordBytes = Encoding.ASCII.GetBytes($"{password}{guidKey.ToString()}");
            var base64Password = Convert.ToBase64String(passwordBytes, Base64FormattingOptions.None);
            var base64PasswordBytes = Encoding.ASCII.GetBytes(base64Password);
            var sha256Bytes = SHA256.Create().ComputeHash(base64PasswordBytes);
            var builder = new StringBuilder();  
            foreach (var t in sha256Bytes)
                builder.Append(t.ToString("x2"));
            return builder.ToString();  
        }
    }
}