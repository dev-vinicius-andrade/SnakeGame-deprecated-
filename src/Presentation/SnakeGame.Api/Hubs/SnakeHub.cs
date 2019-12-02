using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Services;

namespace SnakeGame.Api.Hubs
{
    public class SnakeHub:Hub
    {
        private readonly SnakeService _snakeService;

        public SnakeHub(SnakeService snakeService)
        {
            _snakeService = snakeService;
        }
    }
}
