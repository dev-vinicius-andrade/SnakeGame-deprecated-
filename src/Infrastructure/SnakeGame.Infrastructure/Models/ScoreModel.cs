namespace SnakeGame.Infrastructure.Models
{
    public class ScoreModel
    {
        public ScoreModel(long points = 0)
        {
            Points = 0;
        }


        public string PlayerName { get; set; }
        public string CharColor { get; set; }
        public long Points { get; set; }
    }
}
