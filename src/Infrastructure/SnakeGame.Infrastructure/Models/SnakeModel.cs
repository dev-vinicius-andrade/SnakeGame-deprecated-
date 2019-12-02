using System.Collections.Generic;

namespace SnakeGame.Infrastructure.Models
{
    public class SnakeModel
    {
        public SnakeModel()
        {
            Path=new List<PositionModel>();
        }
        public PositionModel CurrentlyPosition { get; set; }
        public long Size { get; set; }
        public List<PositionModel> Path { get; set; }
    }
}
