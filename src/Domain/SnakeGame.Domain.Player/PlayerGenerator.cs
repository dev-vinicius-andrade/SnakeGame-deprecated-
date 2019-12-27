using System;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;
using ICharHandler = SnakeGame.Domain.Player.Interfaces.ICharHandler;
using PlayerModel = SnakeGame.Infrastructure.Models.PlayerModel;
using ScoreModel = SnakeGame.Infrastructure.Models.ScoreModel;

namespace SnakeGame.Domain.Player
{
    public class PlayerGenerator
    {


        public PlayerGenerator()
        {

        }
        public PlayerModel<TChar> New<TChar>(string connectionId, string name, Guid roomGuid, TChar playerChar)
        where  TChar:IChar,ICharHandler
        {

            var playerModel = new PlayerModel<TChar>
            {
                RoomId = roomGuid,
                ConnectionId = connectionId,
                Name = name,
                Score = new ScoreModel(),
                Char = playerChar
            };
            return playerModel;
        }
    }
}
