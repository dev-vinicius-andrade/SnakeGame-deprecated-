using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Infrastructure.Models
{
    
    public class PositionModel:IPosition
    {
        public PositionModel()
        {
        }
        public ICoordinate Coordinate { get; set; }
        public IColor Color { get; set; }
    }
}
