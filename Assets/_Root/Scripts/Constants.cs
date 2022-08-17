static class Constants
{
    public struct Inputs
    {
        public const string HORIZONTAL = "Horizontal";
    }

    public struct Names
    {
        public struct Iap
        {
            public const string PRODUCT_1 = "product_1";
            public const string PRODUCT_2 = "product_2";
            public const string PRODUCT_3 = "product_3";
            public const string SUBSCIBTION_1 = "subscription_1";
        }
    }

    public struct PrefabPaths
    {
        public struct Input
        {
            public const string KEYBOARD_INPUT = "Prefabs/Input/KeyboardInput";
            public const string ACCELERATION_INPUT = "Prefabs/Input/AccelerationInput";
        }

        public struct Menu
        {
            public const string MAIN = "Prefabs/UI/MainMenu";
            public const string SETTINGS = "Prefabs/UI/SettingsMenu";
        }

        public struct Ui
        {
            public const string ABILITIES = "Prefabs/Ability/AbilitiesView";
            public const string INVENTORY = "Prefabs/Inventory/InventoryView";
            public const string SHED = "Prefabs/Shed/ShedView";
            public const string FIGHT_VIEW = "Prefabs/Fight/FightView";
            public const string START_FIGHT_VIEW = "Prefabs/Fight/StartFightView";
            public const string CURRENCY_VIEW = "Prefabs/Rewards/CurrencyView";
            public const string REWARD_VIEW = "Prefabs/Rewards/RewardView";
        }

        public const string CAR = "Prefabs/Car";
        public const string BACKGROUND = "Prefabs/Background";
        
    }

    public struct Configs
    {
        public const string MENU_PATH = "Configs/";
        public const string ITEM = "Configs/Inventory/ItemConfigDataSource";
        public const string ABILITY_ITEM = "Configs/Ability/AbilityItemConfigDataSource";
        public const string UPGRADE_ITEM = "Configs/Shed/UpgradeItemConfigDataSource";
        public const string RESOURCE_COLLECTION = "Configs/Rewards/ResourceCollection";
        public const string REWARD_COLLECTION = "Configs/Rewards/WeeklyRewards";
    }

    public struct Settings
    {
        public struct Ads
        {
            public const string UNITY_ADS = "Settings/Ads/UnityAdsSettings";
        }

        public struct Iap
        {
            public const string UNITY_IAP = "Settings/IAP/ProductLibrary";
        }
    }

    public struct Text
    {
        public const string DAY = "Day";
        public const string WEEK = "Week";
        public const string REWARD_READY = "The reward is ready to be received!";
        public const string NEXT_TIME = "Time to get the next reward:";
    }
}
