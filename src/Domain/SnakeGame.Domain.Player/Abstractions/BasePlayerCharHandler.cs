using System.Collections.Generic;
using SnakeGame.Domain.Player.Interfaces;
using SnakeGame.Infrastructure.Enums;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;


namespace SnakeGame.Domain.Player.Abstractions
{
    public abstract class BasePlayerCharHandler : ICharHandler
    {
        private readonly int _xLimit;
        private readonly int _yLimit;
        public IChar Model { get; }


        protected BasePlayerCharHandler(IChar playerChar, int xLimit, int yLimit)
        {
            _xLimit = xLimit;
            _yLimit = yLimit;
            Model = playerChar;
        }

        protected abstract int GetDirectionAxisMovement(int axisValue, int movement = 1);
        protected abstract IPosition BoundaryReachPositionRecalculator(IPosition newPosition, IDirection direction, int xMaxValue, int yMaxValue);
        public IPosition Move(IDirection direction, int movement = 1)
        {
            if (direction.XSpeed == 0 && direction.YSpeed == 0)
                return Model.Position;

            var newPosition = new PositionModel
            {
                Coordinate = new CoordinateModel
                {
                    X = Model.Position.Coordinate.X + GetDirectionAxisMovement(direction.XSpeed, movement),
                    Y = Model.Position.Coordinate.Y + GetDirectionAxisMovement(direction.YSpeed, movement)
                }
            };
            return BoundaryReachPositionRecalculator(
                newPosition: newPosition,
                direction: direction,
                xMaxValue: _xLimit,
                yMaxValue: _yLimit);
        }


        public virtual void Add(IChar playerChar, IPosition position, bool removeLast = true)
        {
            playerChar.Position.Color = SnakeGenerator.GetBodyColor(playerChar.Color);
            playerChar.Path.Add(new PositionModel
            {
                Coordinate = new CoordinateModel
                {
                    X = position.Coordinate.X,
                    Y = position.Coordinate.Y
                },
                Color = playerChar.Color
            });
            if (removeLast)
                playerChar.Path.RemoveAt(0);
        }

        public bool ChangeDirection(DirectionsEnum newDirection)
        {
            if (!Model.KnownDirections.ContainsKey(newDirection))
                return false;

            var direction = Model.KnownDirections[newDirection];
            if (!DirectionChanged(Model.Direction, direction))
                return false;
            Model.Direction = direction.Clone();
            return true;
        }

        private bool DirectionChanged(IDirection currentlyDirection, IDirection newDirection)
        {
            if (currentlyDirection.Direction.Equals(newDirection.Direction))
                return false;

            if (currentlyDirection.XSpeed == 0 && newDirection.XSpeed != 0)
                return true;

            if (currentlyDirection.YSpeed == 0 && newDirection.YSpeed != 0)
                return true;

            return false;
        }


    }
}
