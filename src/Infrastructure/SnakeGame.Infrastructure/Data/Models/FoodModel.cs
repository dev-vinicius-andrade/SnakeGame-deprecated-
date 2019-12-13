using System;
using SnakeGame.Infrastructure.Helpers;

namespace SnakeGame.Infrastructure.Data.Models
{
    public class FoodModel
    {
        public PositionModel Position{ get; set; }
        public  int Size { get; set; }
        public Guid Guid { get; set; }
        public string Color { get; set; }
        public string BorderColor { get; set; }
    }
}
