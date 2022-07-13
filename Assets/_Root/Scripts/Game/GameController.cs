using Features.AbilitySystem;
using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Services.Analytics;
using Tool;
using UnityEngine;

namespace Game
{
    internal sealed class GameController : BaseController
    {
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly CarController _carController;
        private readonly InputGameController _inputGameController;
        private readonly AbilitiesController _abilitiesController;
        private readonly TapeBackgroundController _tapeBackgroundController;

        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            AnalyticsManager.Instance.GameStarted();

            _leftMoveDiff = new();
            _rightMoveDiff = new();

            _carController = CreateCarController(profilePlayer.CurrentCar);
            _inputGameController = CreateInputGameController(profilePlayer, _leftMoveDiff, _rightMoveDiff);
            _abilitiesController = CreateAbilitiesController(placeForUi, _carController);
            _tapeBackgroundController = CreateTapeBackground(_leftMoveDiff, _rightMoveDiff);
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

        private AbilitiesController CreateAbilitiesController(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            AbilitiesController abilitiesController = new(placeForUi, abilityActivator);
            AddController(abilitiesController);

            return abilitiesController;
        }
    }
}
