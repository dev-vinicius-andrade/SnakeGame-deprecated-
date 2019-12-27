using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGame.Infrastructure.Data.Interfaces;
using SnakeGame.Infrastructure.Helpers;

namespace SnakeGame.Infrastructure.Data.Extensions
{
    public static class DataExtensions
    {
        public static IEnumerable<IGameData> Where(this IGameContext context, Func<IGameData, bool> predicate)
        {
            var gameContext = context as GameContext;
            return gameContext.IsNullOrEmpty() 
                ? null 
                : gameContext.GameData.Values.Where(predicate);
        }
    }
}
