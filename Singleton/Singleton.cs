using UnityEngine;

namespace LRS.Singleton
{
    /// <summary>
    /// A singleton class that can be inherited from to create a singleton.
    /// </summary>
    /// <typeparam name="T">The class that inherits from this singleton</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Component {
        protected static T instance;
    
        public static bool HasInstance => instance != null;
        public static T TryGetInstance() => HasInstance ? instance : null;

        public static T Instance {
            get {
                if (instance == null) {
                    instance = FindAnyObjectByType<T>();
                    if (instance == null) {
                        GameObject go = new (typeof(T).Name + " Auto-Generated");
                        instance = go.AddComponent<T>();
                    }
                }
            
                return instance;
            }
        }

        /// <summary>
        /// Make sure to call base.Awake() in override if you need awake.
        /// </summary>
        protected virtual void Awake() {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton() {
            if (!Application.isPlaying) return;
        
            if (this != instance) {
                Destroy(gameObject);
            } else {
                instance = this as T;
            }
        }
    }
}
