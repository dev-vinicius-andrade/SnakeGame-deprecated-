using System;
using System.Collections.Generic;
using SnakeGame.Domain.Snake;
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


        public void Move(SnakeModel snake, PositionModel position)
        {
            snake.CurrentlyPosition = new PositionModel
            {
                X = position.X,
                Y = position.Y,
                Angle = position.Angle
            };
            snake.Path.Add(position);
            snake.Path.RemoveAt(0);
        }
    }
}
