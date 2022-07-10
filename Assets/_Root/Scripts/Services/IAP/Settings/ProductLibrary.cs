using UnityEngine;

namespace Services.IAP
{
    [CreateAssetMenu(fileName = nameof(ProductLibrary), menuName = "Settings/IAP/" + nameof(ProductLibrary))]
    internal sealed class ProductLibrary : ScriptableObject
    {
        [field: SerializeField] public Product[] Products { get; private set; }
    }
}
