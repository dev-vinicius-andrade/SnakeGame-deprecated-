using System;
using SnakeGame.Infrastructure.Data.Interfaces;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Infrastructure.Models
{
    public class GameDataModel:IGameData
    {
        public GameDataModel(IRoom room)
        {
            Id = room.Id;
            Room = room;
        }
        public Guid Id { get; }
        public IRoom Room { get; }
    }
}
