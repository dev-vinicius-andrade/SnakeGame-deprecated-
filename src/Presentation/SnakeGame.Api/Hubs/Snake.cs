using System;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Api.Hubs
{
    public partial class Game
    {
        public void DirectionChanged(Guid roomGuid, string playerId, PositionModel newDirection)
        {
            try
            {
                var room = _roomService.Get(roomGuid);
                var player = _playerService.Get(room, playerId);

                if (!_snakeService.ChangeDirection(player.Snake, newDirection)) return;
            }
            catch (Exception)
            {

                
            }

            //_gameService.Configure(Clients,roomGuid,playerId);
            //_gameService.GameStatus();
        }
    }
}
