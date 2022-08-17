using System;

namespace Features.Fight
{
    internal sealed class PlayerData
    {
        public event Action<PlayerData> ValueChanged;
        private int _value;

        public DataType DataType { get; }

        public int Value
        {
            get => _value;
            set => SetValue(value);
        }

        public PlayerData(DataType dataType)
        {
            DataType = dataType;
        }

        private void SetValue(int value)
        {
            if (value < 0)
                return;
            if (_value == value)
                return;

            _value = value;
            ValueChanged?.Invoke(this);
        }
    }
}
