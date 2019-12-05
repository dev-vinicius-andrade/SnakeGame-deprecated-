using Microsoft.AspNetCore.SignalR;
using SnakeGame.Services;

namespace SnakeGame.Api.Hubs
{
    public class Snake:Hub
    {
        private readonly SnakeService _snakeService;
        private readonly RoomService _roomService;

        public Snake(RoomService roomService, SnakeService snakeService)
        {
            _roomService = roomService;
            _snakeService = snakeService;
        }
    }
}
