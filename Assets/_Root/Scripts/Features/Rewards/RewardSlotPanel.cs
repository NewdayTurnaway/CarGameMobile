using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Features.Rewards
{
    internal sealed class RewardSlotPanel
    {
        private readonly RewardsView _view;
        private readonly RewardsInfo _rewardsInfo;

        private readonly List<ContainerSlotRewardView> _slots = new();

        public RewardSlotPanel(RewardsView view, RewardsInfo rewardsInfo, List<ContainerSlotRewardView> slots)
        {
            _view = view;
            _rewardsInfo = rewardsInfo;
            _slots = slots;
        }

        public void InitSlots()
        {
            for (int i = 0; i < _rewardsInfo.Rewards.Count; i++)
            {
                ContainerSlotRewardView instanceSlot = CreateSlotRewardView();
                _slots.Add(instanceSlot);
            }
        }

        public void DeinitSlots()
        {
            foreach (ContainerSlotRewardView slot in _slots)
                Object.Destroy(slot.gameObject);

            _slots.Clear();
        }

        private ContainerSlotRewardView CreateSlotRewardView() =>
            Object.Instantiate
            (
                _view.SlotPrefab,
                _view.SlotsContainer,
                false
            );
    }
}
