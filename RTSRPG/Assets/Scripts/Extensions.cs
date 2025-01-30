using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static bool IsCloseEnough(this Vector2 position, Vector2 target, float epsilon = 1E-8f)
    {
        return Vector2.Distance(position, target) <= epsilon;
    }
    public static bool IsCloseEnough(this Vector3 position, Vector3 target, float epsilon = 1E-8f)
    {
        return Vector3.Distance(position, target) <= epsilon;
    }
    public static T GetRandom<T>(this IEnumerable<T> ienumerable)
    {
        var index = Random.Range(0, ienumerable.Count());
        return ienumerable.ElementAt(index);
    }
}