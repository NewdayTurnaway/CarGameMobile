using System;
using Tool;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal abstract class UnityAdsPlayer : IAdsPlayer, IUnityAdsListener
    {
        public event Action Started;
        public event Action Finished;
        public event Action Failed;
        public event Action Skipped;
        public event Action BecomeReady;

        protected readonly string Id;


        protected UnityAdsPlayer(string id)
        {
            Id = id;
            Advertisement.AddListener(this);
        }


        public void Play()
        {
            Load();
            OnPlaying();
            Load();
        }

        protected abstract void OnPlaying();
        protected abstract void Load();


        void IUnityAdsListener.OnUnityAdsReady(string placementId)
        {
            if (IsIdMy(placementId) == false)
                return;

            this.Log("Ready");
            BecomeReady?.Invoke();
        }

        void IUnityAdsListener.OnUnityAdsDidError(string message) =>
            this.Error($"Error: {message}");

        void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
        {
            if (IsIdMy(placementId) == false)
                return;

            this.Log("Started");
            Started?.Invoke();
        }

        void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (IsIdMy(placementId) == false)
                return;

            switch (showResult)
            {
                case ShowResult.Finished:
                    this.Log("Finished");
                    Finished?.Invoke();
                    break;

                case ShowResult.Failed:
                    this.Error("Failed");
                    Failed?.Invoke();
                    break;

                case ShowResult.Skipped:
                    this.Log("Skipped");
                    Skipped?.Invoke();
                    break;
            }
        }


        private bool IsIdMy(string id) => Id == id;
    }
}
