using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Infrastructure.Models
{
    public class ColorModel:IColor
    {
        public ColorModel(){}
        public ColorModel(string backgroundColor, string borderColor)
        {
            Background = backgroundColor;
            Border = borderColor;
        }
        public string Background { get; set; }
        public string Border { get; set; }
    }
}
