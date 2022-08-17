using Features.Rewards.Currency;
using Profile;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

namespace Features.Rewards
{
    internal sealed class RewardsController : BaseController
    {
        private readonly ResourcePath _resourcePath = new(Constants.PrefabPaths.Ui.REWARD_VIEW);
        private readonly ResourcePath _dataSourcePath = new(Constants.Configs.REWARD_COLLECTION);

        private readonly RewardsView _view;
        private readonly CurrencyController _currencyController;

        private readonly List<ContainerSlotRewardView> _slots = new();
        private Coroutine _coroutine;

        private bool _isInitialized;

        private readonly RewardSlotPanel _rewardSlotPanel;
        private readonly RewardsStateController _rewardsStateController;
        private readonly RewardsUiController _uiController;


        public RewardsController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _view = LoadView(placeForUi);
            RewardCollection rewardsData = ContentDataSourceLoader.LoadRewardCollection(_dataSourcePath);

            _currencyController = CreateCurrencyController(profilePlayer.Currency, placeForUi);

            RewardsInfo rewardsInfo = new(rewardsData);
            _rewardSlotPanel = new(_view, rewardsInfo, _slots);
            _rewardsStateController = new(_view, rewardsInfo, _currencyController);
            _uiController = new(_view, rewardsInfo, _rewardsStateController, _slots, profilePlayer);

            Init();
        }

        protected override void OnDispose()
        {
            Deinit();
            base.OnDispose();
        }

        private RewardsView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<RewardsView>();
        }

        private CurrencyController CreateCurrencyController(CurrencyModel currencyModel, Transform placeForUi)
        {
            var currencyController = new CurrencyController(currencyModel, placeForUi);
            AddController(currencyController);

            return currencyController;
        }

        private void Init()
        {
            if (_isInitialized)
                return;

            _rewardSlotPanel.InitSlots();
            _uiController.Init();
            StartRewardsUpdating();

            _isInitialized = true;
        }

        private void Deinit()
        {
            if (!_isInitialized)
                return;

            _rewardSlotPanel.DeinitSlots();
            StopRewardsUpdating();
            _uiController.Deinit();

            _isInitialized = false;
        }

        private void StartRewardsUpdating() =>
            _coroutine = _view.StartCoroutine(RewardsStateUpdater());

        private void StopRewardsUpdating()
        {
            if (_coroutine == null)
                return;

            _view.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator RewardsStateUpdater()
        {
            WaitForSeconds waitForSecond = new(1);

            while (true)
            {
                _rewardsStateController.RefreshRewardsState();
                _uiController.RefreshUi();
                _currencyController.CurrencySlotsRefresh();
                yield return waitForSecond;
            }
        }
    }
}
