using Tool;
using System;
using Profile;
using UnityEngine;
using Features.Inventory;
using Features.Shed.Upgrade;
using System.Diagnostics.CodeAnalysis;
using Object = UnityEngine.Object;

namespace Features.Shed
{
    internal sealed class ShedContext : BaseContext
    {
        private readonly ResourcePath _viewPath = new(Constants.PrefabPaths.Ui.SHED);
        private readonly ResourcePath _dataSourcePath = new(Constants.Configs.UPGRADE_ITEM);

        public ShedContext([NotNull] Transform placeForUi, [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            if (profilePlayer == null)
                throw new ArgumentNullException(nameof(profilePlayer));

            CreateController(placeForUi, profilePlayer);
        }

        private ShedController CreateController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            CreateInventoryContext(placeForUi, profilePlayer.Inventory);
            ShedView view = LoadView(placeForUi);
            UpgradeHandlersRepository upgradeHandlersRepository = CreateRepository();

            ShedController controller = new(profilePlayer, view, upgradeHandlersRepository);

            AddController(controller);

            return controller;
        }

        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }

        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            UpgradeHandlersRepository repository = new(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private InventoryContext CreateInventoryContext(Transform placeForUi, IInventoryModel model)
        {
            InventoryContext context = new(placeForUi, model);
            AddContext(context);

            return context;
        }
    } 
}
