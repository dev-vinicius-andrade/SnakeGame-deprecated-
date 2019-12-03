using System.Collections.Generic;
using System.Linq;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Domain.Snake
{
    public class SnakeMovementTracker
    {
        private readonly SnakeModel _snake;
        private readonly List<PositionModel> _beforeMovement;
        private List<PositionModel> _afterMovement;
        public SnakeMovementTracker(SnakeModel snake)
        {
            _snake = snake;
            _beforeMovement = snake.Path.Clone();
        }

        public SnakeModel Snake => _snake;
        public IReadOnlyList<PositionModel> BeforeMovement => _beforeMovement;
        public IReadOnlyList<PositionModel> AfterMovement => _afterMovement;

        public SnakeMovementTracker TrackMovement(PositionModel position)
        {
            _snake.Path.Add(position);
            _snake.Path.RemoveAt(0);
            _afterMovement = _snake.Path.Clone();
            return this;
        }

    }
}
