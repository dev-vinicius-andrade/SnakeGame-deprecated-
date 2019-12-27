using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGame.Infrastructure.Enums;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Domain.Player.Abstractions
{
    public abstract class BaseChar:IChar
    {
        public  IReadOnlyDictionary<DirectionsEnum, IDirection> KnownDirections { get; }
        private readonly int _initialSize;

        protected BaseChar(IReadOnlyDictionary<DirectionsEnum, IDirection> knownDirections, int initialSize=0)
        {
            Id = Guid.NewGuid();
            _initialSize = initialSize;
            Path = new List<IPosition>();
            KnownDirections = knownDirections;
        }

        public Guid Id { get; }
        public IDirection Direction { get; set; }
        public int Size { get; set; }
        public IList<IPosition> Path { get; set; }
        public int Speed { get; set; }
        public IColor Color { get; set; }
        public IPosition Position => Path.Last();
        public long Length => Path.Count-_initialSize;
    }
}
