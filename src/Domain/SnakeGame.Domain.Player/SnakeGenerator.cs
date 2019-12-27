using System.Collections.Generic;
using SnakeGame.Domain.Player.Abstractions;
using SnakeGame.Domain.Player.Configurations;
using SnakeGame.Domain.Player.Helpers;
using SnakeGame.Domain.Player.Models;
using SnakeGame.Infrastructure.Enums;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Domain.Player
{
    public class SnakeGenerator
    {
        private readonly SnakeConfigurationModel _snakeConfiguration;
        private readonly IReadOnlyDictionary<DirectionsEnum, IDirection> _knownDirections;
        public SnakeGenerator(
            IReadOnlyDictionary<DirectionsEnum, IDirection> knownDirections,
            SnakeConfigurationModel snakeConfiguration
            )
        {
            _snakeConfiguration = snakeConfiguration;
            _knownDirections = knownDirections;
        }
        public IChar Generate(ColorModel color, int xMaxValue, int yMaxValue)
        {
            var initialPosition = GenerateInitialPosition(color, _snakeConfiguration.HeadSize, xMaxValue, yMaxValue);
            var initialDirection = RandomDirection();
            var initialPath = GenerateInitialPath(
                initialPosition: initialPosition,
                initialDirection: initialDirection,
                color: color
            );

            return new SnakeModel(_knownDirections)
            {
                Path = initialPath,
                Size = _snakeConfiguration.HeadSize,
                Direction = initialDirection,
                Speed = _snakeConfiguration.Speed,
                Color = color,
            };

        }

        private IPosition GenerateInitialPosition(IColor color, in int snakeConfigurationHeadSize,
            in int xMaxValue, in int yMaxValue)
            =>
                RandomHelper.RandomPosition(
                    xMinValue: 0,
                    xMaxValue: xMaxValue,
                    yMinValue: 0,
                    yMaxValue: yMaxValue,
                    color: color);



        private IList<IPosition> GenerateInitialPath(IPosition initialPosition, IDirection initialDirection, ColorModel color)
        {
            var initialPath = new List<IPosition>().AddEntity(initialPosition);
            for (var size = 1; size <= _snakeConfiguration.InitialSnakeSize; size++)
                initialPath
                    .Insert(0, new PositionModel
                    {
                        Coordinate = new CoordinateModel
                        {
                            X = initialPosition.Coordinate.X + (initialDirection.XSpeed * size * _snakeConfiguration.HeadSize),
                            Y = initialPosition.Coordinate.Y + (initialDirection.YSpeed * size * _snakeConfiguration.HeadSize)
                        },
                        Color = GetBodyColor(color)
                    });
            return initialPath;
        }

        public static IColor GetBodyColor(IColor color) => new ColorModel
        {
            Background = ColorHelper.ChangeColorLevel(color.Background, 1.5),
            Border = color.Border
        };

        private IDirection RandomDirection()
        {
            var randomNumber = RandomHelper.RandomNumber(0, _knownDirections.Count);
            return _knownDirections.ContainsKey(randomNumber.ToDirectionsEnum())
                ? _knownDirections[randomNumber.ToDirectionsEnum()]
                : RandomDirection();
        }

    }
}
