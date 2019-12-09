using System;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Infrastructure.Helpers
{
    public static class RandomHelper
    {
        public static PositionModel RandomPosition(int xMinValue, int xMaxValue, int yMinValue, int yMaxValue, string color, string borderColor)
        {
            var randomizer = new Random();
            return new PositionModel
            {
                X = randomizer.Next(xMinValue, xMaxValue),
                Y = randomizer.Next(yMinValue, yMaxValue),
                Color = color,
                BorderColor = borderColor
            };
        }
        public static int RandomNumber(int minimunValue, int maximunValue)=>
            new Random().Next(minimunValue,maximunValue);
        public static string RandomColor()=>
             $"#{RandomNumber(0x1000000):X6}";

        public static int RandomNumber(int? maxValue=null)=>maxValue.IsNull()
                ?new Random().Next()
                :new Random().Next(maxValue.Value);
    }
}
