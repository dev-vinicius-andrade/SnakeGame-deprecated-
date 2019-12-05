using Microsoft.AspNetCore.SignalR;
using SnakeGame.Services;

namespace SnakeGame.Api.Hubs
{
    public class Food:Hub
    {
        private readonly FoodService _foodService;
        private readonly RoomService _roomService;
        public Food(RoomService roomService,FoodService foodService)
        {
            _foodService = foodService;
            _roomService = roomService;
        }
    }
}
