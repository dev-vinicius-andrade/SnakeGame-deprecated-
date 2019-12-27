using System;

namespace SnakeGame.Infrastructure.Interfaces
{
    public interface IPlayer:ITrackable
    {
        string Name { get;}
        Guid RoomId { get;}
        string ConnectionId { get; set; }
        bool Alive { get; set; }
        IScore Score { get; set; }
        IPosition Position { get; }
        IChar Char { get; set; }
    }
}