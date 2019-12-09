using SnakeGame.Infrastructure.Helpers;

namespace SnakeGame.Infrastructure.Models
{
    public class PositionModel
    {
        public int? X { get; set; }
        public int? Y { get; set; }
        public string Color { get; set; }
        public string BorderColor { get; set; }
        public void ChangeColor(string color)
        {
            if(!color.IsNullOrEmpty())
                Color = color;
        }

    }
}
