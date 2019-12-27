using SnakeGame.Infrastructure.Enums;

namespace SnakeGame.Infrastructure.Interfaces
{
    public interface IDirection
    {
        DirectionsEnum Direction { get; }
        int XSpeed { get;}
        int YSpeed { get; }
        int ZSpeed { get; }
        double Angle { get; set; }
    }
}