using System;
using System.ComponentModel.DataAnnotations;

namespace SnakeGame.Infrastructure.Data.Models
{
    public class PlayerModel
    {
        public PlayerModel(bool alive = true)
        {
            Alive = alive;
        }
        [Key]
        public Guid PlayerGuid { get; set; }
        public  Guid RoomId { get; set; }
        public string Name { get; set; }
        
        public string ConnectionId { get; set; }
        public  bool Alive { get; set; }
        public ScoreModel Score { get; set; }
        public SnakeModel Snake { get; set; }
    }
}
