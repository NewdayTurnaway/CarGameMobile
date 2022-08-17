using Features.Rewards.Resource;
using System.Collections.Generic;
using Tool;
using UnityEngine;

namespace Features.Rewards.Currency
{
    internal sealed class CurrencyController : BaseController
    {
        private readonly ResourcePath _resourcePath = new(Constants.PrefabPaths.Ui.CURRENCY_VIEW);
        private readonly ResourcePath _dataSourcePath = new(Constants.Configs.RESOURCE_COLLECTION);

        private readonly CurrencyModel _model;
        private readonly CurrencyView _view;

        private readonly ResourceConfig[] _resources;
        private readonly List<CurrencySlotView> _slots = new();
        private readonly CurrencySlotPanel _currencySlotPanel;

        public CurrencyController(CurrencyModel currencyModel, Transform placeForUi)
        {
            _model = currencyModel;
            _view = LoadView(placeForUi);
            _resources = ContentDataSourceLoader.LoadResourceConfigs(_dataSourcePath);
            _currencySlotPanel = new(_view, _resources, _slots);

            Init();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            Deinit();
        }

        public void CurrencySlotsRefresh()
        {
            foreach (CurrencySlotView view in _slots)
            {
                view.SetData(_model.GetResourceValue(view.Type));
            }
        }

        public void AddResource(ResourceType type, int value)
        {
            int resourceValue = _model.GetResourceValue(type);
            _model.SetResourceValue(type, resourceValue + value);
            foreach(CurrencySlotView view in _slots)
            {
                if (view.Type == type)
                {
                    view.SetData(resourceValue);
                }
            }
        }

        private CurrencyView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<CurrencyView>();
        }

        private void Init()
        {
            _currencySlotPanel.Init();
            foreach (CurrencySlotView view in _slots)
            {
                view.SetData(_model.GetResourceValue(view.Type));
            }
        }

        private void Deinit() =>
            _currencySlotPanel.Deinit();
    }
}
