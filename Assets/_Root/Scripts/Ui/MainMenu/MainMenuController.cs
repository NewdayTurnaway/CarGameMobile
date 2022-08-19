using Profile;
using Services.IAP;
using Services.Ads.UnityAds;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal sealed class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new(Constants.PrefabPaths.Menu.MAIN);
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, OpenSettings, OpenShed, PlayRewardedAds, BuyProduct, OpenDailyReward, ExitGame);
            UnityAdsService.Instance.Initialized.AddListener(PlayRewardedAds);
            IAPService.Instance.Initialized.AddListener(BuyProduct);
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void OpenSettings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void OpenShed() =>
            _profilePlayer.CurrentState.Value = GameState.Shed;

        private void PlayRewardedAds() =>
            UnityAdsService.Instance.RewardedPlayer.Play();

        private void BuyProduct() =>
            IAPService.Instance.Buy(Constants.Names.Iap.PRODUCT_2);

        private void OpenDailyReward() =>
            _profilePlayer.CurrentState.Value = GameState.Rewards;

        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}