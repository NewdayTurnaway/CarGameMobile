using Profile;
using UnityEngine;
using Services.IAP;
using Services.Ads.UnityAds;
using Services.Analytics;

internal sealed class EntryPoint : MonoBehaviour
{
    [Header("Initial Settings")]
    [SerializeField] private float _speedCar;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private GameState _initialState;

    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;

    private void Start()
    {
        ProfilePlayer profilePlayer = new(_speedCar, _jumpHeight, _initialState);
        _mainController = new(_placeForUi, profilePlayer);

        AnalyticsManager.Instance.MainMenuOpened();

        if (UnityAdsService.Instance.IsInitialized)
            OnAdsInitialized();
        else UnityAdsService.Instance.Initialized.AddListener(OnAdsInitialized);

        if (IAPService.Instance.IsInitialized) 
            OnIapInitialized();
        else IAPService.Instance.Initialized.AddListener(OnIapInitialized);
    }

    private void OnDestroy()
    {
        UnityAdsService.Value.Initialized.RemoveListener(OnAdsInitialized);
        IAPService.Value.Initialized.RemoveListener(OnIapInitialized);
        _mainController.Dispose();
    }

    private void OnAdsInitialized() => UnityAdsService.Instance.InterstitialPlayer.Play();
    private void OnIapInitialized() => IAPService.Instance.Buy(Constants.Names.Iap.PRODUCT_1);
}
