using System;
using System.Collections.Generic;
using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Application.Entities
{
    public class GameModel
    {
        public GameModel(RoomModel room, IReadOnlyList<ScoreModel> score)
        {
            Score = score;
            Foods = room.Foods;
            RoomId = room.Id;
            Players = room.Players;
        }

        public Guid RoomId { get; }
        public IReadOnlyList<ScoreModel> Score { get;  }
        public IReadOnlyList<PlayerModel> Players { get; }
        public IReadOnlyList<BaseFood> Foods { get; }
    }
}
