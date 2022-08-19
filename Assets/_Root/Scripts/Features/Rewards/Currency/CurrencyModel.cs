using Features.Rewards.Resource;
using UnityEngine;

namespace Features.Rewards.Currency
{
    internal sealed class CurrencyModel
    {
        public int GetResourceValue(ResourceType type) =>
            PlayerPrefs.GetInt(type.ToString(), 0);

        public void SetResourceValue(ResourceType type, int value)
        {
            int oldValue = GetResourceValue(type);
            if (oldValue == value)
                return;

            PlayerPrefs.SetInt(type.ToString(), value);
        }
    }
}
