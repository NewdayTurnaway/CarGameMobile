using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    internal sealed class PopupView : MonoBehaviour
    {
        [field: SerializeField] public Button ClosePopupButton { get; private set; }
        [field: SerializeField] public Button MainMenuButton { get; private set; }
        [field: SerializeField] public Image Background { get; private set; }
        [field: SerializeField] public RectTransform ButtonsRectTransform { get ; private set; }


        [field: Header("Settings")]
        [field: SerializeField] public Vector3 ShowSize { get; private set; } = Vector3.one;
        [field: SerializeField] public Vector3 HideSize { get; private set; } = Vector3.zero;
        [field: SerializeField] public float StartAlpha { get; private set; } = 0f;
        [field: SerializeField] public float EndAlpha { get; private set; } = 0.5f;
        [field: SerializeField] public float Duration { get; private set; } = 0.3f;
    }
}
