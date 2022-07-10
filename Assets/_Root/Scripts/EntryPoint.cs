using Profile;
using UnityEngine;
using Services.IAP;
using Services.Ads.UnityAds;
using Services.Analytics;

internal sealed class EntryPoint : MonoBehaviour
{
    private const GameState INITIAL_STATE = GameState.Start;

    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;

    private void Start()
    {
        ProfilePlayer profilePlayer = new(Constants.Variables.SPEED_CAR, Constants.Variables.JUMP_HEIGHT, INITIAL_STATE);
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
