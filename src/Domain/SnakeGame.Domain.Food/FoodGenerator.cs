using System;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Domain.Food
{
    public class FoodGenerator
    {
        private readonly GameConfigurationsModel _configurations;
        public FoodGenerator(GameConfigurationsModel configurations)
        {
            _configurations = configurations;
        }

        public FoodModel Generate()
        {

            return new FoodModel
            {
                Guid = Guid.NewGuid(),
                Position = RandomHelper.RandomPosition(
                    xMinValue: 0,
                    xMaxValue: _configurations.Width - _configurations.FoodSize,
                    yMinValue: 0,
                    yMaxValue: _configurations.Height - _configurations.FoodSize)
            };

        }


    }
}
