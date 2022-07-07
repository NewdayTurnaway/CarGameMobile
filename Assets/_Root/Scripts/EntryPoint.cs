using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    private const GameState INITIAL_STATE = GameState.Start;

    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;

    private void Start()
    {
        ProfilePlayer profilePlayer = new(Constants.Variables.SPEED_CAR, INITIAL_STATE);
        _mainController = new(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
