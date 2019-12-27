using System;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Infrastructure.Abstractions
{
    public abstract class BaseFood:IPositionObject
    {
        protected BaseFood(Guid id, PositionModel position)
        {
            Id = id;
            Position = position;
        }
        public Guid Id { get;}
        public PositionModel Position { get;}
    }
}