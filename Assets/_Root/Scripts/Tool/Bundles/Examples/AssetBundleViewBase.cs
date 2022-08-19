using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

namespace Tool.Bundles.Examples
{
    internal class AssetBundleViewBase : MonoBehaviour
    {
        private const string URL_GDRIVE = "https://drive.google.com/uc?export=download&id=";

        [Header("Google Drive Id Asset Files")]
        [SerializeField] private string _assetBundleSpritesId = "1Pg0GiIG7SeVcF-PP0ymRWpAVoAfF_7UY";
        [SerializeField] private string _assetBundleAudioId = "1PYL-GSZ8fW7QQczJkzlIW-hyo4qK4PPH";
        [SerializeField] private string _assetBundleButtonId = "1454tPEyO60aJwictv2fptveiFHKTkQ4X";

        [Header("Data Bundles")]
        [SerializeField] private DataSpriteBundle[] _dataSpriteBundles;
        [SerializeField] private DataAudioBundle[] _dataAudioBundles;
        [SerializeField] private DataSpriteBundle[] _dataButtonBundles;

        private string _urlAssetBundleSprites;
        private string _urlAssetBundleAudio;
        private string _urlAssetBundleButton;

        private AssetBundle _spritesAssetBundle;
        private AssetBundle _audioAssetBundle;
        private AssetBundle _buttonAssetBundle;

        private void Awake()
        {
            _urlAssetBundleSprites = URL_GDRIVE + _assetBundleSpritesId;
            _urlAssetBundleAudio = URL_GDRIVE + _assetBundleAudioId;
            _urlAssetBundleButton = URL_GDRIVE + _assetBundleButtonId;
        }

        protected IEnumerator DownloadAndSetAssetBundles()
        {
            yield return GetSpritesAssetBundle();
            yield return GetAudioAssetBundle();

            if (_spritesAssetBundle != null)
                SetSpriteAssets(_spritesAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_spritesAssetBundle)} failed to load");

            if (_audioAssetBundle != null)
                SetAudioAssets(_audioAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_audioAssetBundle)} failed to load");
        }

        protected IEnumerator DownloadAndSetButtonAssetBundle()
        {
            yield return GetButtonAssetBundle();

            if (_buttonAssetBundle != null)
                SetButtonAssets(_buttonAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_buttonAssetBundle)} failed to load");
        }

        private IEnumerator GetButtonAssetBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_urlAssetBundleButton);
            DateTime dateTime = DateTime.Now;
            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _buttonAssetBundle, dateTime);
        }

        private IEnumerator GetSpritesAssetBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_urlAssetBundleSprites);
            DateTime dateTime = DateTime.Now;
            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _spritesAssetBundle, dateTime);
        }

        private IEnumerator GetAudioAssetBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_urlAssetBundleAudio);
            DateTime dateTime = DateTime.Now;
            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _audioAssetBundle, dateTime);
        }

        private void StateRequest(UnityWebRequest request, out AssetBundle assetBundle, DateTime dateTime)
        {
            if (request.error == null)
            {
                assetBundle = DownloadHandlerAssetBundle.GetContent(request);
                Debug.Log("Complete");
                Debug.Log($"Load Time: {DateTime.Now - dateTime}");
            }
            else
            {
                assetBundle = null;
                Debug.LogError(request.error);
            }
        }

        private void SetSpriteAssets(AssetBundle assetBundle)
        {
            foreach (DataSpriteBundle data in _dataSpriteBundles)
                data.Image.sprite = assetBundle.LoadAsset<Sprite>(data.NameAssetBundle);
        }

        private void SetButtonAssets(AssetBundle assetBundle)
        {
            foreach (DataSpriteBundle data in _dataButtonBundles)
                data.Image.sprite = assetBundle.LoadAsset<Sprite>(data.NameAssetBundle);
        }

        private void SetAudioAssets(AssetBundle assetBundle)
        {
            foreach (DataAudioBundle data in _dataAudioBundles)
            {
                data.AudioSource.clip = assetBundle.LoadAsset<AudioClip>(data.NameAssetBundle);
                data.AudioSource.Play();
            }
        }
    }
}
