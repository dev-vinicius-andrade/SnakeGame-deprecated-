using System;
using SnakeGame.Infrastructure.Configurations;
using SnakeGame.Infrastructure.Data.Models;
using SnakeGame.Infrastructure.Helpers;

namespace SnakeGame.Domain.Food
{
    public class FoodGenerator
    {
        private readonly GameConfigurations _configurations;
        public FoodGenerator(GameConfigurations configurations)
        {
            _configurations = configurations;
        }

        public FoodModel Generate(string color,string borderColor)
        {

            return new FoodModel
            {
                Guid = Guid.NewGuid(),
                Position = RandomHelper.RandomPosition(
                    xMinValue: 0,
                    xMaxValue: _configurations.RoomConfiguration.Width - _configurations.FoodConfiguration.FoodSize,
                    yMinValue: 0,
                    yMaxValue: _configurations.RoomConfiguration.Height - _configurations.FoodConfiguration.FoodSize,
                    color:color,
                    borderColor:borderColor
                    ),
                Color = color,
                BorderColor = _configurations.RoomConfiguration.BackgroundColor,
                Size = _configurations.FoodConfiguration.FoodSize
            };

        }


    }
}
