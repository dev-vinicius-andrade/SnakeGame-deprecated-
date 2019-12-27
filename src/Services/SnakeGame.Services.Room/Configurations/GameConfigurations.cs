using SnakeGame.Domain.Food.Configurations;
using SnakeGame.Domain.Player.Configurations;
using SnakeGame.Domain.Room.Configurations;

namespace SnakeGame.Services.Room.Configurations
{
    public class GameConfigurations
    {
        public int GameFrameRateMilliSeconds { get; set; }
        public RoomConfigurationModel RoomConfiguration { get; set; }
        public FoodConfigurationModel FoodConfiguration {get;set;}
        public SnakeConfigurationModel SnakeConfiguration{get;set;}
        
        


    }

}
