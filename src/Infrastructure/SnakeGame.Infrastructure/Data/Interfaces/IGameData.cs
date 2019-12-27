using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Infrastructure.Data.Interfaces
{
    public interface IGameData:ITrackable
    {
        IRoom Room { get; }
    }
}