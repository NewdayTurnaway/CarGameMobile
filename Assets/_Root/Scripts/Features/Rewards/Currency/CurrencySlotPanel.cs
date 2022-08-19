using Features.Rewards.Resource;
using System.Collections.Generic;
using Tool;
using Object = UnityEngine.Object;

namespace Features.Rewards.Currency
{
    internal sealed class CurrencySlotPanel
    {
        private readonly CurrencyView _view;

        private readonly ResourceConfig[] _resources;
        private readonly List<CurrencySlotView> _slots = new();

        public CurrencySlotPanel(CurrencyView currencyView, ResourceConfig[] resources, List<CurrencySlotView> slots)
        {
            _view = currencyView;
            _resources = resources;
            _slots = slots;
        }

        public void Init()
        {
            for (int i = 0; i < _resources.Length; i++)
            {
                ResourceType type = _resources[i].Type;
                CurrencySlotView instanceSlot = CreateCurrencySlotView();
                instanceSlot.SetInfo(type, _resources[i].Icon);
                _slots.Add(instanceSlot);
            }
        }

        public void Deinit()
        {
            foreach (CurrencySlotView slot in _slots)
                Object.Destroy(slot.gameObject);

            _slots.Clear();
        }

        private CurrencySlotView CreateCurrencySlotView() =>
            Object.Instantiate
            (
                _view.CurrencyPrefab,
                _view.CurrencyContainer,
                false
            );
    }
}
