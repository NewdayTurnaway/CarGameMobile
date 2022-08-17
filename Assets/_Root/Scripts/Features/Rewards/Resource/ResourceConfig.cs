using UnityEngine;

namespace Features.Rewards.Resource
{
    [CreateAssetMenu(fileName = nameof(ResourceConfig), menuName = Constants.Configs.MENU_PATH + nameof(ResourceConfig))]
    internal sealed class ResourceConfig : ScriptableObject
    {
        [field: SerializeField] public ResourceType Type { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
