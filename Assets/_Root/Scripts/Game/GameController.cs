using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Tool;
using Services.Analytics;

namespace Game
{
    internal sealed class GameController : BaseController
    {
        public GameController(ProfilePlayer profilePlayer)
        {
            AnalyticsManager.Instance.SendGameStarted();

            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            var carController = new CarController();
            AddController(carController);
        }
    }
}
