using Microsoft.Extensions.DependencyInjection;
using SnakeGame.Infrastructure.Data;
using SnakeGame.Infrastructure.Data.Interfaces;

namespace SnakeGame.Infrastructure.Helpers
{
    public static class StartupExtensions
    {

        public static IServiceCollection AddGameContext(this IServiceCollection services)
        {
            services.AddSingleton<IGameContext>(p=>new GameContext());
            return services;
        }
    }
}
