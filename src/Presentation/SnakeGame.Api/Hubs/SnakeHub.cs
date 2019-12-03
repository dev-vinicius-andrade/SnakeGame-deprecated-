using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services;

namespace SnakeGame.Api.Hubs
{
    public class SnakeHub:Hub
    {
        private readonly SnakeService _snakeService;
        private readonly PlayerService _playerService;
        private readonly RoomService _roomService;

        public SnakeHub(SnakeService snakeService,PlayerService playerService, RoomService roomService)
        {
            _snakeService = snakeService;
            _playerService = playerService;
            _roomService = roomService;
        }

        public void DirectionChanged(Guid roomGuid, string playerId, PositionModel newDirection)
        {
            var room = _roomService.Get(roomGuid);
            var player = _playerService.Get(room, playerId);
            player.Snake.Direction =  newDirection;
        }

        public void Moved(Guid roomGuid, string playerId, PositionModel position)
        {
            var room = _roomService.Get(roomGuid);
            var player = _playerService.Get(room, playerId);
            _snakeService.Move(player.Snake, position);
            Clients.Caller.SendCoreAsync("SnakeMoved", new object[] {player.Snake});
            Clients.Clients(_roomService.GetConnectedClientsIds(room))
                .SendCoreAsync("OnGameMonitoring", new object[]{room});
        }

    }
}
