using Profile;
using Tool;
using Tool.Localization;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Object = UnityEngine.Object;

namespace Ui
{
    internal sealed class SettingsMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new(Constants.PrefabPaths.Menu.SETTINGS);
        private readonly ProfilePlayer _profilePlayer;
        private readonly SettingsMenuView _view;

        public SettingsMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(SetEnLanguage, SetRuLanguage, ReturnToMenu);
        }

        private SettingsMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<SettingsMenuView>();
        }

        private void ChangeLanguage(Language language) =>
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)language];

        private void SetEnLanguage() => 
            ChangeLanguage(Language.En);

        private void SetRuLanguage() =>
            ChangeLanguage(Language.Ru);

        private void ReturnToMenu() => 
            _profilePlayer.CurrentState.Value = GameState.Start;
    }
}