using Features.Rewards.Currency;
using System;

namespace Features.Rewards
{
    internal sealed class RewardsStateController
    {
        private readonly RewardsView _view;
        private readonly RewardsInfo _rewardsInfo;
        private readonly CurrencyController _currencyController;

        public RewardsStateController(RewardsView view, RewardsInfo rewardsInfo, CurrencyController currencyController)
        {
            _view = view;
            _rewardsInfo = rewardsInfo;
            _currencyController = currencyController;
        }

        public bool IsGetReward { get; private set; }

        public void RefreshRewardsState()
        {
            bool gotRewardEarlier = _view.TimeGetReward.HasValue;
            if (!gotRewardEarlier)
            {
                IsGetReward = true;
                return;
            }

            TimeSpan timeFromLastRewardGetting =
                DateTime.UtcNow - _view.TimeGetReward.Value;

            bool isDeadlineElapsed =
                timeFromLastRewardGetting.Seconds >= _rewardsInfo.TimeDeadline;

            bool isTimeToGetNewReward =
                timeFromLastRewardGetting.Seconds >= _rewardsInfo.TimeCooldown;

            if (isDeadlineElapsed)
                ResetRewardsState();

            IsGetReward = isTimeToGetNewReward;
        }

        public void ClaimReward()
        {
            if (!IsGetReward)
                return;

            RewardConfig reward = _rewardsInfo.Rewards[_view.CurrentSlotInActive];
            _currencyController.AddResource(reward.ResourceType, reward.CountCurrency);

            _view.TimeGetReward = DateTime.UtcNow;
            _view.CurrentSlotInActive++;

            RefreshRewardsState();
        }

        public void ResetRewardsState()
        {
            _view.TimeGetReward = null;
            _view.CurrentSlotInActive = 0;
        }
    }
}