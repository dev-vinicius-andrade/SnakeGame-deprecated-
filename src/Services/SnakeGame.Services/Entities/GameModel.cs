using System;
using System.Collections.Generic;
using System.Text;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Services.Entities
{
    public class GameModel
    {
        public GameModel(Room room)
        {
            Score = room.Score;
            Foods = room.Foods;
            RoomId = room.RoomGuid;
            Players = room.Players;
        }

        public Guid RoomId { get; }
        public List<ScoreModel> Score { get;  }
        public List<PlayerModel> Players { get; }
        public List<FoodModel> Foods { get; }
    }
}
