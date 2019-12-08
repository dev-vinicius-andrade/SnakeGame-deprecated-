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
            var randomAngle = RandomHelper.RandomNumber(0, 0);
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
            var initialPosition = GenerateInitialPosition();
            var initialDirection = RandomDirection();
            var initialPath = GeneratePath(
                initialPosition: initialPosition,
                initialDirection: initialDirection,
                pathSize: _configurations.SnakeConfiguration.InitialSnakeSize);
            
            return new SnakeModel
            {
                Path = initialPath,
                Size = initialPath.Count,
                HeadSize = _configurations.SnakeConfiguration.HeadSize,
                Direction = initialDirection,
                Color = color,
                BorderColor = borderColor,
                Speed = speed
                
            };

        }


        public IList<PositionModel> GeneratePath(PositionModel initialPosition, PositionModel initialDirection, int pathSize)
        {
            var initialPath = new List<PositionModel>().AddEntity(initialPosition);

            for (var size = 1; size <= pathSize; size++)
                initialPath
                    .Add(new PositionModel
                    {
                        X = initialPosition.X + (initialDirection.X * size*_configurations.SnakeConfiguration.HeadSize),
                        Y = initialPosition.Y + (initialDirection.Y * size*_configurations.SnakeConfiguration.HeadSize),
                        Angle = initialDirection.Angle
                    });
            return initialPath;
        }

        private PositionModel RandomDirection()
        {
            var randomNumber = RandomHelper.RandomNumber(0, _knownDirections.Count);
            return _knownDirections.ContainsKey(randomNumber.ToDirectionsEnum()) 
                ? _knownDirections[randomNumber.ToDirectionsEnum()] 
                : RandomDirection();
        }
        private PositionModel GenerateInitialPosition()=>RandomHelper.RandomPosition(
            xMinValue: 0,
            xMaxValue: _configurations.RoomConfiguration.Width - _configurations.SnakeConfiguration.HeadSize,
            yMinValue: 0,
            yMaxValue: _configurations.RoomConfiguration.Height - _configurations.SnakeConfiguration.HeadSize);
    }
}
