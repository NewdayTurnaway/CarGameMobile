using Tool;
using Profile;
using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Features.AbilitySystem;
using Services.Analytics;
using UnityEngine;
using Ui;
using Features.Fight;

namespace Game
{
    internal sealed class GameController : BaseController
    {
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly CarController _carController;

        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            AnalyticsManager.Instance.GameStarted();

            _leftMoveDiff = new();
            _rightMoveDiff = new();

            _carController = CreateCarController(profilePlayer.CurrentCar);
            CreateInputGameController(profilePlayer, _leftMoveDiff, _rightMoveDiff);
            CreateTapeBackground(_leftMoveDiff, _rightMoveDiff);
            CreateAbilitiesContext(placeForUi, _carController);
            CreateStartFightController(placeForUi, profilePlayer);
            CreateInGameMenuController(placeForUi, profilePlayer);
        }

        private InGameMenuController CreateInGameMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            InGameMenuController controller = new(placeForUi, profilePlayer);
            AddController(controller);

            return controller;
        }

        private StartFightController CreateStartFightController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            StartFightController controller = new(placeForUi, profilePlayer);
            AddController(controller);

            return controller;
        }

        private TapeBackgroundController CreateTapeBackground(SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            TapeBackgroundController tapeBackgroundController = new(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }

        private InputGameController CreateInputGameController(ProfilePlayer profilePlayer,
            SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            InputGameController inputGameController = new(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            return inputGameController;
        }

        private CarController CreateCarController(CarModel carModel)
        {
            CarController carController = new(carModel);
            AddController(carController);

            return carController;
        }

        private AbilitiesContext CreateAbilitiesContext(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            AbilitiesContext context = new(placeForUi, abilityActivator);
            AddContext(context);

            return context;
        }
    }
}