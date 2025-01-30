using System.Collections.Generic;

public static class EventNames
{
    public const string TimeChanged = "TimeChanged";
}

public delegate void EventHandler(params object[] args);

public class EventManager : Singleton<EventManager>
{
    private readonly Dictionary<string, List<EventHandler>> _events = new();

    public void Register(string eventName, EventHandler action)
    {
        if(!_events.ContainsKey(eventName)) _events[eventName] = new List<EventHandler>();
        _events[eventName].Add(action);
    }

    public void UnRegister(string eventName, EventHandler action)
    {
        if (!_events.TryGetValue(eventName, out var evt)) throw new System.NullReferenceException("Event not registered");
        evt.Remove(action);
    }

    public void Notify(string eventName, params object[] args)
    {
        if(!_events.TryGetValue(eventName, out var evt)) throw new System.NullReferenceException("Event not registered");
        evt.ForEach(action => action?.Invoke(args));
    }
}