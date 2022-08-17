using Profile;
using System;
using System.Collections.Generic;

namespace Features.Rewards
{
    internal sealed class RewardsUiController
    {
        private readonly RewardsView _view;
        private readonly RewardsInfo _rewardsInfo;
        private readonly ProfilePlayer _profilePlayer;

        private readonly List<ContainerSlotRewardView> _slots = new();

        private readonly RewardsStateController _rewardsStateController;
        private readonly RewardsUiButtonsController _uiButtonsController;

        public RewardsUiController(RewardsView view, RewardsInfo rewardsInfo,
            RewardsStateController rewardsStateController, List<ContainerSlotRewardView> slots, ProfilePlayer profilePlayer)
        {
            _view = view;
            _rewardsInfo = rewardsInfo;
            _rewardsStateController = rewardsStateController;
            _slots = slots;
            _profilePlayer = profilePlayer;

            _uiButtonsController = new(_view, _rewardsStateController, _profilePlayer);
        }

        public void Init()
        {
            RefreshUi();
            _uiButtonsController.SubscribeButtons();
        }

        public void Deinit() => 
            _uiButtonsController.UnsubscribeButtons();

        public void RefreshUi()
        {
            _uiButtonsController.RefreshButtonState();
            _view.TimerNewReward.text = GetTimerNewRewardText();
            RefreshSlots();
        }

        private string GetTimerNewRewardText()
        {
            if (_rewardsStateController.IsGetReward)
                return Constants.Text.REWARD_READY;

            if (_view.TimeGetReward.HasValue)
            {
                DateTime nextClaimTime = _view.TimeGetReward.Value.AddSeconds(_rewardsInfo.TimeCooldown);
                TimeSpan currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

                string timeGetReward =
                    $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:" +
                    $"{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

                return $"{Constants.Text.NEXT_TIME} {timeGetReward}";
            }

            return string.Empty;
        }

        private void RefreshSlots()
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                int countCooldownPeriods = i + 1;
                bool isSelected = i == _view.CurrentSlotInActive;

                _slots[i].SetData(_rewardsInfo.RewardType, _rewardsInfo.Rewards[i], countCooldownPeriods, isSelected);
            }
        }
    }
}
