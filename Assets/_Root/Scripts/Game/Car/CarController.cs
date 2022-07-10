using Features.AbilitySystem;
using Tool;
using UnityEngine;

namespace Game.Car
{
    internal sealed class CarController : BaseController, IAbilityActivator
    {
        private readonly ResourcePath _viewPath = new(Constants.PrefabPaths.CAR);
        private readonly CarView _view;
        private readonly CarModel _model;

        public GameObject ViewGameObject => _view.gameObject;
        public Rigidbody2D BodyRigidbody => _view.BodyRigidbody;
        public CarModel Model => _model;

        public CarController(CarModel carModel)
        {
            _view = LoadView();
            _model = carModel;
        }


        private CarView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<CarView>();
        }
    }
}
