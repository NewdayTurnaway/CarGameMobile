using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Rewards
{
    internal sealed class RewardsView : MonoBehaviour
    {
        [Header("Settings PlayerPrefs Keys")]
        [SerializeField] private string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);
        [SerializeField] private string TimeGetRewardKey = nameof(TimeGetRewardKey);

        [field: Header("Ui Elements")]
        [field: SerializeField] public TMP_Text TimerNewReward { get; private set; }
        [field: SerializeField] public RectTransform SlotsContainer { get; private set; }
        [field: SerializeField] public ContainerSlotRewardView SlotPrefab { get; private set; }
        [field: SerializeField] public Button GetRewardButton { get; private set; }
        [field: SerializeField] public Button ResetButton { get; private set; }
        [field: SerializeField] public Button ReturnButton { get; private set; }

        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CurrentSlotInActiveKey);
            set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
        }

        public DateTime? TimeGetReward
        {
            get
            {
                string data = PlayerPrefs.GetString(TimeGetRewardKey);
                return !string.IsNullOrEmpty(data) ? DateTime.Parse(data) : null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeGetRewardKey);
            }
        }
    }
}
