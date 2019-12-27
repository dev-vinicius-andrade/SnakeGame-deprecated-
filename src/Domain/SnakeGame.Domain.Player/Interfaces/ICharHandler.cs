using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Domain.Player.Interfaces
{
    public interface ICharHandler:IChar
    {
        PositionModel Move(PositionModel direction,int movement=1);
        void Add(BaseChar playerChar, PositionModel position, bool removeLast = true);
        bool ChangeDirection(PositionModel newDirection);
    }
}