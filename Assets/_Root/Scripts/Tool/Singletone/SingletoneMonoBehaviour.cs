using UnityEngine;

namespace Tool
{
    internal abstract class SingletoneMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        private static bool _exist = true;
        private static readonly object _lock = new();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance != null)
                        return _instance;

                    T[] instances = FindObjectsOfType<T>();

                    if (instances != null)
                    {
                        if (instances.Length == 1)
                        {
                            _instance = instances[0];
                            DontDestroyOnLoad(_instance);

                            return _instance;
                        }

                        if(instances.Length > 1)
                        {
                            for (int i = 0; i < instances.Length; ++i)
                            {
                                T tempInstance = instances[i];
                                Destroy(tempInstance);
                            }
                        }
                    }

                    GameObject newGameObject = new(typeof(T).Name, typeof(T));
                    _instance = newGameObject.GetComponent<T>();
                    DontDestroyOnLoad(_instance);
                    return _instance;
                }
            }
        }

        public static T Value { get { return _instance; } }

        public static bool IsExist
        {
            get
            {
                if (_instance == null)
                    return false;

                return _exist;
            }
        }

        private void OnEnable()
        {
            this.Log($"IsExist: {IsExist}");
        }

        protected void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            _instance = this as T;
            Init();
        }

        protected virtual void Init() { }

        protected void OnDestroy() => 
            _exist = false;

        protected void OnApplicationQuit() => 
            _exist = false;
    } 
}
