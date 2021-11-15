using System.Linq;
using System.Collections.Generic;

public static class EnumerableExtension
{
    public static bool NotContains<T>(this IEnumerable<T> enumerable, T value)
    {
        return !enumerable.Contains(value);
    }
}
