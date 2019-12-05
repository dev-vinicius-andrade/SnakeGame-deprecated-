using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Infrastructure.Models.Configurations;

namespace SnakeGame.Api.Configurations
{
    public class ConfigurationFilesEntities
    {
        
        public HubOptions HubOptions { get; set; }
        public  string AllowedHosts { get; set; }
        public GameConfigurationsModel GameConfigurations { get; set; }
        public AvailableUsers AvailableUsersConfiguration { get; set; }
        public PasswordEncrypt PasswordEncryptConfiguration { get; set; }
        public class PasswordEncrypt
        {
            public Guid GuidKey { get; set; }
        }
        public class AvailableUsers
        {
            public List<UserModel> Users { get; set; }
        }
        public class ConnectedUsers
        {
            public List<UserModel> Users { get; set; }
        }
    }
}
