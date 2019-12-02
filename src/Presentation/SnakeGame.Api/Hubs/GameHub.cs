using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SnakeGame.Domain.Food;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services;

namespace SnakeGame.Api.hubs
{
    public class GameHub:Hub
    {
        private readonly GameService _gameService;


        public GameHub(GameService gameService)
        {
            _gameService = gameService;
            
        }




        
        
        
    }
}
