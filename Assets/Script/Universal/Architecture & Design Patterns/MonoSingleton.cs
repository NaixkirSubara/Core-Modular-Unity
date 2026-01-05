using UnityEngine;

namespace MyStudio.Core.Architecture
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        [Header("Singleton Settings")]
        [SerializeField] private bool _dontDestroyOnLoad = true;

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        Debug.LogError($"[MonoSingleton] Instance of {typeof(T)} is missing in the scene!");
                        // auto create script dibutuhkan ( opsional)
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogWarning($"[MonoSingleton] Duplicate instance of {typeof(T)} detected. Destroying duplicate.");
                Destroy(gameObject);
                return;
            }

            _instance = (T)this;
            
            if (_dontDestroyOnLoad)
            {
                transform.SetParent(null); 
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}