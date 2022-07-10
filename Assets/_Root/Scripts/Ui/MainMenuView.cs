using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal sealed class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonShed;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonRewardedAd;
        [SerializeField] private Button _buttonBuyItem;

        public void Init(UnityAction startGame, UnityAction openShed, UnityAction settings, UnityAction rewardedAd, UnityAction buyItem)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonShed.onClick.AddListener(openShed);
            _buttonSettings.onClick.AddListener(settings);
            _buttonRewardedAd.onClick.AddListener(rewardedAd);
            _buttonBuyItem.onClick.AddListener(buyItem);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonShed.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonRewardedAd.onClick.RemoveAllListeners();
            _buttonBuyItem.onClick.RemoveAllListeners();
        }
    }
}