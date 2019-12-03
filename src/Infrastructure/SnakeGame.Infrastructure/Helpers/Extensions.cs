using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SnakeGame.Infrastructure.Helpers
{
    public static class Extensions
    {

        public static bool IsNullOrEmpty(this string text) => string.IsNullOrEmpty(text);
        public static bool IsNull(this object obj) => obj == null;
        public static Guid ToGuid(this string guid) => Guid.Parse(guid);

        public static T Clone<T>(this T originalObject) where T:class
        {
            try
            {
                var originalObjectSerialized = JsonSerializer.Serialize(originalObject);
                return JsonSerializer.Deserialize<T>(originalObjectSerialized);
            }
            catch (Exception)
            {

                return originalObject.Clone();
            }
        }


    }
}
