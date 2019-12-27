using System;
using SnakeGame.Domain.Food.Abstractions;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Domain.Food.Models
{
    public class FoodModel:BaseFood
    {
        public FoodModel(Guid id, IPosition position) : base( id,position)
        {
        }
    }
}
