static class Constants
{
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

    public struct Variables
    {
        public const float SPEED_CAR = 15f;
    }

    public struct PrefabPaths
    {
        public struct EndlessMove
        {
            public const string KEYBOARD_INPUT = "Prefabs/Input/EndlessMoveKeyboardInput";
            public const string ACCELERATION_INPUT = "Prefabs/Input/EndlessMoveAccelerationInput";
        }

        public struct Menu
        {
            public const string MAIN = "Prefabs/UI/MainMenu";
            public const string SETTINGS = "Prefabs/UI/SettingsMenu";
        }

        public const string CAR = "Prefabs/Car";
        public const string BACKGROUND = "Prefabs/Background";
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
}
