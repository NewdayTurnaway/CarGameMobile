using UnityEngine;

namespace Tool
{
    internal abstract class SingletoneMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        private static bool _exist = true;

        public static T Instance
        {
            get
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

                    DestroyInstances(instances);
                }

                CreateObject();
                return _instance;
            }
        }

        private static void DestroyInstances(T[] instances)
        {
            if (instances.Length > 1)
            {
                for (int i = 0; i < instances.Length; ++i)
                {
                    T tempInstance = instances[i];
                    Destroy(tempInstance);
                }
            }
        }

        private static void CreateObject()
        {
            GameObject newGameObject = new(typeof(T).Name, typeof(T));
            _instance = newGameObject.GetComponent<T>();
            DontDestroyOnLoad(_instance);
        }

        public static T Value { get { return _instance; } }

        private void OnEnable()
        {
            this.Log($"IsExist: {_exist}");
        }

        private void Awake()
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

        private void OnDestroy() => 
            _exist = false;

        private void OnApplicationQuit() => 
            _exist = false;
    } 
}
