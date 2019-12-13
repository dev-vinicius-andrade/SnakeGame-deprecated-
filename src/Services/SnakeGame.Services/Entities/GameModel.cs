using System;
using System.Collections.Generic;
using SnakeGame.Infrastructure.Data.Models;

namespace SnakeGame.Services.Entities
{
    public class GameModel
    {
        public GameModel(RoomModel room, IReadOnlyList<ScoreModel> score)
        {
            Score = score;
            Foods = room.Foods;
            RoomId = room.RoomGuid;
            Players = room.Players;
        }

        public Guid RoomId { get; }
        public IReadOnlyList<ScoreModel> Score { get;  }
        public IReadOnlyList<PlayerModel> Players { get; }
        public IReadOnlyList<FoodModel> Foods { get; }
    }
}
