using UnityEngine;

namespace Generic
{
    [System.Serializable]
    public class Observed<T>
    {
        public GenericEvent<T> Changed = new();
        [SerializeField] private T value;

        public T Value
        {
            get => value;
            set
            {
                if (value != null && value.Equals(this.value)) return;
                this.value = value;
                Changed?.Invoke(value);
            }
        }

        public static implicit operator T(Observed<T> observed) => observed.Value;
    }

    public class Observed
    {
        public GenericEvent Changed = new();
    }
}