using System.Collections.Generic;
using UnityEngine;

namespace Features.Rewards
{
    [CreateAssetMenu(fileName = nameof(RewardCollection), menuName = Constants.Configs.MENU_PATH + nameof(RewardCollection))]
    internal sealed class RewardCollection : ScriptableObject
    {
        private const int SECONDS = 60;
        private const int MINUTES = 60;
        private const int HOURS = 24;
        private const int DAYS = 7;

        private int? _timeCooldown;
        private int? _timeDeadline;

        [SerializeField] private int _cooldown = 1;
        [SerializeField] private int _deadlineCoeff = 2;
        [field: SerializeField] public RewardType RewardType { get; private set; }
        [field: SerializeField] public List<RewardConfig> Rewards { get; private set; }

        public int? TimeCooldown
        {
            get
            {
                if (_timeCooldown == null)
                {
                    _timeCooldown = CalculationCooldown(RewardType);
                }
                return _timeCooldown;
            }
        }

        public int? TimeDeadline
        { 
            get
            {
                if (_timeDeadline == null)
                {
                    _timeDeadline = _timeCooldown * _deadlineCoeff;
                }
                return _timeDeadline;
            }
        }

        private int CalculationCooldown(RewardType rewardType)
        {
            int cooldown = _cooldown * SECONDS * MINUTES * HOURS;

            return rewardType switch
            {
                RewardType.Daily => cooldown,
                RewardType.Weekly => cooldown * DAYS,
                _ => 0,
            };
        }
    }
}
