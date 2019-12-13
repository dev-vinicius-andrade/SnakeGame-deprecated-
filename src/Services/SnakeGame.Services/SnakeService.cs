using System.Collections.Generic;
using System.Linq;
using SnakeGame.Domain.Snake;
using SnakeGame.Infrastructure.Configurations;
using SnakeGame.Infrastructure.Data.Models;
using SnakeGame.Infrastructure.Helpers;

namespace SnakeGame.Services
{
    public class SnakeService
    {
        private readonly GameConfigurations _configurations;

        public SnakeService(GameConfigurations configurations)
        {
            _configurations = configurations;
        }
        public SnakeModel Create(string color, string borderColor)
        {
            lock (_configurations)
            {
                return new SnakeGenerator(_configurations)
                    .Generate(color, borderColor);
            }
        }


        public PositionModel Move(SnakeModel snake, PositionModel direction)
        {
            lock (snake)
            {
                return BoundaryReachPositionRecalculator(
                    position: new PositionModel
                    {
                        X = snake.CurrentlyPosition.X + GetDirectionAxisMovement(direction.X.Value),
                        Y = snake.CurrentlyPosition.Y + GetDirectionAxisMovement(direction.Y.Value)

                    },
                    direction: snake.Direction);
            }

        }

        private PositionModel BoundaryReachPositionRecalculator(PositionModel position, PositionModel direction)
        {
            var recalculatedPosition = position.Clone();
            if (position.X >= _configurations.RoomConfiguration.Width && direction.X == 1)
                recalculatedPosition.X = 0;
            if (position.X <= 0 && direction.X == -1)
                recalculatedPosition.X = _configurations.RoomConfiguration.Width;
            if (position.Y <= 0 && direction.Y == -1)
                recalculatedPosition.Y = _configurations.RoomConfiguration.Height;
            if (position.Y >= _configurations.RoomConfiguration.Height && direction.Y == 1)
                recalculatedPosition.Y = 0;
            return recalculatedPosition;
        }

        private int GetDirectionAxisMovement(int axisValue) => axisValue * _configurations.SnakeConfiguration.HeadSize;

        public ResponseModel ChangeSpeedConfiguration(int value)
        {
            lock (_configurations)
            {
                if (value == _configurations.SnakeConfiguration.Speed)
                    return ResponseHelper.CreateBadRequest("Speed is already at this value");
                _configurations.SnakeConfiguration.Speed = value;
                return ResponseHelper.CreateOk("Speed Changed");
            }
        }

        public ResponseModel ChangeInitialSize(int value)
        {
            lock (_configurations)
            {
                if (value == _configurations.SnakeConfiguration.InitialSnakeSize)
                    return ResponseHelper.CreateBadRequest("Initial Size is already at this value");
                _configurations.SnakeConfiguration.InitialSnakeSize = value;
                return ResponseHelper.CreateOk("Initial Size Changed");
            }
        }

        public void Add(SnakeModel snake, PositionModel position, bool removeLast = true)
        {
            lock (_configurations)
            {
                snake.CurrentlyPosition.Color = SnakeGenerator.GetBodyColor(snake.Color);
                snake.Path.Add(new PositionModel
                {
                    X = position.X,
                    Y = position.Y,
                    Color = snake.Color,
                    BorderColor = snake.BorderColor
                });
                if (removeLast)
                    snake.Path.RemoveAt(0);



            }
        }

        public bool ChangeDirection(SnakeModel snake, PositionModel newDirection)
        {
            if (!DirectionChanged(snake.Direction, newDirection))
                return false;


            snake.Direction = new PositionModel
            {
                X = newDirection.X,
                Y = newDirection.Y
            };

            return true;
        }

        public bool DirectionChanged(PositionModel currentlyDirection, PositionModel newDirection)
        {
            if (currentlyDirection.X == 0 && newDirection.X != 0)
                return true;

            if (currentlyDirection.Y == 0 && newDirection.Y != 0)
                return true;

            return false;
        }


    }
}
