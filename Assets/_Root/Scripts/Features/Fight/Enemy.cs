using UnityEngine;

namespace Features.Fight
{
    internal interface IEnemy
    {
        void Update(PlayerData playerData);
    }

    internal sealed class Enemy : IEnemy
    {
        private const float MONEY_COEFF = 5f;
        private const float POWER_COEFF = 2f;
        private const int DEFAULT_HEALTH_ENEMY = 6;

        private readonly string _name;

        private int _moneyPlayer;
        private int _healthPlayer;
        private int _powerPlayer;


        public Enemy(string name) =>
            _name = name;


        public void Update(PlayerData playerData)
        {
            switch (playerData.DataType)
            {
                case DataType.Money:
                    _moneyPlayer = playerData.Value;
                    break;

                case DataType.Health:
                    _healthPlayer = playerData.Value;
                    break;

                case DataType.Power:
                    _powerPlayer = playerData.Value;
                    break;
            }

            Debug.Log($"Notified {_name} change to {playerData.DataType:F}");
        }

        public int CalcPower()
        {
            int healthCoeff = CalcHealthCoeff();
            float moneyRatio = _moneyPlayer / MONEY_COEFF;
            float powerRatio = _powerPlayer / POWER_COEFF;

            return (int)(moneyRatio + healthCoeff + powerRatio);
        }

        private int CalcHealthCoeff()
        {
            int coeff = (int)(0.75f * DEFAULT_HEALTH_ENEMY * (_healthPlayer / 12));
            return DEFAULT_HEALTH_ENEMY + coeff;
        }
    }
}

