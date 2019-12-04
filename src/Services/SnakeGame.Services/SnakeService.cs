using System;
using System.Collections.Generic;
using SnakeGame.Domain.Snake;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services.Entities;

namespace SnakeGame.Services
{
    public class SnakeService
    {
        private readonly GameData _gameData;
        private readonly FoodService _foodService;

        public SnakeService(GameData gameData, FoodService foodService)
        {
            _gameData = gameData;
            _foodService = foodService;
        }
        public SnakeModel Create(string color,string borderColor)
        {
            return new SnakeGenerator(_gameData.Configurations)
                .Generate(color,borderColor);
        }


        public SnakeMovementTracker Move(SnakeModel snake, PositionModel direction)
        {
            lock (snake)
            {
                var currentlyPosition = snake.CurrentlyPosition;
                return new SnakeMovementTracker(snake)
                    .TrackMovement( BoundaryReachPositionRecalculator(
                          new PositionModel
                {
                    X = currentlyPosition.X + GetDirectionAxisSpeed(direction.X.Value),
                    Y = currentlyPosition.Y + GetDirectionAxisSpeed(direction.Y.Value),
                    Angle = direction.Angle
                }));
            }
            
        }

        private PositionModel BoundaryReachPositionRecalculator(PositionModel position)
        {
            var recalculatedPosition = position.Clone();
            if (position.X >= _gameData.Configurations.RoomConfiguration.Width)
                recalculatedPosition.X = 0;
            if (position.X <=0)
                recalculatedPosition.X = _gameData.Configurations.RoomConfiguration.Width;
            if (position.Y <= 0)
                recalculatedPosition.Y = _gameData.Configurations.RoomConfiguration.Height;
            if (position.Y >= _gameData.Configurations.RoomConfiguration.Height)
                recalculatedPosition.Y = 0;
            return recalculatedPosition;
        }
        private int GetDirectionAxisSpeed(int axisValue) => axisValue * _gameData.Configurations.SnakeConfiguration.Speed;
    }
}
