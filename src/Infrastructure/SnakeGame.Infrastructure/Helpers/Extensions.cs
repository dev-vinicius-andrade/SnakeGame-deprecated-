using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace SnakeGame.Infrastructure.Helpers
{
    public static class Extensions
    {

        public static bool IsNullOrEmpty(this object obj)
        {
            if (obj is string)
                return string.IsNullOrEmpty(obj.ToString());

            return obj == null;
        }
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

        public static IList<T> AddEntity<T>(this IEnumerable<T> enumerable, T entity)
        {
            var list = enumerable.ToList();
            list.Add(entity);
            return list;
        }
    }
}
