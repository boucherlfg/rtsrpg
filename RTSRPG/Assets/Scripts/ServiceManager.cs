using System;
using System.Collections.Generic;
using System.Linq;

public class ServiceManager : Singleton<ServiceManager>
{
    private readonly List<object> _services = new List<object>();

    public T Get<T>(params object[] args)
    {
        var srv = _services.Find(x => x is T);
        if (srv != null) return (T)srv;
        
        srv = DefaultGenerator<T>(args);
        _services.Add(srv);
        return (T)srv;
    }

    private T DefaultGenerator<T>(params object[] args)
    {
        var type = typeof(T);
        var ctors = type.GetConstructors();

        foreach (var ctor in ctors)
        {
            var parameters = ctor.GetParameters();
            if (parameters.Length != args.Length) continue;

            var isRightCtor = true;
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var arg = args[i];
                
                var paramType = parameter.ParameterType;
                var argType = arg.GetType();
                if (paramType == argType || argType.IsSubclassOf(paramType))
                {
                    continue;
                }

                isRightCtor = false;
                break;
            }

            if (!isRightCtor) continue;

            return (T)ctor.Invoke(args);
        }
        throw new MissingMethodException($"No matching constructor found for {type}");
    }
}