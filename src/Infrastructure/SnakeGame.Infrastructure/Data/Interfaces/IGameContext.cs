using System;

namespace SnakeGame.Infrastructure.Data.Interfaces
{
    public interface IGameContext
    {
        long Count { get; }
        IGameData Get(Guid id);
        bool Add(IGameData gameData);
        bool Update(IGameData gameData);
        IGameData Remove(Guid id);

    }
}