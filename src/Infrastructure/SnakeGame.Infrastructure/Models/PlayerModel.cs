using System;

namespace SnakeGame.Infrastructure.Models
{
    public class PlayerModel
    {
        public PlayerModel(bool alive = true)
        {
            Alive = alive;
        }
        public  Guid RoomId { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public  bool Alive { get; set; }
        public SnakeModel Snake { get; set; }
    }
}
