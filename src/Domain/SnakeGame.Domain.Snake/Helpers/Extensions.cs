using SnakeGame.Domain.Snake.Enums;

namespace SnakeGame.Domain.Snake.Helpers
{
    public static class Extensions
    {

        public static DirectionsEnum ToDirectionsEnum(this int value) => (DirectionsEnum) value;
    }
}
