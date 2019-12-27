using System.Collections.Generic;
using SnakeGame.Domain.Player.Configurations;
using SnakeGame.Domain.Player.Enums;
using SnakeGame.Domain.Player.Helpers;
using SnakeGame.Domain.Player.Models;
using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Domain.Player
{
    public class SnakeGenerator
    {
        private readonly SnakeConfigurationModel _snakeConfiguration;
        private readonly Dictionary<DirectionsEnum, PositionModel> _knownDirections;
        public SnakeGenerator(SnakeConfigurationModel snakeConfiguration)
        {
            _snakeConfiguration = snakeConfiguration;
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
        public BaseChar Generate(ColorModel color, int xMaxValue, int yMaxValue)
        {
            var initialPosition = GenerateInitialPosition(color, _snakeConfiguration.HeadSize, xMaxValue, yMaxValue);
            var initialDirection = RandomDirection();
            var initialPath = GenerateInitialPath(
                initialPosition: initialPosition,
                initialDirection: initialDirection,
                color: color
            );

            return new SnakeModel
            {
                Path = initialPath,
                Size = _snakeConfiguration.HeadSize,
                Direction = initialDirection,
                Speed = _snakeConfiguration.Speed,
                Color = color,
            };

        }

        private PositionModel GenerateInitialPosition(ColorModel color, in int snakeConfigurationHeadSize,
            in int xMaxValue, in int yMaxValue)
            =>
                RandomHelper.RandomPosition(
                    xMinValue: 0,
                    xMaxValue: xMaxValue,
                    yMinValue: 0,
                    yMaxValue: yMaxValue,
                    color: color);



        private IList<PositionModel> GenerateInitialPath(PositionModel initialPosition, PositionModel initialDirection, ColorModel color)
        {
            var initialPath = new List<PositionModel>().AddEntity(initialPosition);
            for (var size = 1; size <= _snakeConfiguration.InitialSnakeSize; size++)
                initialPath
                    .Insert(0, new PositionModel
                    {
                        X = initialPosition.X + (initialDirection.X * size * _snakeConfiguration.HeadSize),
                        Y = initialPosition.Y + (initialDirection.Y * size * _snakeConfiguration.HeadSize),
                        Color = GetBodyColor(color)
                    });
            return initialPath;
        }

        public static ColorModel GetBodyColor(ColorModel color) => new ColorModel
        {
            BackgroundColor = ColorHelper.ChangeColorLevel(color.BackgroundColor, 1.5),
            BorderColor = color.BorderColor
        };

        private PositionModel RandomDirection()
        {
            var randomNumber = RandomHelper.RandomNumber(0, _knownDirections.Count);
            return _knownDirections.ContainsKey(randomNumber.ToDirectionsEnum())
                ? _knownDirections[randomNumber.ToDirectionsEnum()]
                : RandomDirection();
        }

    }
}
