using SnakeGame.Infrastructure.Enums;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Domain.Player.Directions
{
    public class Up: IDirection
    {
        public Up()
        {
            Direction = DirectionsEnum.Down;
            XSpeed = 0;
            YSpeed = -1;
            ZSpeed = 0;
            Angle = 0;
        }

        public DirectionsEnum Direction { get; }
        public int XSpeed { get; }
        public int YSpeed { get; }
        public int ZSpeed { get; }
        public double Angle { get; set; }
    }
}
