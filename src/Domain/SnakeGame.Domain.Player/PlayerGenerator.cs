using System;
using SnakeGame.Domain.Player.Interfaces;
using SnakeGame.Domain.Player.Models;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;


namespace SnakeGame.Domain.Player
{
    public class PlayerGenerator
    {
        private readonly string _connectionId;
        private readonly string _name;
        private readonly Guid _roomGuid;


        public PlayerGenerator(string connectionId, string name, Guid roomGuid)
        {
            _connectionId = connectionId;
            _name = name;
            _roomGuid = roomGuid;
        }
        public IPlayer New(ICharHandler playerChar)
        {

            var playerModel = new PlayerModel
            {
                RoomId = _roomGuid,
                ConnectionId = _connectionId,
                Name = _name,
                Score = new ScoreModel(),
                Char = playerChar.Model
            };
            return playerModel;
        }
    }
}
