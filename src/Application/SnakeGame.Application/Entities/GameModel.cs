using System;
using System.Collections.Generic;
using SnakeGame.Infrastructure.Interfaces;
namespace SnakeGame.Application.Entities
{
    public class GameModel
    {
        public GameModel(IRoom room, IReadOnlyList<IScore> score)
        {
            Score = score;
            RoomId = room.Id;
            Foods = room.Foods as IReadOnlyList<IFood>;
            Players = room.Players as IReadOnlyList<IPlayer>;
        }

        public Guid RoomId { get; }
        public IReadOnlyList<IScore> Score { get;  }
        public IReadOnlyList<IPlayer> Players { get; }
        public IReadOnlyList<IFood> Foods { get; }
    }
}
