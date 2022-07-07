using Profile;
using UnityEngine;
using Services.IAP;
using Services.Ads.UnityAds;
using Services.Analytics;

internal class EntryPoint : MonoBehaviour
{
    private const GameState INITIAL_STATE = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private IAPService _iapService;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analytics;

    private MainController _mainController;

    private void Start()
    {
        ProfilePlayer profilePlayer = new(Constants.Variables.SPEED_CAR, INITIAL_STATE);
        _mainController = new(_placeForUi, profilePlayer);

        _analytics.SendMainMenuOpened();

        if (_adsService.IsInitialized) OnAdsInitialized();
        else _adsService.Initialized.AddListener(OnAdsInitialized);

        if (_iapService.IsInitialized) OnIapInitialized();
        else _iapService.Initialized.AddListener(OnIapInitialized);
    }

    private void OnDestroy()
    {
        _adsService.Initialized.RemoveListener(OnAdsInitialized);
        _iapService.Initialized.RemoveListener(OnIapInitialized);
        _mainController.Dispose();
    }

    private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
    private void OnIapInitialized() => _iapService.Buy("product_1");
}
