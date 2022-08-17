using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal sealed class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _shedButton;
        [SerializeField] private Button _adsRewardButton;
        [SerializeField] private Button _buyProductButton;
        [SerializeField] private Button _rewardsButton;
        [SerializeField] private Button _exitButton;

        public void Init(UnityAction startGame, UnityAction openSettings, UnityAction openShed, 
            UnityAction playRewardedAds, UnityAction buyProduct, UnityAction openDailyReward, UnityAction exitGame)
        {
            _startButton.onClick.AddListener(startGame);
            _settingsButton.onClick.AddListener(openSettings);
            _shedButton.onClick.AddListener(openShed);
            _adsRewardButton.onClick.AddListener(playRewardedAds);
            _buyProductButton.onClick.AddListener(buyProduct);
            _rewardsButton.onClick.AddListener(openDailyReward);
            _exitButton.onClick.AddListener(exitGame);
        }

        public void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _shedButton.onClick.RemoveAllListeners();
            _adsRewardButton.onClick.RemoveAllListeners();
            _buyProductButton.onClick.RemoveAllListeners();
            _rewardsButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}