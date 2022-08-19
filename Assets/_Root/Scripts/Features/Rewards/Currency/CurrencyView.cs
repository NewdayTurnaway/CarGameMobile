using UnityEngine;

namespace Features.Rewards.Currency
{
    internal sealed class CurrencyView : MonoBehaviour
    {
        [field: SerializeField] public RectTransform CurrencyContainer { get; private set; }
        [field: SerializeField] public CurrencySlotView CurrencyPrefab { get; private set; }
    }
}
