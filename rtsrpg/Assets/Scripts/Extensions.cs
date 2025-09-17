using System;
using System.Threading.Tasks;
using Core;
using UnityEngine;

public static class Extensions
{
    public static T Copy<T>(this T source)
    {
        var type = typeof(T);
        var copy = type.GetConstructor(new Type[] { })?.Invoke(new object[]{});
        foreach (var fields in type.GetFields())
        {
            fields.SetValue(copy, fields.GetValue(source));
        }
        return (T)copy;
    }

    public static async Task Interpolate(
        float duration,
        Action<float> onStep,
        Action onCompleted = null, 
        Func<float, float> curve = null)
    {
        curve ??= x => x; 
        for (float f = 0; f < duration; f += Time.deltaTime)
        {
            onStep?.Invoke(curve(f / duration));
            await Task.Yield();
        }

        onStep?.Invoke(1.0f);
        onCompleted?.Invoke();
    }
}