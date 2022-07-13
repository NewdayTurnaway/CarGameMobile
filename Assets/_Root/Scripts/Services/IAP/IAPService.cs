using Services.Analytics;
using Tool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

namespace Services.IAP
{
    internal sealed class IAPService : SingletoneMonoBehaviour<IAPService>, IStoreListener, IIAPService
    {
        [Header("Components")]
        [SerializeField] private ProductLibrary _productLibrary;

        [field: Header("Events")]
        [field: SerializeField] public UnityEvent Initialized { get; private set; }
        [field: SerializeField] public UnityEvent PurchaseSucceed { get; private set; }
        [field: SerializeField] public UnityEvent PurchaseFailed { get; private set; }
        public bool IsInitialized { get; private set; }

        private IExtensionProvider _extensionProvider;
        private PurchaseValidator _purchaseValidator;
        private PurchaseRestorer _purchaseRestorer;
        private IStoreController _controller;

        private void OnValidate()
        {
            _productLibrary ??= _productLibrary = ResourcesLoader.LoadObject<ProductLibrary>(new ResourcePath(Constants.Settings.Iap.UNITY_IAP));
        }

        protected override void Init()
        {
            _productLibrary ??= _productLibrary = ResourcesLoader.LoadObject<ProductLibrary>(new ResourcePath(Constants.Settings.Iap.UNITY_IAP));
            Initialized = new();
            PurchaseSucceed = new();
            PurchaseFailed = new();

            InitializeProducts();
        }

        private void InitializeProducts()
        {
            StandardPurchasingModule purchasingModule = StandardPurchasingModule.Instance();
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(purchasingModule);

            foreach (Product product in _productLibrary.Products)
                builder.AddProduct(product.Id, product.ProductType);

            this.Log("Products initialized");
            UnityPurchasing.Initialize(this, builder);
        }


        void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensionsProvider)
        {
            IsInitialized = true;
            _controller = controller;
            _extensionProvider = extensionsProvider;
            _purchaseValidator = new PurchaseValidator();
            _purchaseRestorer = new PurchaseRestorer(_extensionProvider);

            this.Log("Initialized");
            Initialized?.Invoke();
        }

        void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
        {
            IsInitialized = false;
            this.Error("Initialization Failed");
        }

        PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs args)
        {
            UnityEngine.Purchasing.Product product = args.purchasedProduct;
            if (_purchaseValidator.Validate(args))
            {
                PurchaseSucceed.Invoke();
                AnalyticsManager.Instance.PurchaseSucceed(product.definition.id,
                    product.metadata.localizedPrice, product.metadata.localizedPriceString);
            }
            else
                OnPurchaseFailed(product.definition.id, "NonValid");

            return PurchaseProcessingResult.Complete;
        }

        void IStoreListener.OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason) =>
            OnPurchaseFailed(product.definition.id, failureReason.ToString());

        private void OnPurchaseFailed(string productId, string reason)
        {
            this.Error($"Failed {productId}: {reason}");
            PurchaseFailed?.Invoke();
        }


        public void Buy(string id)
        {
            if (IsInitialized)
                _controller.InitiatePurchase(id);
            else
                this.Error($"Buy {id} FAIL. Not initialized.");
        }

        public string GetCost(string productID)
        {
            UnityEngine.Purchasing.Product product = _controller.products.WithID(productID);
            return product != null ? product.metadata.localizedPriceString : "N/A";
        }

        public void RestorePurchases()
        {
            if (IsInitialized)
                _purchaseRestorer.Restore();
            else
                this.Error("RestorePurchases FAIL. Not initialized.");
        }
    }
}
