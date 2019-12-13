using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Infrastructure.Data.Models
{
    public class SnakeModel
    {
        public SnakeModel()
        {
            Path=new List<PositionModel>();
        }

        public PositionModel CurrentlyPosition => Path.Last();
        public PositionModel Direction { get; set; }
        public long Size { get; set; }
        public int HeadSize { get; set; }
        public IList<PositionModel> Path { get; set; }
        public string Color { get; set; }
        public int Speed { get; set; }
        public  string BorderColor { get; set; }
    }
}
