using System.Collections.Generic;
using System.Linq;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Domain.Snake
{
    public class SnakeMovementTracker
    {
        private readonly SnakeModel _snake;
        private List<PositionModel> _afterMovement;
        public SnakeMovementTracker(SnakeModel snake)
        {
            _snake = snake;
            RemovePosition = snake.Path.Last().Clone();
        }

        public SnakeModel Snake => _snake;
        public PositionModel RemovePosition { get; }

        public IReadOnlyList<PositionModel> AfterMovement => _afterMovement;

        public SnakeMovementTracker TrackMovement(PositionModel position)
        {
            _snake.Path.Add(position);
            _snake.Path.RemoveAt(0);
            _afterMovement = _snake.Path.Clone().ToList();
            return this;
        }


    }
}
