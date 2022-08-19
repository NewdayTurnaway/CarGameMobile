using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal sealed class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonReturnMenu;

        public void Init(UnityAction returnMenu)
        {
            _buttonReturnMenu.onClick.AddListener(returnMenu);
        }

        public void OnDestroy()
        {
            _buttonReturnMenu.onClick.RemoveAllListeners();
        }
    }
}