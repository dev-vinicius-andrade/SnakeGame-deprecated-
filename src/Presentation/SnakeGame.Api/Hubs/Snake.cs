using System;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services;

namespace SnakeGame.Api.Hubs
{
    public class Snake:Hub
    {
        private readonly SnakeService _snakeService;
        private readonly PlayerService _playerService;
        private readonly GameService _gameService;
        private readonly RoomService _roomService;

        public Snake(RoomService roomService, SnakeService snakeService, PlayerService playerService, GameService gameService)
        {
            _roomService = roomService;
            _snakeService = snakeService;
            _playerService = playerService;
            _gameService = gameService;
        }
        public void DirectionChanged(Guid roomGuid, string playerId, PositionModel newDirection)
        {
            var room = _roomService.Get(roomGuid);
            var player = _playerService.Get(room, playerId);
            if (_snakeService.DirectionChanged(player.Snake, newDirection))
            {
                _gameService.Configure(Clients, roomGuid,playerId);
                _gameService.GameStatus();
            }
        }
    }
}
