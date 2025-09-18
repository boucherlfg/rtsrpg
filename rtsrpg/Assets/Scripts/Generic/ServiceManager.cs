using System.Collections.Generic;

namespace Generic
{
    public class ServiceManager : Singleton<ServiceManager>
    {
        private readonly List<object> _services = new ();

        public T Get<T>(System.Func<T> generator = null) where T : class, new()
        {
            generator ??= () => new T();
            var service = _services.Find(x => x is T);
            if (service != null)
            {
                return (T)service;
            }

            service = generator();
            _services.Add(service);
            return (T)service;
        }

        ~ServiceManager()
        {
            _services.Clear();
        }
    }
}