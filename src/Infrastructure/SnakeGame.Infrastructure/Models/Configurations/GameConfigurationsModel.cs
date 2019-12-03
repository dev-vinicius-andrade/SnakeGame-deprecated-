namespace SnakeGame.Infrastructure.Models.Configurations
{
    public class GameConfigurationsModel
    {
        public int MaxFoods { get; set; }
        public int MaxPlayers { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public FoodConfigurationModel FoodConfiguration {get;set;}
        public SnakeConfigurationModel SnakeConfiguration{get;set;}
        public class SnakeConfigurationModel
        {
            public int RotationSpeed{get;set;}
            public int Speed{get;set;}
            public int HeadSize{get;set;}
        }
        public class FoodConfigurationModel
        {
            public int FoodGenerationInterval { get; set; }
            public int FoodSize { get; set; }
            
        }
    }

}
