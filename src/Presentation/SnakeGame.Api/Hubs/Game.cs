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
    public class Game:Hub
    {
        private readonly GameService _gameService;

        private readonly FoodService _foodService;
        private readonly RoomService _roomService;
        private readonly PlayerService _playerService;

        public Game(GameService gameService,FoodService foodService,RoomService roomService,PlayerService playerService)
        {
            _gameService = gameService;
            _foodService = foodService;
            _roomService = roomService;
            _playerService = playerService;
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendCoreAsync("Configurations", new object[]{_gameService.GetConfigurations()});
            return base.OnConnectedAsync();
        }


        public  void GameStatus(Guid roomId, string playerId)
        {
            _gameService.Configure(Clients, roomId, playerId);
            _gameService.GameStatus();

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



   

    }
}
