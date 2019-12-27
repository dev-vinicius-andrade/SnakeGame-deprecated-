using System.Collections.Generic;
using SnakeGame.Infrastructure.Enums;

namespace SnakeGame.Infrastructure.Interfaces
{
    public interface IChar :ICurrentPosition,  ITrackable
    {
        IReadOnlyDictionary<DirectionsEnum, IDirection> KnownDirections { get; }
        IDirection Direction { get; set; }
        int Size { get; set; }
        IList<IPosition> Path { get; set; }
        int Speed { get; set; }
        IColor Color { get; set; }
        long Length { get;}
    }
}