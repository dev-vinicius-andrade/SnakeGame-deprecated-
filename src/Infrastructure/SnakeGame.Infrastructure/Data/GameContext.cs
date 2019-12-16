using System;
using System.Collections.Concurrent;
using SnakeGame.Infrastructure.Data.Models;

namespace SnakeGame.Infrastructure.Data
{
    public class GameContext
    {
        public GameContext()
        {
            Rooms = new ConcurrentDictionary<Guid, RoomModel>();
        }
        public  ConcurrentDictionary<Guid, RoomModel> Rooms { get; }
    }
}
