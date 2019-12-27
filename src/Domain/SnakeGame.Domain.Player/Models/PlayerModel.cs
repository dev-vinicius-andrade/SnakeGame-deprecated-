using System;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Domain.Player.Models
{
    public class PlayerModel:IPlayer

    {
        public PlayerModel(bool alive = true)
        {
            Alive = alive;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; }
        public  Guid RoomId { get; set; }
        public string Name { get; set; }
        
        public string ConnectionId { get; set; }
        public  bool Alive { get; set; }
        public IScore Score { get; set; }
        public IPosition Position => Char.Position;
        public IChar Char { get; set; }
       
    }
}
