using System;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Application.Entities;
using SnakeGame.Application.Handlers;
using SnakeGame.Services.Room.Configurations;

namespace SnakeGame.Application.Services
{

    public class GameService : Hub
    {
        private readonly GameConfigurations _configurations;
        private readonly GameHandler _gameHandler;
        public GameService(
            GameConfigurations configurations,
            GameHandler gameHandler)
        {
            _configurations = configurations;
            _gameHandler = gameHandler;
        }
        

      


    }
}
