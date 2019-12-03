using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services;

namespace SnakeGame.Api.Hubs
{
    public class FoodHub:Hub
    {
        
        private readonly FoodService _foodService;
        private readonly RoomService _roomService;
        public FoodHub(FoodService foodService,RoomService roomService)
        {
            _foodService = foodService;
            _roomService = roomService;
        }
        public void GenerateFood(string roomGuid)
        {
            try
            {
                var food = _foodService.GenerateFood(_roomService.Get(roomGuid.ToGuid()));
                Clients.All.SendCoreAsync("FruitGenerated", new object[] { food });
            }
            catch (Exception ex)
            {

                
            }
        }

        public List<FoodModel> GetAll(Guid roomGuid)
        {
            try
            {
                return _roomService.Get(roomGuid).Foods;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}
