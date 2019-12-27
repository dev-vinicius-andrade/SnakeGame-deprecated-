using Microsoft.AspNetCore.SignalR;
using SnakeGame.Domain.Admin.Models;
using SnakeGame.Services.Room.Configurations;

namespace SnakeGame.Application.Configurations
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
