using System.Collections.Generic;
using System.Linq;

public static class ArrayExtensions
{
    public static T Random<T>(this IEnumerable<T> list)
    {
        return list.ElementAtOrDefault(UnityEngine.Random.Range(0, list.Count()));
    }
}