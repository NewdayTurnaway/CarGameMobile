using System;
using Profile;
using TMPro;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Fight
{
    internal sealed class FightController : BaseController
    {
        private readonly ResourcePath _resourcePath = new(Constants.PrefabPaths.Ui.FIGHT_VIEW);
        private readonly ProfilePlayer _profilePlayer;
        private readonly FightView _view;
        private readonly Enemy _enemy;

        private const int CRIME_LEVEL_LIMIT = 6;

        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _crimeLevel;

        public FightController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);

            _enemy = new(nameof(Enemy));

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _crimeLevel = CreatePlayerData(DataType.CrimeLevel);

            Subscribe(_view);

            ChangeFightButtons(_crimeLevel);
        }

        protected override void OnDispose()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);
            DisposePlayerData(ref _crimeLevel);

            Unsubscribe(_view);
        }

        private FightView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<FightView>();
        }

        private PlayerData CreatePlayerData(DataType dataType)
        {
            PlayerData playerData = new(dataType);
            playerData.ValueChanged += _enemy.Update;

            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.ValueChanged -= _enemy.Update;
            playerData = null;
        }

        private void Subscribe(FightView view)
        {
            view.AddMoneyButton.onClick.AddListener(IncreaseMoney);
            view.MinusMoneyButton.onClick.AddListener(DecreaseMoney);

            view.AddHealthButton.onClick.AddListener(IncreaseHealth);
            view.MinusHealthButton.onClick.AddListener(DecreaseHealth);

            view.AddPowerButton.onClick.AddListener(IncreasePower);
            view.MinusPowerButton.onClick.AddListener(DecreasePower);

            view.CrimeLevelButton.onClick.AddListener(ChangeCrimeLevel);

            view.PassButton.onClick.AddListener(PassFight);
            view.FightButton.onClick.AddListener(Fight);
        }

        private void Unsubscribe(FightView view)
        {
            view.AddMoneyButton.onClick.RemoveListener(IncreaseMoney);
            view.MinusMoneyButton.onClick.RemoveListener(DecreaseMoney);

            view.AddHealthButton.onClick.RemoveListener(IncreaseHealth);
            view.MinusHealthButton.onClick.RemoveListener(DecreaseHealth);

            view.AddPowerButton.onClick.RemoveListener(IncreasePower);
            view.MinusPowerButton.onClick.RemoveListener(DecreasePower);

            view.CrimeLevelButton.onClick.RemoveListener(ChangeCrimeLevel);

            view.PassButton.onClick.RemoveListener(PassFight);
            view.FightButton.onClick.RemoveListener(Fight);
        }

        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_heath);
        private void DecreaseHealth() => DecreaseValue(_heath);

        private void IncreasePower() => IncreaseValue(_power);
        private void DecreasePower() => DecreaseValue(_power);


        private void IncreaseValue(PlayerData playerData) => AddToValue(1, playerData);
        private void DecreaseValue(PlayerData playerData) => AddToValue(-1, playerData);

        private void AddToValue(int addition, PlayerData playerData)
        {
            playerData.Value += addition;
            ChangeDataWindow(playerData);
        }

        private void ChangeCrimeLevel() => NextCrimeLevel(_crimeLevel);

        private void NextCrimeLevel(PlayerData playerData)
        {
            playerData.Value = (playerData.Value + 1) % CRIME_LEVEL_LIMIT;
            ChangeDataWindow(playerData);
            ChangeFightButtons(playerData);
        }

        private void ChangeFightButtons(PlayerData playerData)
        {
            bool canPass = playerData.Value <= 2;
            _view.PassButton.gameObject.SetActive(canPass);
            _view.FightButton.gameObject.SetActive(!canPass);
        }

        private void ChangeDataWindow(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F}: {value}";

            int enemyPower = _enemy.CalcPower();
            _view.CountPowerEnemyText.text = $"Enemy Power: {enemyPower}";
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _view.CountMoneyText,
                DataType.Health => _view.CountHealthText,
                DataType.Power => _view.CountPowerText,
                DataType.CrimeLevel => _view.CrimeLevelText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };


        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _power.Value >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");

            Close();
        }

        private void PassFight()
        {
            Debug.Log($"<color=white>Pass</color>");

            Close();
        }

        private void Close() => _profilePlayer.CurrentState.Value = GameState.Game;
    }
}
