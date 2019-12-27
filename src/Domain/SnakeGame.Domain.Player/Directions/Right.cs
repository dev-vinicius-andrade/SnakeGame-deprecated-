using SnakeGame.Infrastructure.Enums;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Domain.Player.Directions
{
    public class Right: IDirection
    {
        public Right()
        {
            Direction = DirectionsEnum.Right;
            XSpeed = 1;
            YSpeed = 0;
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
