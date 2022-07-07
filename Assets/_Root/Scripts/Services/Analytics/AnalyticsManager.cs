using UnityEngine;
using Services.Analytics.UnityAnalytics;
using System.Collections.Generic;

namespace Services.Analytics
{
    internal class AnalyticsManager : MonoBehaviour
    {
        private IAnalyticsService[] _services;


        private void Awake() =>
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };


        public void SendMainMenuOpened() =>
            SendEvent("MainMenuOpened");


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
    }
}
