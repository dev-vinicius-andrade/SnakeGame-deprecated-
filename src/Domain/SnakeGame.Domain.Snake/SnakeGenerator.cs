using System.Collections.Generic;
using SnakeGame.Domain.Snake.Enums;
using SnakeGame.Domain.Snake.Helpers;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Infrastructure.Models.Configurations;

namespace SnakeGame.Domain.Snake
{
    public class SnakeGenerator
    {
        private readonly GameConfigurationsModel _configurations;
        private readonly Dictionary<DirectionsEnum, PositionModel> _knownDirections;
        public SnakeGenerator(GameConfigurationsModel configurations)
        {
            _configurations = configurations;
            _knownDirections = RegisterKnownDirections();
        }
        private Dictionary<DirectionsEnum, PositionModel> RegisterKnownDirections()
        {
            var randomAngle = RandomHelper.RandomNumber(0, 360);
            return new Dictionary<DirectionsEnum, PositionModel>
            {
                { DirectionsEnum.Left, new PositionModel { X = -1, Y = 0, Angle = randomAngle } },
                { DirectionsEnum.Up, new PositionModel { X = 0, Y = -1, Angle = 0 } },
                { DirectionsEnum.Right, new PositionModel { X = 1, Y = 0, Angle = randomAngle } },
                { DirectionsEnum.Down, new PositionModel { X = 0, Y = 1, Angle = 0 } }
            };
        }

        public SnakeModel Generate(string color)
        {
            var initialPosition = RandomHelper.RandomPosition(
                xMinValue: 0,
                xMaxValue: _configurations.Width - _configurations.FoodConfiguration.FoodSize,
                yMinValue: 0,
                yMaxValue: _configurations.Height - _configurations.FoodConfiguration.FoodSize);

            return new SnakeModel
            {

                CurrentlyPosition = initialPosition,
                Path = new List<PositionModel> { initialPosition },
                Size = 1,
                Direction = RandomDirection(),
                Color = color,
                Speed = _configurations.SnakeConfiguration.Speed
            };

        }

        private PositionModel RandomDirection()
        {

            var randomNumber = RandomHelper.RandomNumber(0, _knownDirections.Count);
            return _knownDirections.ContainsKey(randomNumber.ToDirectionsEnum()) 
                ? _knownDirections[randomNumber.ToDirectionsEnum()] 
                : RandomDirection();
        }
    }
}
