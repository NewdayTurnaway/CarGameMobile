using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

namespace Tool.Bundles.Examples
{
    internal sealed class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _loadAssetsButton;
        [SerializeField] private Button _loadSpriteAssetButton;

        [Header("Addressables")]
        [SerializeField] private AssetReference _spawningButtonPrefab;
        [SerializeField] private RectTransform _spawnedButtonsContainer;
        [SerializeField] private Button _spawnAssetButton;
        [SerializeField] private AssetReferenceSprite _backgroundSprite;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Button _addAssetBackgroundButton;
        [SerializeField] private Button _removeAssetBackgroundButton;

        private readonly List<AsyncOperationHandle<GameObject>> _addressablePrefabs = new();
        private AsyncOperationHandle<Sprite> _addressableSprite;


        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAssets);
            _loadSpriteAssetButton.onClick.AddListener(LoadSpriteAsset);

            _spawnAssetButton.onClick.AddListener(SpawnPrefab);
            _addAssetBackgroundButton.onClick.AddListener(AddBackground);
            _removeAssetBackgroundButton.onClick.AddListener(RemoveBackground);
        }

        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveListener(LoadAssets);
            _loadSpriteAssetButton.onClick.RemoveListener(LoadSpriteAsset);

            _spawnAssetButton.onClick.RemoveListener(SpawnPrefab);
            _addAssetBackgroundButton.onClick.RemoveListener(AddBackground);
            _removeAssetBackgroundButton.onClick.RemoveListener(RemoveBackground);

            DespawnPrefabs();
        }

        private void LoadAssets()
        {
            _loadAssetsButton.interactable = false;
            StartCoroutine(DownloadAndSetAssetBundles());
        }
        private void LoadSpriteAsset()
        {
            _loadSpriteAssetButton.interactable = false;
            StartCoroutine(DownloadAndSetButtonAssetBundle());
        }

        private void SpawnPrefab()
        {
            AsyncOperationHandle<GameObject> addressablePrefab =
                Addressables.InstantiateAsync(_spawningButtonPrefab, _spawnedButtonsContainer);

            _addressablePrefabs.Add(addressablePrefab);
        }

        private void DespawnPrefabs()
        {
            foreach (AsyncOperationHandle<GameObject> addressablePrefab in _addressablePrefabs)
                Addressables.ReleaseInstance(addressablePrefab);

            _addressablePrefabs.Clear();
        }

        private void AddBackground()
        {
            _addressableSprite = _backgroundSprite.LoadAssetAsync();
            _addressableSprite.Completed += BackgroundLoaded;
        }

        private void BackgroundLoaded(AsyncOperationHandle<Sprite> obj)
        {
            switch (obj.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    _backgroundImage.sprite = obj.Result;
                    _backgroundImage.color = Color.white;
                    break;
                case AsyncOperationStatus.Failed:
                    Debug.LogError(nameof(BackgroundLoaded));
                    break;
                default:
                    break;
            }
        }

        private void RemoveBackground()
        {
            _backgroundImage.sprite = null;
            _backgroundImage.color = Color.clear;
            Addressables.Release(_addressableSprite);
        }
    }
}
