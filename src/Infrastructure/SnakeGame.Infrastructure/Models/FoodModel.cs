
using System;

namespace SnakeGame.Infrastructure.Models
{
    public class FoodModel
    {
        public PositionModel Position{ get; set; }
        public  int Size { get; set; }
        public Guid Guid { get; set; }
    }
}
