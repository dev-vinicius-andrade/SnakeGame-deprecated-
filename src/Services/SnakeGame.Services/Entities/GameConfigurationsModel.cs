using SnakeGame.Infrastructure.Configurations;

namespace SnakeGame.Services.Entities
{
    public class GameConfigurationsModel
    {
        public RoomConfiguirationModel Room { get; set; }

        public class RoomConfiguirationModel
        {
            public int Width { get; internal set; }
            public int Height { get; set; }
            public object BackgroundColor { get; set; }
            public int FrameRateInterval { get; set; }
            public GameConfigurations.RoomConfigurationModel.InfosConfigurationModel Infos { get; set; }
        }
    }
}
