namespace SnakeGame.Infrastructure.Models
{
    public class GameConfigurationsModel
    {
        public int MaxFoods { get; set; }
        public  int MaxPlayers { get; set; }
        public int Width { get; set; }
        public int Height{ get; set; }
        public  int FoodGenerationInterval { get; set; }
        public  int FoodSize { get; set; }
    }
}
