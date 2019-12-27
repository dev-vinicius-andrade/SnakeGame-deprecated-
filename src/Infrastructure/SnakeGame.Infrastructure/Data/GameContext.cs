using System;
using System.Collections.Concurrent;
using SnakeGame.Infrastructure.Data.Interfaces;

namespace SnakeGame.Infrastructure.Data
{
    internal class GameContext : IGameContext

    {
        internal readonly ConcurrentDictionary<Guid, IGameData> GameData;
        public GameContext()
        {
            GameData = new ConcurrentDictionary<Guid, IGameData>();
        }

        public long Count=>GameData.Count;
        

        public IGameData Get(Guid id)
        {
            try
            {
                GameData.TryGetValue(id, out var gameData);
                return gameData;
            }
            catch
            {
                return null;
            }
        }

        public bool Add(IGameData gameData)
        {
            

            try
            {
                if (GameData.ContainsKey(gameData.Id))
                    throw new Exception("existent_id");
                GameData.TryAdd(gameData.Id, gameData);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(IGameData gameData)
        {


            try
            {
                if (!GameData.ContainsKey(gameData.Id))
                    return Add(gameData);
                GameData.TryGetValue(gameData.Id, out var oldGameData);
                return GameData.TryUpdate(gameData.Id, gameData, oldGameData);
            }
            catch
            {
                return false;
            }
        }

        public IGameData Remove(Guid id)
        {
            try
            {
                if (!GameData.ContainsKey(id))
                    throw new Exception("unexistent_id");

                GameData.TryRemove(id, out var gameData);
                return gameData;
            }
            catch
            {
                return null;
            }
        }

    }
}
