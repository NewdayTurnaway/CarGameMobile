using UnityEngine;

namespace Tool
{
    internal sealed class DontDestroyOnLoadObject : MonoBehaviour
    {
        private void Awake()
        {
            if (enabled)
                DontDestroyOnLoad(gameObject);
        }
    }
}
