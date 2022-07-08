using UnityEngine;

namespace Tool
{
    internal static class ResourcesLoader
    {
        public static Sprite LoadSprite(ResourcePath path) =>
            Resources.Load<Sprite>(path.PathResource);

        public static GameObject LoadPrefab(ResourcePath path) =>
            Resources.Load<GameObject>(path.PathResource);

        public static T Load<T>(ResourcePath path) where T : Object =>
            Resources.Load<T>(path.PathResource);
    }
}
