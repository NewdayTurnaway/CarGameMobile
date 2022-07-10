using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;
using Tool;

namespace Services.Ads.UnityAds
{
    internal sealed class UnityAdsService : SingletoneMonoBehaviour<UnityAdsService>, IUnityAdsInitializationListener, IAdsService
    {
        [Header("Components")]
        [SerializeField] private UnityAdsSettings _settings;

        [field: Header("Events")]
        [field: SerializeField] public UnityEvent Initialized { get; private set; }

        public IAdsPlayer InterstitialPlayer { get; private set; }
        public IAdsPlayer RewardedPlayer { get; private set; }
        public IAdsPlayer BannerPlayer { get; private set; }
        public bool IsInitialized => Advertisement.isInitialized;

        private void OnValidate()
        {
            _settings ??= _settings = ResourcesLoader.Load<UnityAdsSettings>(new ResourcePath(Constants.Settings.Ads.UNITY_ADS));
        }

        protected override void Init()
        {
            _settings ??= _settings = ResourcesLoader.Load<UnityAdsSettings>(new ResourcePath(Constants.Settings.Ads.UNITY_ADS));
            Initialized = new();

            InitializeAds();
            InitializePlayers();
        }

        private void InitializeAds()
        {
            Advertisement.Initialize(
                _settings.GameId,
                _settings.TestMode,
                _settings.EnablePerPlacementMode,
                this);
        }

        private void InitializePlayers()
        {
            InterstitialPlayer = CreateInterstitial();
            RewardedPlayer = CreateRewarded();
            BannerPlayer = CreateBanner();
        }


        private IAdsPlayer CreateInterstitial() =>
            _settings.Interstitial.Enabled
                ? new InterstitialPlayer(_settings.Interstitial.Id)
                : new StubPlayer("");

        private IAdsPlayer CreateRewarded() =>
            _settings.Rewarded.Enabled
                ? new RewardedPlayer(_settings.Rewarded.Id)
                : new StubPlayer("");

        private IAdsPlayer CreateBanner() =>
            new StubPlayer("");


        void IUnityAdsInitializationListener.OnInitializationComplete()
        {
            this.Log("Initialization complete.");
            Initialized?.Invoke();
        }

        void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message) =>
            this.Error($"Initialization Failed: {error} - {message}");
    }
}
