using Microsoft.AspNetCore.SignalR;
using SnakeGame.Infrastructure.Models.Configurations;

namespace SnakeGame.Api
{
    public class AppSettings
    {
        
        public HubOptions HubOptions { get; set; }
        public  string AllowedHosts { get; set; }
        public GameConfigurationsModel GameConfigurations { get; set; }

        

    }
}
