using DG.Tweening;
using Profile;
using System;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal sealed class PopupController : BaseController
    {
        private readonly ResourcePath _resourcePath = new(Constants.PrefabPaths.Menu.POPUP);
        private readonly ProfilePlayer _profilePlayer;
        private readonly PopupView _view;

        public PopupController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            Subscribe(_view);
        }

        protected override void OnDispose() =>
            Unsubscribe(_view);

        public void ShowPopup() =>
            PlayAnimation(_view.ShowSize, _view.EndAlpha, _view.Duration, 
                onStart: ActivatePopup);


        public void HidePopup() =>
            PlayAnimation(_view.HideSize, _view.StartAlpha, _view.Duration,
                onFinish: DeactivatePopup);

        private void ActivatePopup() =>
            _view.gameObject.SetActive(true);
        private void DeactivatePopup() =>
            _view.gameObject.SetActive(false);


        private void PlayAnimation(Vector3 targetScale, float alpha, float duration,
            Action onStart = null, Action onFinish = null)
        {
            onStart?.Invoke();

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_view.ButtonsRectTransform.DOScale(targetScale, duration));
            sequence.Insert(0, _view.Background.DOFade(alpha, duration));
            sequence.OnComplete(
                () => onFinish?.Invoke());
        }

        private PopupView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PopupView>();
        }

        private void Subscribe(PopupView view)
        {
            view.ClosePopupButton.onClick.AddListener(HidePopup);
            view.MainMenuButton.onClick.AddListener(MainMenu);
        }

        private void Unsubscribe(PopupView view)
        {
            view.ClosePopupButton.onClick.RemoveListener(HidePopup);
            view.MainMenuButton.onClick.RemoveListener(MainMenu);
        }

        private void MainMenu() =>
            _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
