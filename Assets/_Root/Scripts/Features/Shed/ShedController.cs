using Tool;
using Profile;
using System;
using System.Collections.Generic;
using UnityEngine;
using Features.Inventory;
using Features.Shed.Upgrade;
using System.Diagnostics.CodeAnalysis;

namespace Features.Shed
{
    internal interface IShedController
    {
    }

    internal sealed class ShedController : BaseController, IShedController
    {
        private readonly IShedView _view;
        private readonly IUpgradeHandlersRepository _upgradeHandlersRepository;
        private readonly ProfilePlayer _profilePlayer;
        private readonly InventoryContext _inventoryContext;


        public ShedController(
            [NotNull] Transform placeForUi,
            [NotNull] ProfilePlayer profilePlayer,
            [NotNull] IShedView view,
            [NotNull] IUpgradeHandlersRepository upgradeHandlersRepository,
            [NotNull] InventoryContext inventoryContext)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _view
                = view ?? throw new ArgumentNullException(nameof(view));

            _upgradeHandlersRepository
                = upgradeHandlersRepository ?? throw new ArgumentNullException(nameof(upgradeHandlersRepository));

            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));
            
            _inventoryContext 
                = inventoryContext ?? throw new ArgumentNullException(nameof(inventoryContext));

            _view.Init(Apply, Back);
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
