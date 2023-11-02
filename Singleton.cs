using UnityEngine;


/// <summary>
/// A singleton class that can be inherited from to create a singleton.
/// </summary>
/// <typeparam name="T">The class that inherits from this singleton</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindFirstObjectByType<T>();
            if (_instance == null)
            {
                Debug.LogError("An instance of " + typeof(T) +
                               " is needed in the scene, but there is none.");
            }
            return _instance;
        }
    }

    protected bool useDontDestroyOnLoad = false;
    
    public static bool IsInitialized => _instance != null;

    protected virtual void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this as T;
            if (useDontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }
    }
}
