using System.Collections.Generic;
using UnityEngine;

namespace Features.Rewards.Resource
{
    [CreateAssetMenu(fileName = nameof(ResourceCollection), menuName = Constants.Configs.MENU_PATH + nameof(ResourceCollection))]
    internal sealed class ResourceCollection : ScriptableObject
    {
        [field: SerializeField] public List<ResourceConfig> Resources { get; private set; }
    }
}
