using Tool;
using System;
using UnityEngine;
using Features.Inventory.Items;
using System.Diagnostics.CodeAnalysis;
using Object = UnityEngine.Object;

namespace Features.Inventory
{
    internal sealed class InventoryContext : BaseContext
    {
        private static readonly ResourcePath _viewPath = new(Constants.PrefabPaths.Ui.INVENTORY);
        private static readonly ResourcePath _dataSourcePath = new(Constants.Configs.ITEM);


        public InventoryContext([NotNull] Transform placeForUi, [NotNull] IInventoryModel model)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            if (model == null)
                throw new ArgumentNullException(nameof(model));

            CreateController(placeForUi, model);
        }


        private InventoryController CreateController(Transform placeForUi, IInventoryModel model)
        {
            InventoryView view = LoadView(placeForUi);
            ItemsRepository repository = CreateRepository();

            InventoryController inventoryController = new(view, model, repository);
            AddController(inventoryController);

            return inventoryController;
        }

        private InventoryView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }

        private ItemConfig[] LoadConfigs() =>
            ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);

        private ItemsRepository CreateRepository()
        {
            ItemConfig[] itemConfigs = LoadConfigs();
            ItemsRepository repository = new(itemConfigs);
            AddRepository(repository);

            return repository;
        }
    }
}