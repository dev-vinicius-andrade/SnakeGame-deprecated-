using SnakeGame.Infrastructure.Enums;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Domain.Player.Interfaces
{
    public interface ICharHandler
    {
        IChar Model { get; }
        IPosition Move(IDirection direction,int movement=1);
        void Add(IChar playerChar, IPosition position, bool removeLast = true);
        bool ChangeDirection(DirectionsEnum newDirection);
    }
}