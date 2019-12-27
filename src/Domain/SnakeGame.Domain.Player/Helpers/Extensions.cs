using SnakeGame.Infrastructure.Enums;

namespace SnakeGame.Domain.Player.Helpers
{
    public static class Extensions
    {

        public static DirectionsEnum ToDirectionsEnum(this int value) => (DirectionsEnum) value;
    }
}
