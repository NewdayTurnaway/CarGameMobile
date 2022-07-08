using Ui;
using Game;
using Profile;
using UnityEngine;

internal sealed class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private GameController _gameController;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _settingsMenuController?.Dispose();
        _gameController?.Dispose();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new(_placeForUi, _profilePlayer);
                _settingsMenuController?.Dispose();
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new(_profilePlayer);
                _mainMenuController?.Dispose();
                _settingsMenuController?.Dispose();
                break;
            case GameState.Settings:
                _settingsMenuController = new(_placeForUi, _profilePlayer);
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _settingsMenuController?.Dispose();
                _gameController?.Dispose();
                break;
        }
    }
}