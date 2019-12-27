using SnakeGame.Domain.Player.Enums;

namespace SnakeGame.Domain.Player.Helpers
{
    public static class Extensions
    {

        public static DirectionsEnum ToDirectionsEnum(this int value) => (DirectionsEnum) value;
    }
}
