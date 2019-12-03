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
        public SnakeModel Create(string color)
        {
            return new SnakeGenerator(_gameData.Configurations)
                .Generate(color);
        }


        public SnakeMovementTracker Move(SnakeModel snake, PositionModel position)
        {
            return new SnakeMovementTracker(snake).TrackMovement(position);
        }
    }
}
