using System.Collections.Generic;
using SnakeGame.Domain.Player.Abstractions;
using SnakeGame.Infrastructure.Enums;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Domain.Player.Models
{
    public class SnakeModel:BaseChar
    {
        public SnakeModel(IReadOnlyDictionary<DirectionsEnum, IDirection> knownDirections, int initialSize = 0) : base(knownDirections, initialSize)
        {
        }
    }
}
