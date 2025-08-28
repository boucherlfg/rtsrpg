using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> 
{
    public static T Instance
    {
        get; 
        private set; 
    }
    protected virtual void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            Debug.LogWarning($"There should be more than one instance of {typeof(T).Name}");
            return;
        }
        
        Instance = this as T;
    }
}
