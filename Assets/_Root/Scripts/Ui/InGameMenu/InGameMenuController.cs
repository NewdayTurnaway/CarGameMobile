using Profile;
using Tool;
using UnityEngine;

namespace Ui
{
    internal sealed class InGameMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new(Constants.PrefabPaths.Menu.IN_GAME);
        private readonly InGameMenuView _view;
        private readonly PopupController _popupController;

        public InGameMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _view = LoadView(placeForUi);
            _popupController = CreatePopupController(placeForUi, profilePlayer);

            Subscribe(_view);
        }

        protected override void OnDispose() => 
            Unsubscribe(_view);

        private InGameMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<InGameMenuView>();
        }

        private PopupController CreatePopupController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            PopupController popupController = new(placeForUi, profilePlayer);
            AddController(popupController);

            return popupController;
        }

        private void Subscribe(InGameMenuView view) => 
            view.InGameMenuButton.onClick.AddListener(ShowPopup);

        private void Unsubscribe(InGameMenuView view) => 
            view.InGameMenuButton.onClick.RemoveListener(ShowPopup);

        private void ShowPopup() =>
            _popupController.ShowPopup();
    }
}
