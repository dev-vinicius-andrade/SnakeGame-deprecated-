using SnakeGame.Infrastructure.Models.Configurations;

namespace SnakeGame.Api
{
    public class AppSettings
    {
        
        public  string AllowedHosts { get; set; }
        public GameConfigurationsModel GameConfigurations { get; set; }
    }
}
