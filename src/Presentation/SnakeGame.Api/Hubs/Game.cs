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
    public partial class Game:Hub
    {
        private readonly GameService _gameService;

        private readonly SnakeService _snakeService;
        private readonly RoomService _roomService;
        private readonly PlayerService _playerService;

        public Game(GameService gameService,SnakeService snakeService,RoomService roomService,PlayerService playerService)
        {
            _gameService = gameService;
            _snakeService = snakeService;
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
       
    }
}
