namespace SnakeGame.Infrastructure.Interfaces
{
    public interface IPosition
    {
        ICoordinate Coordinate { get; set; }
        IColor Color { get; set; }
    }
}