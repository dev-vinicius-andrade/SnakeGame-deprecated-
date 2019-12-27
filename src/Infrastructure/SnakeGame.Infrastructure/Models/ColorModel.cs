namespace SnakeGame.Infrastructure.Models
{
    public class ColorModel
    {
        public ColorModel(){}
        public ColorModel(string backgroundColor, string borderColor)
        {
            BackgroundColor = backgroundColor;
            BorderColor = borderColor;
        }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
    }
}
