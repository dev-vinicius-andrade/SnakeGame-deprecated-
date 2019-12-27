using System;
using SnakeGame.Infrastructure.Abstractions;

namespace SnakeGame.Infrastructure.Interfaces
{
    public interface IChar
    {
        Guid Id { get; }
        BaseChar Model { get; }
    }
}