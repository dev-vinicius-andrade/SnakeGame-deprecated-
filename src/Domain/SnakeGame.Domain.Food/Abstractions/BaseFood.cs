using System;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Domain.Food.Abstractions
{
    public abstract class BaseFood:IFood
    {
        protected BaseFood(Guid id, IPosition position)
        {
            Id = id;
            Position = position;
        }
        public Guid Id { get;}
        public IPosition Position { get; }
    }
}