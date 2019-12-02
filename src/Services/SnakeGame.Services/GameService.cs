using System.Threading.Tasks;
using SnakeGame.Domain.Food;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services.Entities;

namespace SnakeGame.Services
{

    public class GameService
    {
        private readonly GameData _gameData;
        public GameService(GameData gameData)
        {
            _gameData = gameData;
        }
        

    }
}
