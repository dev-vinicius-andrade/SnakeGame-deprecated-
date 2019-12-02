using System.Collections.Generic;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Domain.Snake
{
    public class SnakeGenerator
    {
        private readonly GameConfigurationsModel _configurations;
        public SnakeGenerator(GameConfigurationsModel configurations)
        {

            _configurations = configurations;
        }

        public SnakeModel Generate()
        {
            var initialPosition = RandomHelper.RandomPosition(
                xMinValue:0, 
                xMaxValue:_configurations.Width - _configurations.FoodSize, 
                yMinValue:  0,
                yMaxValue:_configurations.Height - _configurations.FoodSize);
            
            return new SnakeModel
            {
                
                CurrentlyPosition = initialPosition,
                Path = new List<PositionModel> { initialPosition},
                Size = 1
                
            };

        }
    }
}
