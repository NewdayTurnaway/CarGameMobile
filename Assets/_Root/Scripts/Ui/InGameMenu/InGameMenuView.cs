using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    internal sealed class InGameMenuView : MonoBehaviour
    {
        [field: SerializeField] public Button InGameMenuButton { get; private set; }
    }
}
