using System;
using SnakeGame.Infrastructure.Abstractions;

namespace SnakeGame.Infrastructure.Models
{
    public class FoodModel:BaseFood
    {
        public FoodModel(Guid id, PositionModel position) : base( id,position)
        {
        }
    }
}
