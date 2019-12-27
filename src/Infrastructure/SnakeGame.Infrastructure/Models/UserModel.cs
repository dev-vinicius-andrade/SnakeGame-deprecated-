using System;

namespace SnakeGame.Infrastructure.Models
{
    public class UserModel
    {
        public Guid UserGuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
