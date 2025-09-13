using System.Collections;
using System.Collections.Generic;

namespace Generic
{
    public class StaticDict<T> : IEnumerable<T>
    {
        private T[] _values;
        public StaticDict(int capacity)
        {
            _values = new T[capacity];
        }

        public T this[int index]
        {
            get => _values[index];
            set => _values[index] = value;
        }

        public bool TryAdd(int index, T value)
        {
            if (_values[index] != null) return false;
            _values[index] = value;
            return true;
        }

        public bool TryGetValue(int index, out T value)
        {
            value = _values[index];
            return value != null;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }
    }
}