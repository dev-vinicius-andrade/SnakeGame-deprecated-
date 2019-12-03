
using System;
using SnakeGame.Infrastructure.Helpers;

namespace SnakeGame.Infrastructure.Models
{
    public class FoodModel
    {
        public PositionModel Position{ get; set; }
        public  int Size { get; set; }
        public Guid Guid { get; set; }
        public string Color { get; set; }

        public FoodModel ChangeColor(string color)
        {
            if (!color.IsNullOrEmpty())
                Color = color;
            return this;
        }
    }
}
