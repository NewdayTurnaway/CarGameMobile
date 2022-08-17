using UnityEngine;
using System.Collections.Generic;

namespace Features.Shed.Upgrade
{
    [CreateAssetMenu(
        fileName = nameof(UpgradeItemConfigDataSource),
        menuName = Constants.Configs.MENU_PATH + nameof(UpgradeItemConfigDataSource))]
    internal sealed class UpgradeItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private UpgradeItemConfig[] _itemConfigs;

        public IReadOnlyList<UpgradeItemConfig> ItemConfigs => _itemConfigs;
    }
}
