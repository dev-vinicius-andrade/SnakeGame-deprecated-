using Microsoft.AspNetCore.SignalR;
using SnakeGame.Services;

namespace SnakeGame.Api.Hubs
{
    public class Player:Hub
    {
        private readonly PlayerService _playerService;
        private readonly RoomService _roomService;

        public Player(RoomService roomService,PlayerService playerService)
        {
            _playerService = playerService;
            _roomService = roomService;
        }
    }
}
