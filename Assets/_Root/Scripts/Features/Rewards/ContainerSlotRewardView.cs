using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Rewards
{
    internal sealed class ContainerSlotRewardView : MonoBehaviour
    {
        [SerializeField] private Image _originalBackground;
        [SerializeField] private Image _selectBackground;
        [SerializeField] private Image _iconCurrency;
        [SerializeField] private TMP_Text _textDays;
        [SerializeField] private TMP_Text _countReward;


        public void SetData(RewardType type, RewardConfig reward, int countCooldownPeriods, bool isSelected)
        {
            _iconCurrency.sprite = reward.ResourceIcon;

            string text = type switch
            {
                RewardType.Daily => Constants.Text.DAY,
                RewardType.Weekly => Constants.Text.WEEK,
                _ => string.Empty,
            };
            _textDays.text = $"{text} {countCooldownPeriods}";

            _countReward.text = reward.CountCurrency.ToString();

            UpdateBackground(isSelected);
        }

        private void UpdateBackground(bool isSelect)
        {
            _originalBackground.gameObject.SetActive(!isSelect);
            _selectBackground.gameObject.SetActive(isSelect);
        }
    }
}
