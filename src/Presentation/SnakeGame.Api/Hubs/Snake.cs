using System;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Api.Hubs
{
    public partial class Game
    {
        public void DirectionChanged(Guid roomGuid, string playerId, PositionModel newDirection)
        {
            var room = _roomService.Get(roomGuid);
            var player = _playerService.Get(room, playerId);
            
            if (!_snakeService.DirectionChanged(player.Snake, newDirection)) return;

            _gameService.Configure(Clients,roomGuid,playerId);
            _gameService.GameStatus();
        }
    }
}
