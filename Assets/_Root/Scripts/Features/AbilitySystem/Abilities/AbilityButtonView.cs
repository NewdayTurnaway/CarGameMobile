using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

namespace Features.AbilitySystem.Abilities
{
    internal interface IAbilityButtonView
    {
        void Init(Sprite icon, UnityAction click);
        void Deinit();
    }

    internal sealed class AbilityButtonView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;

        [Header("Settings Animation")]
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;
        [SerializeField] private LoopType _loopType = LoopType.Restart;
        [Min(-1)]
        [SerializeField] private int _loops = 0;


        private void OnDestroy() => Deinit();


        public void Init(Sprite icon, UnityAction click)
        {
            _icon.sprite = icon;
            _button.onClick.AddListener(click);
        }

        public void Deinit()
        {
            _icon.sprite = null;
            _button.onClick.RemoveAllListeners();
        }

        public void ActivateAnimation()
        {
            _rectTransform.DOKill(_rectTransform);
            _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase).SetLoops(_loops, _loopType);
        }
    }
}
