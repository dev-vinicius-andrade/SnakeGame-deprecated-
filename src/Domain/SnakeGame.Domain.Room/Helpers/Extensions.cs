using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SnakeGame.Infrastructure.Abstractions;
using SnakeGame.Infrastructure.Helpers;
using SnakeGame.Infrastructure.Interfaces;
using SnakeGame.Infrastructure.Models;

namespace SnakeGame.Domain.Room.Helpers
{
    public static class Extensions
    {
        public static bool IsColorBeeingUsed<TChar, TFood>(this IRoom<TChar, TFood> room, string color)
            where TChar : IChar
            where TFood : BaseFood, IPositionObject
        {
            return room.AnyCharWithColor(color) || room.AnyFoodWithColor(color);
        }

        public static bool AnyCharWithColor<TChar, TFood>(this IRoom<TChar, TFood> room, string color)
            where TChar : IChar
            where TFood : BaseFood, IPositionObject
        {
            return room.Players.Any(p => p.Char.Model.Color.BackgroundColor == color);
        }

        public static bool AnyFoodWithColor<TChar, TFood>(this IRoom<TChar, TFood> room, string color)
            where TChar : IChar
            where TFood : BaseFood, IPositionObject
        {
            return room.Foods.Any(p => p.Position.Color.BackgroundColor == color);
        }



        public static IList<T> GetNearBy<T>(this IList<T> positions, PositionModel position, int delta = 0) 
            where  T:IPositionObject
            =>
            positions.Where(p =>
            {
                var xPositionCompare = CalculationsHelper.Distance(p.Position.X.Value, position.X.Value) <=
                                       delta;
                var yPositionCompare = CalculationsHelper.Distance(p.Position.Y.Value, position.Y.Value) <=
                                       delta;
                return xPositionCompare && yPositionCompare;
            }).ToList();

    }
}
