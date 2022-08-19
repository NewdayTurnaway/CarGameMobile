using UnityEngine;
using System.Collections.Generic;

namespace Features.AbilitySystem.Abilities
{
    [CreateAssetMenu(
        fileName = nameof(AbilityItemConfigDataSource),
        menuName = Constants.Configs.MENU_PATH + nameof(AbilityItemConfigDataSource))]
    internal sealed class AbilityItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private AbilityItemConfig[] _abilityConfigs;

        public IReadOnlyList<AbilityItemConfig> AbilityConfigs => _abilityConfigs;
    }
}
