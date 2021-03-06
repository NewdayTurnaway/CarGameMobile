using Services.Analytics.UnityAnalytics;
using System.Collections.Generic;
using Tool;

namespace Services.Analytics
{
    internal sealed class AnalyticsManager : SingletoneMonoBehaviour<AnalyticsManager>
    {
        private IAnalyticsService[] _services;

        protected override void Init()
        {
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };
        }

        public void MainMenuOpened()
        {
            string name = nameof(MainMenuOpened);
            SendEvent(name);
            this.Log(name);
        }

        public void GameStarted()
        {
            string name = nameof(GameStarted);
            SendEvent(name);
            this.Log(name);
        }

        public void PurchaseSucceed(string productId, decimal amount, string currency)
        {
            Transaction(productId, amount, currency);
            this.Log($"{nameof(PurchaseSucceed)} | {productId} | {amount} | {currency}");
        }

        private void SendEvent(string eventName)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName);
        }

        private void SendEvent(string eventName, Dictionary<string,object> eventData)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName, eventData);
        }

        private void Transaction(string productId, decimal amount, string currency)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].Transaction(productId, amount, currency);
        }
    }
}