using System.Collections;
using System.Collections.Generic;

namespace Generic
{
    public class StaticDict<T> : IEnumerable<(int index, T value)>
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
        public IEnumerator<(int index, T value)> GetEnumerator()
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if(_values[i] != null) yield return (i, _values[i]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if(_values[i] != null) yield return (i, _values[i]);
            }
        }
    }
}