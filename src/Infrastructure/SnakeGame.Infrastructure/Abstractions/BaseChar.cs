using System.Collections.Generic;
using System.Linq;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Infrastructure.Abstractions
{
    public abstract class BaseChar:IPositionObject
    {

        protected BaseChar()
        {
            Path=new List<PositionModel>();
        }
        public PositionModel Direction { get; set; }
        public int Size { get; set; }
        public IList<PositionModel> Path { get; set; }
        public int Speed { get; set; }
        public  ColorModel Color { get; set; }
        public virtual PositionModel Position => Path.Last();
        public virtual long Length => Path.Count();
    }
}
