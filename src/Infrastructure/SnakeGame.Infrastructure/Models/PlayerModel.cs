using System;

namespace SnakeGame.Infrastructure.Models
{
    public class PlayerModel
    {
        public  Guid RoomId { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public SnakeModel Snake { get; set; }
    }
}
