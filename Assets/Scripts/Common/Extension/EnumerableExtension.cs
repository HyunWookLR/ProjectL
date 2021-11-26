using System.Linq;
using System.Collections.Generic;

public static class EnumerableExtension
{
    public static bool NotContains<T>(this IEnumerable<T> enumerable, T value)
    {
        return !enumerable.Contains(value);
    }
    public static bool HasItem<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.Count() > 0;
    }
    public static bool NotHaveItem<T>(this IEnumerable<T> enumerable)
    {
        return !enumerable.HasItem();
    }
}
