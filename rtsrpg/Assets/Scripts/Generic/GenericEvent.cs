
[System.Serializable]
public class GenericEvent
{
    private event System.Action Evt;
    
    public void Subscribe(System.Action action) => Evt += action;
    
    public void Unsubscribe(System.Action action) => Evt -= action;
    
    public void Invoke() => Evt?.Invoke();
}

public class GenericEvent<T> : GenericEvent
{
    private event System.Action<T> Evt;
    public void Subscribe(System.Action<T> action) => Evt += action;
    public void Unsubscribe(System.Action<T> action) => Evt -= action;
    public void Invoke(T arg) => Evt?.Invoke(arg);
}