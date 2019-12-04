using System;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Infrastructure.Models.Configurations;

namespace SnakeGame.Domain.Food
{
    public class FoodGenerator
    {
        private readonly GameConfigurationsModel _configurations;
        public FoodGenerator(GameConfigurationsModel configurations)
        {
            _configurations = configurations;
        }

        public FoodModel Generate(string color)
        {

            return new FoodModel
            {
                Guid = Guid.NewGuid(),
                Position = RandomHelper.RandomPosition(
                    xMinValue: 0,
                    xMaxValue: _configurations.RoomConfiguration.Width - _configurations.FoodConfiguration.FoodSize,
                    yMinValue: 0,
                    yMaxValue: _configurations.RoomConfiguration.Height - _configurations.FoodConfiguration.FoodSize),
                Color = color,
                BorderColor = _configurations.RoomConfiguration.BackgroundColor,
                Size = _configurations.FoodConfiguration.FoodSize
            };

        }


    }
}
