using System;
using System.Linq;
using Features.Shed.Upgrade;
using Features.Inventory.Items;
using Features.AbilitySystem.Abilities;
using Features.Rewards.Resource;
using Features.Rewards;

namespace Tool
{
    internal sealed class ContentDataSourceLoader
    {
        public static ItemConfig[] LoadItemConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<ItemConfigDataSource>(resourcePath);
            return dataSource == null ? Array.Empty<ItemConfig>() : dataSource.ItemConfigs.ToArray();
        }

        public static UpgradeItemConfig[] LoadUpgradeItemConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<UpgradeItemConfigDataSource>(resourcePath);
            return dataSource == null ? Array.Empty<UpgradeItemConfig>() : dataSource.ItemConfigs.ToArray();
        }

        public static AbilityItemConfig[] LoadAbilityItemConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<AbilityItemConfigDataSource>(resourcePath);
            return dataSource == null ? Array.Empty<AbilityItemConfig>() : dataSource.AbilityConfigs.ToArray();
        }

        public static ResourceConfig[] LoadResourceConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<ResourceCollection>(resourcePath);
            return dataSource == null ? Array.Empty<ResourceConfig>() : dataSource.Resources.ToArray();
        }

        public static RewardCollection LoadRewardCollection(ResourcePath resourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<RewardCollection>(resourcePath);
            return dataSource == null ? null : dataSource;
        }
    }
}
