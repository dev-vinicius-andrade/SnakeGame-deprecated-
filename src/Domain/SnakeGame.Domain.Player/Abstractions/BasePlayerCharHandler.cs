
using System;
using SnakeGame.Domain.Player.Interfaces;
using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;


namespace SnakeGame.Domain.Player.Abstractions
{
    public abstract class BasePlayerCharHandler : ICharHandler
    {
        private readonly int _xLimit;
        private readonly int _yLimit;
        public Guid Id { get; }
        public BaseChar Model { get; }

        protected BasePlayerCharHandler(BaseChar playerChar, int xLimit,int yLimit)
        {
            _xLimit = xLimit;
            _yLimit = yLimit;
            Model = playerChar;
            Id = Guid.NewGuid();

        }

        protected abstract int GetDirectionAxisMovement(int axisValue,int movement=1);
        protected abstract PositionModel BoundaryReachPositionRecalculator(PositionModel newPosition, PositionModel direction, int xMaxValue, int yMaxValue);
        public PositionModel Move(PositionModel direction,int movement=1)
        {
            if (direction.X == null && direction.Y == null)
                return Model.Position;

            var newPosition = new PositionModel
            {
                X = Model.Position.X + GetDirectionAxisMovement(direction.X.Value,movement),
                Y = Model.Position.Y + GetDirectionAxisMovement(direction.Y.Value,movement)
            };
            return BoundaryReachPositionRecalculator(
                newPosition:newPosition, 
                direction:direction,
                xMaxValue:_xLimit,
                yMaxValue:_yLimit);
        }
 

        public virtual void Add(BaseChar playerChar, PositionModel position, bool removeLast = true)
        {
            playerChar.Position.Color = SnakeGenerator.GetBodyColor(playerChar.Color);
            playerChar.Path.Add(new PositionModel
            {
                X = position.X,
                Y = position.Y,
                Color = playerChar.Color
            });
            if (removeLast)
                playerChar.Path.RemoveAt(0);
        }

        public bool ChangeDirection(PositionModel newDirection)
        {
            if (!DirectionChanged(Model.Direction, newDirection))
                return false;


            Model.Direction = new PositionModel
            {
                X = newDirection.X,
                Y = newDirection.Y
            };

            return true;
        }

        private bool DirectionChanged(PositionModel currentlyDirection, PositionModel newDirection)
        {
            if (currentlyDirection.X == 0 && newDirection.X != 0)
                return true;

            if (currentlyDirection.Y == 0 && newDirection.Y != 0)
                return true;

            return false;
        }


    }
}
