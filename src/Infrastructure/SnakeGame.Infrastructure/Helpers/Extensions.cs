using System;
namespace SnakeGame.Infrastructure.Helpers
{
    public static class Extensions
    {

        public static bool IsNullOrEmpty(this string text) => string.IsNullOrEmpty(text);
        public static bool IsNull(this object obj) => obj == null;
        public static Guid ToGuid(this string guid) => Guid.Parse(guid);


    }
}
