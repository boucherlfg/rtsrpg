using System;

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
}