using System;
using System.ComponentModel.DataAnnotations;
using SnakeGame.Infrastructure.Helpers;

namespace SnakeGame.Infrastructure.Data.Models
{
    public class FoodModel
    {
        public Guid Guid { get; set; }
        public PositionModel Position{ get; set; }
        public  int Size { get; set; }
        
        public string Color { get; set; }
        public string BorderColor { get; set; }
    }
}
