using System;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Infrastructure.Helpers
{
    public static class RandomHelper
    {
        
        public static PositionModel RandomPosition(int xMinValue, int xMaxValue, int yMinValue, int yMaxValue)
        {
            var randomizer = new Random();
            return new PositionModel
            {
                Angle = 0,
                X = randomizer.Next(xMinValue, xMaxValue),
                Y = randomizer.Next(yMinValue, yMaxValue)
            };
        }
    }
}
