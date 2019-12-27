using System;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Infrastructure.Models
{
    public class PlayerModel

    {
        public PlayerModel(bool alive = true)
        {
            Alive = alive;
        }
        public Guid PlayerGuid { get; set; }
        public  Guid RoomId { get; set; }
        public string Name { get; set; }
        
        public string ConnectionId { get; set; }
        public  bool Alive { get; set; }
        public ScoreModel Score { get; set; }
        public PositionModel Position => Char.Model.Position;
        public IChar Char { get; set; }
    }
}
