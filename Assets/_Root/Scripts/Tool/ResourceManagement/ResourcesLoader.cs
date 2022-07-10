using UnityEngine;

namespace Tool
{
    internal static class ResourcesLoader
    {
        public static Sprite LoadSprite(ResourcePath path) =>
            LoadObject<Sprite>(path);

        public static GameObject LoadPrefab(ResourcePath path) =>
            LoadObject<GameObject>(path);

        public static T LoadObject<T>(ResourcePath path) where T : Object =>
            Resources.Load<T>(path.PathResource);
    }
}
