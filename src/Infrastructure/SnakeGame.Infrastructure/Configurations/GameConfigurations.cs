namespace SnakeGame.Infrastructure.Configurations
{
    public class GameConfigurations
    {
        public int GameFrameRateMilliSeconds { get; set; }
        public RoomConfigurationModel RoomConfiguration { get; set; }
        public FoodConfigurationModel FoodConfiguration {get;set;}
        public SnakeConfigurationModel SnakeConfiguration{get;set;}
        
        public class RoomConfigurationModel
        {
            public int MaxFoods { get; set; }
            public int MaxPlayers { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public string BackgroundColor { get; set; }
            public int PlayersInScore { get; set; }
            public InfosConfigurationModel Infos { get; set; }

            public class InfosConfigurationModel
            {
                public string Width { get; set; }
                public string Height { get; set; }
                public string BackgroundColor { get; set; }
                public double Opacity { get; set; }
            }
        }
        public class SnakeConfigurationModel
        {
            public int RotationSpeed{get;set;}
            public int Speed{get;set;}
            public int HeadSize{get;set;}
            public int InitialSnakeSize { get; set; }
        }
        public class FoodConfigurationModel
        {
            public int FoodGenerationInterval { get; set; }
            public int FoodSize { get; set; }
            
        }
    }

}
