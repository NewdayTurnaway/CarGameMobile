using Features.Rewards.Resource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Rewards.Currency
{
    internal sealed class CurrencySlotView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _count;

        public ResourceType Type { get; private set; }

        public void SetInfo(ResourceType type, Sprite icon)
        {
            Type = type;
            _image.sprite = icon;
        }

        public void SetData(int count) =>
            _count.text = count.ToString();
    }
}
