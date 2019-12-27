using Microsoft.AspNetCore.SignalR;
using SnakeGame.Application.Configurations;
using SnakeGame.Domain.Admin.Models;

namespace SnakeGame.Api.Configurations
{
    public class ConfigurationFilesEntities
    {
        
        public HubOptions HubOptions { get; set; }
        public  string AllowedHosts { get; set; }
        public GameConfigurations GameConfigurations { get; set; }
        public AdminModels.AvailableUsers AvailableUsersConfiguration { get; set; }
        public AdminModels.PasswordEncrypt PasswordEncryptConfiguration { get; set; }

    }
}
