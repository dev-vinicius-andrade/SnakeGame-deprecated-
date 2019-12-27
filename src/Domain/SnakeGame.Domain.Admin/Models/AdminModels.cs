using System;
using System.Collections.Generic;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Domain.Admin.Models
{
    public  class AdminModels
    {
        public class PasswordEncrypt
        {
            public Guid GuidKey { get; set; }
        }
        public class AvailableUsers
        {
            public List<UserModel> Users { get; set; }
        }
        public class ConnectedUser
        {
            public UserModel User { get; set; }
        }
    }
}
