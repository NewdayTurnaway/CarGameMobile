using System.Collections.Generic;

namespace Features.Rewards
{
    internal sealed class RewardsInfo
    {
        public RewardType RewardType { get; private set; }
        public float TimeCooldown { get; private set; }
        public float TimeDeadline { get; private set; }
        public List<RewardConfig> Rewards { get; private set; } = new();

        public RewardsInfo(RewardCollection rewardCollection)
        {
            RewardType = rewardCollection.RewardType;

            TimeCooldown = (int)rewardCollection.TimeCooldown;
            TimeDeadline = (int)rewardCollection.TimeDeadline;
            Rewards = rewardCollection.Rewards;
        }
    } 
}
