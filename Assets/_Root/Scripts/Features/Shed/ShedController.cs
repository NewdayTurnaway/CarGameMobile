using Tool;
using Profile;
using System;
using System.Collections.Generic;
using UnityEngine;
using Features.Inventory;
using Features.Shed.Upgrade;
using System.Diagnostics.CodeAnalysis;
using Object = UnityEngine.Object;

namespace Features.Shed
{
    internal interface IShedController
    {
    }

    internal sealed class ShedController : BaseController, IShedController
    {
        private readonly ResourcePath _viewPath = new(Constants.PrefabPaths.Ui.SHED);
        private readonly ResourcePath _dataSourcePath = new(Constants.Configs.UPGRADE_ITEM);

        private readonly ShedView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly InventoryContext _inventoryContext;
        private readonly UpgradeHandlersRepository _upgradeHandlersRepository;


        public ShedController(
            [NotNull] Transform placeForUi,
            [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _upgradeHandlersRepository = CreateRepository();
            _inventoryContext = CreateInventoryContext(placeForUi, _profilePlayer.Inventory);
            _view = LoadView(placeForUi);

            _view.Init(Apply, Back);
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

        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }

        private void Apply()
        {
            _profilePlayer.CurrentCar.Restore();

            UpgradeWithEquippedItems(
                _profilePlayer.CurrentCar,
                _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            _profilePlayer.CurrentState.Value = GameState.Start;
            this.Log($"Apply. Current: Speed {_profilePlayer.CurrentCar.Speed} | JumpHeight {_profilePlayer.CurrentCar.JumpHeight}");
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            this.Log($"Back. Current: Speed {_profilePlayer.CurrentCar.Speed} | JumpHeight {_profilePlayer.CurrentCar.JumpHeight}");
        }

        private void UpgradeWithEquippedItems(
            IUpgradable upgradable,
            IReadOnlyList<string> equippedItems,
            IReadOnlyDictionary<string, IUpgradeHandler> upgradeHandlers)
        {
            foreach (string itemId in equippedItems)
                if (upgradeHandlers.TryGetValue(itemId, out IUpgradeHandler handler))
                    handler.Upgrade(upgradable);
        }
    }
}
