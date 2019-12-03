using System.Collections.Generic;
using SnakeGame.Infrastructure.Helpers;

namespace SnakeGame.Infrastructure.Models
{
    public class SnakeModel
    {
        public SnakeModel()
        {
            Path=new List<PositionModel>();
        }
        public PositionModel CurrentlyPosition { get; set; }
        public PositionModel Direction { get; set; }
        public long Size { get; set; }
        public List<PositionModel> Path { get; set; }
        public string Color { get; set; }
        public int Speed { get; set; }

        public SnakeModel ChangeColor(string color)
        {
            if(!color.IsNullOrEmpty())
                Color = color;
            return this;
        }
    }
}
