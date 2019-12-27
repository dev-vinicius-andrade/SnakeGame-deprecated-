using System.Collections.Generic;
using System.Linq;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Interfaces;

namespace SnakeGame.Domain.Room.Helpers
{
    public static class Extensions
    {
        public static bool IsColorBeeingUsed(this IRoom room, string color)
            => room.AnyCharWithColor(color) || room.AnyFoodWithColor(color);

        public static bool AnyCharWithColor(this IRoom room, string color)
            => room.Players.Any(p => p.Char.Color.Background == color);

        public static bool AnyFoodWithColor(this IRoom room, string color)
            =>room.Foods.Any(p => p.Position.Color.Background == color);



        public static IList<T> GetNearBy<T>(this IList<T> positions, IPosition position, int delta = 0)
            where T : ICurrentPosition
            =>
            positions.Where(p =>
            {
                var xPositionCompare = CalculationsHelper.Distance(p.Position.Coordinate.X.Value, position.Coordinate.X.Value) <=
                                       delta;
                var yPositionCompare = CalculationsHelper.Distance(p.Position.Coordinate.Y.Value, position.Coordinate.Y.Value) <=
                                       delta;
                return xPositionCompare && yPositionCompare;
            }).ToList();

    }
}
