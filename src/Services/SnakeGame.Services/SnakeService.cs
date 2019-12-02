using SnakeGame.Domain.Snake;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Services
{
    public class SnakeService
    {
        private readonly GameConfigurationsModel _configurations;

        public SnakeService(GameConfigurationsModel configurations)
        {
            _configurations = configurations;
        }
        public SnakeModel Create()
        {
            return new SnakeGenerator(_configurations).Generate();
        }
    }
}
