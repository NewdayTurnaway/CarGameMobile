using Profile;
using Tool;
using UnityEngine;

namespace Features.Fight
{
    internal sealed class StartFightController : BaseController
    {
        private readonly ResourcePath _resourcePath = new(Constants.PrefabPaths.Ui.START_FIGHT_VIEW);

        private readonly StartFightView _view;
        private readonly ProfilePlayer _profilePlayer;


        public StartFightController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartFight);
        }


        private StartFightView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<StartFightView>();
        }

        private void StartFight() =>
            _profilePlayer.CurrentState.Value = GameState.Fight;
    }
}
