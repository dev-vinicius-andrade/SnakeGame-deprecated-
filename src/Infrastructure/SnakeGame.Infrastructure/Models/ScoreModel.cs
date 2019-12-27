using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Infrastructure.Models
{
    public class ScoreModel : IScore
    {
        public ScoreModel(long points = 0)
        {
            Points = 0;
        }


        public string PlayerName { get; set; }
        public IColor Color { get; set; }
        public long Points { get; set; }
    }
}
