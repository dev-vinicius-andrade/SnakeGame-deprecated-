using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnakeGame.Infrastructure.Helpers
{   public static class ColorHelper
    {
        /// <summary>
        /// level > 1 lighten color
        /// level < 1 darken color
        /// </summary>
        /// <param name="hexadecimalColor"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static string ChangeColorLevel(string hexadecimalColor, double level) {
            
            var colorConverted = ColorTranslator.FromHtml(hexadecimalColor.GetHexadecimalColor());

                var color = (Color) colorConverted;
                var lightedColor = Color.FromArgb(color.A, (int) (CalculateColorLevel(color.R,level)), (int) (CalculateColorLevel(color.G,level)),
                    (int) (CalculateColorLevel(color.B , level)));

            return $"#{(lightedColor.ToArgb() & 0x00FFFFFF).ToString("X6")}";
        }

        private static int CalculateColorLevel(int color, double level)
        {
            var calculatedValue = color * Math.Abs(level);
            if (calculatedValue > 255)
                return 255;
            if (calculatedValue <0)
                return 0;
            return (int)Math.Round(calculatedValue);
        }
        public static string GetHexadecimalColor(this string color) => color.StartsWith("#") ? color : $"#{color}";




    }
}
