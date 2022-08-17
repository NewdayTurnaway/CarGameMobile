using Profile;

namespace Features.Rewards
{
    internal sealed class RewardsUiButtonsController
    {
        private readonly RewardsView _view;
        private readonly RewardsStateController _rewardsStateController;
        private readonly ProfilePlayer _profilePlayer;

        public RewardsUiButtonsController(RewardsView view, RewardsStateController rewardsStateController, ProfilePlayer profilePlayer)
        {
            _view = view;
            _rewardsStateController = rewardsStateController;
            _profilePlayer = profilePlayer;
        }

        public void SubscribeButtons()
        {
            _view.GetRewardButton.onClick.AddListener(ClaimReward);
            _view.ResetButton.onClick.AddListener(ResetRewardsState);
            _view.ReturnButton.onClick.AddListener(Close);
        }

        public void UnsubscribeButtons()
        {
            _view.GetRewardButton.onClick.RemoveListener(ClaimReward);
            _view.ResetButton.onClick.RemoveListener(ResetRewardsState);
            _view.ReturnButton.onClick.RemoveListener(Close);
        }

        public void RefreshButtonState() => 
            _view.GetRewardButton.interactable = _rewardsStateController.IsGetReward;

        private void ClaimReward() =>
            _rewardsStateController.ClaimReward();

        private void ResetRewardsState() =>
            _rewardsStateController.ResetRewardsState();

        private void Close() =>
            _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
