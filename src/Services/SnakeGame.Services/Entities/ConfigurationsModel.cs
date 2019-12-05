using SnakeGame.Infrastructure.Models.Configurations;

namespace SnakeGame.Services.Entities
{
    public class ConfigurationsModel
    {
        public RoomConfiguirationModel Room { get; set; }

        public class RoomConfiguirationModel
        {
            public int Width { get; internal set; }
            public int Height { get; set; }
            public object BackgroundColor { get; set; }
            public int FrameRateInterval { get; set; }
            public GameConfigurationsModel.RoomConfigurationModel.InfosConfigurationModel Infos { get; set; }
        }
    }
}
