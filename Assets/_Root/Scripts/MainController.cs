using Ui;
using Game;
using Profile;
using UnityEngine;
using Features.Shed;

internal sealed class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private ShedContext _shedContext;
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
        DisposeControllers();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }

    private void OnChangeGameState(GameState state)
    {
        DisposeControllers();
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                _shedContext = CreateShedContext(_placeForUi, _profilePlayer);
                break;
            case GameState.Settings:
                _settingsMenuController = new(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                _gameController = new(_placeForUi, _profilePlayer);
                break;
        }
    }

    private void DisposeControllers()
    {
        _mainMenuController?.Dispose();
        _settingsMenuController?.Dispose();
        _shedContext?.Dispose();
        _gameController?.Dispose();
    }

    private ShedContext CreateShedContext(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        ShedContext context = new(placeForUi, profilePlayer);
        AddContext(context);

        return context;
    }
}