
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Infrastructure.Models
{
    public class CoordinateModel:ICoordinate
    {
        public int? X { get; set; }
        public int? Y { get; set; }
        public int? Z { get; set; }
    }
}
