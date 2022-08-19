using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal sealed class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private Button _returnMenuButton;
        
        [Header("Language")]
        [SerializeField] private Button _enLanguageButton;
        [SerializeField] private Button _ruLanguageButton;

        public void Init(UnityAction enLanguange, UnityAction ruLanguange, UnityAction returnMenu)
        {
            _enLanguageButton.onClick.AddListener(enLanguange);
            _ruLanguageButton.onClick.AddListener(ruLanguange);
            _returnMenuButton.onClick.AddListener(returnMenu);
        }

        public void OnDestroy()
        {
            _enLanguageButton.onClick.RemoveAllListeners();
            _ruLanguageButton.onClick.RemoveAllListeners();
            _returnMenuButton.onClick.RemoveAllListeners();
        }
    }
}