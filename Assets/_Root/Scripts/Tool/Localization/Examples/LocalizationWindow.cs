using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

namespace Tool.Localization.Examples
{
    internal abstract class LocalizationWindow : MonoBehaviour
    {
        [Header("Scene Components")]
        [SerializeField] private Button _englishButton;
        [SerializeField] private Button _frenchButton;
        [SerializeField] private Button _russianButton;


        private void Start()
        {
            _englishButton.onClick.AddListener(() => ChangeLanguage(Language.En));
            _frenchButton.onClick.AddListener(() => ChangeLanguage(Language.Fr));
            _russianButton.onClick.AddListener(() => ChangeLanguage(Language.Ru));
            OnStarted();
        }

        private void OnDestroy()
        {
            _englishButton.onClick.RemoveAllListeners();
            _frenchButton.onClick.RemoveAllListeners();
            _russianButton.onClick.RemoveAllListeners();
            OnDestroyed();
        }

        protected virtual void OnStarted() { }
        protected virtual void OnDestroyed() { }

        private void ChangeLanguage(Language language) =>
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)language];
    }
}
