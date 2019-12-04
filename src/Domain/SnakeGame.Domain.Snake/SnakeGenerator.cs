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

        public SnakeModel Generate(string color, string borderColor)
        {
            var speed = _configurations.SnakeConfiguration.Speed;
            var initialPosition = RandomHelper.RandomPosition(
                xMinValue: 0,
                xMaxValue: _configurations.RoomConfiguration.Width - _configurations.FoodConfiguration.FoodSize,
                yMinValue: 0,
                yMaxValue: _configurations.RoomConfiguration.Height - _configurations.FoodConfiguration.FoodSize);
            var randomDirection = RandomDirection();
            var initialPath = new List<PositionModel>()
                                .AddEntity(initialPosition)
                                .AddEntity(new PositionModel
                                {
                                    X = initialPosition.X +(randomDirection.X*speed),
                                    Y = initialPosition.Y + (randomDirection.Y*speed),
                                    Angle = randomDirection.Angle
                                }).AddEntity(new PositionModel
                                {
                                    X = initialPosition.X + (randomDirection.X * speed*2),
                                    Y = initialPosition.Y + (randomDirection.Y * speed*2),
                                    Angle = randomDirection.Angle
                                });
            
            return new SnakeModel
            {
                Path = initialPath,
                Size = initialPath.Count,
                HeadSize = _configurations.SnakeConfiguration.HeadSize,
                Direction = randomDirection,
                Color = color,
                BorderColor = borderColor,
                Speed = speed
                
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
