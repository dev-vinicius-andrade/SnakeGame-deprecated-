using System;
using System.Collections.Concurrent;
using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Infrastructure.Data
{
    public class GameContext<TEntity> where 
        TEntity:IRoom<IChar,BaseFood>
    {
        public GameContext()
        {
            GameData = new ConcurrentDictionary<Guid,TEntity>();
        }
        public  ConcurrentDictionary<Guid, TEntity> GameData { get; }
    }
}
