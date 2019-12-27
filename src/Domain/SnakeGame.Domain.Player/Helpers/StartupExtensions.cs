using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SnakeGame.Domain.Player.Directions;
using SnakeGame.Infrastructure.Enums;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Domain.Player.Helpers
{
    public static class StartupExtensions
    {
        public static IServiceCollection RegisterKnowDirections(this IServiceCollection services)
        {
            services.AddSingleton<IReadOnlyDictionary<DirectionsEnum, IDirection>>(new Dictionary<DirectionsEnum, IDirection>
            {
                {DirectionsEnum.Left, new Left()},
                {DirectionsEnum.Up, new Up()},
                {DirectionsEnum.Right, new Right()},
                {DirectionsEnum.Down, new Down()}
            });
            return services;
        }
    }
}
