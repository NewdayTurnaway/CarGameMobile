using UnityEngine;

namespace Tool
{
    static class Logging
    {
        public static void Log(this object obj, string message)
        {
            Debug.Log(WrapMessage(obj, message));
        }

        public static void Error(this object obj, string message)
        {
            Debug.LogError($"<color=#B31321>[!]</color> " + WrapMessage(obj, message));
        }

        private static string WrapMessage(object obj, string message)
        {
            return $"<color=#1E7F1E>[{obj.GetType().Name}]</color> {message}";
        }
    }
}
