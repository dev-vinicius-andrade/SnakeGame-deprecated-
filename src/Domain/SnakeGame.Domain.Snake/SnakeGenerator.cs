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
        private readonly GameConfigurations _configurations;
        private readonly Dictionary<DirectionsEnum, PositionModel> _knownDirections;
        public SnakeGenerator(GameConfigurations configurations)
        {
            _configurations = configurations;
            _knownDirections = RegisterKnownDirections();
        }
        private Dictionary<DirectionsEnum, PositionModel> RegisterKnownDirections()
        {
            return new Dictionary<DirectionsEnum, PositionModel>
            {
                { DirectionsEnum.Left, new PositionModel { X = -1, Y = 0 } },
                { DirectionsEnum.Up, new PositionModel { X = 0, Y = -1} },
                { DirectionsEnum.Right, new PositionModel { X = 1, Y = 0 } },
                { DirectionsEnum.Down, new PositionModel { X = 0, Y = 1 } }
            };
        }

        public SnakeModel Generate(string color, string borderColor)
        {
            var speed = _configurations.SnakeConfiguration.Speed;
            var initialPosition = GenerateInitialPosition(color,borderColor);
            var initialDirection = RandomDirection();
            var initialPath = GenerateInitialPath(
                initialPosition: initialPosition,
                initialDirection: initialDirection,
                pathSize: _configurations.SnakeConfiguration.InitialSnakeSize,
                color: GetBodyColor(color) ,
                borderColor: borderColor
            );
            
            return new SnakeModel
            {
                Path = initialPath,
                Size = _configurations.SnakeConfiguration.InitialSnakeSize,
                HeadSize = _configurations.SnakeConfiguration.HeadSize,
                Direction = initialDirection,
                Speed = speed,
                Color = initialPosition.Color,
                BorderColor = initialPosition.BorderColor
                
            };

        }


        public IList<PositionModel> GenerateInitialPath(PositionModel initialPosition, PositionModel initialDirection, int pathSize,string color, string borderColor)
        {
            var initialPath = new List<PositionModel>().AddEntity(initialPosition);

            for (var size = 1; size <= pathSize; size++)
                initialPath
                    .Insert(0, new PositionModel
                    {
                        X = initialPosition.X + (initialDirection.X * size*_configurations.SnakeConfiguration.HeadSize),
                        Y = initialPosition.Y + (initialDirection.Y * size*_configurations.SnakeConfiguration.HeadSize),
                        Color =  color,
                        BorderColor = borderColor
                    });
            return initialPath;
        }

        public static string GetBodyColor(string color) => ColorHelper.ChangeColorLevel(color, 1.5);

        private PositionModel RandomDirection()
        {
            var randomNumber = RandomHelper.RandomNumber(0, _knownDirections.Count);
            return _knownDirections.ContainsKey(randomNumber.ToDirectionsEnum()) 
                ? _knownDirections[randomNumber.ToDirectionsEnum()] 
                : RandomDirection();
        }
        private PositionModel GenerateInitialPosition(string color, string borderColor) =>RandomHelper.RandomPosition(
            xMinValue: 0,
            xMaxValue: _configurations.RoomConfiguration.Width - _configurations.SnakeConfiguration.HeadSize,
            yMinValue: 0,
            yMaxValue: _configurations.RoomConfiguration.Height - _configurations.SnakeConfiguration.HeadSize,
            color: color,
            borderColor:borderColor

            );
    }
}
