using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tool.Tween
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RectTransform))]
    internal sealed class AnimationButtonComponent : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _rectTransform;

        [Header("Settings")]
        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;
        [SerializeField] private LoopType _loopType = LoopType.Restart;
        [Min(-1)]
        [SerializeField] private int _loops = 0;

        private Tweener _tweener;

        private void OnValidate() => InitComponents();
        private void Awake() => InitComponents();

        private void Start() => _button.onClick.AddListener(OnButtonClick);
        private void OnDestroy() => _button.onClick.RemoveAllListeners();

        private void InitComponents()
        {
            _button ??= GetComponent<Button>();
            _rectTransform ??= GetComponent<RectTransform>();
        }

        private void OnButtonClick() =>
            ActivateAnimation();

        [ContextMenu(nameof(ActivateAnimation))]
        public void ActivateAnimation()
        {
            StopAnimation();

            _tweener = _animationButtonType switch
            {
                AnimationButtonType.ChangeRotation => _rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength),
                AnimationButtonType.ChangePosition => _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength),
                _ => default,
            };

            _tweener.SetEase(_curveEase).SetLoops(_loops, _loopType);
        }

        [ContextMenu(nameof(StopAnimation))]
        public void StopAnimation()
        {
            _rectTransform.DOKill(_rectTransform);
        }
    }
}
