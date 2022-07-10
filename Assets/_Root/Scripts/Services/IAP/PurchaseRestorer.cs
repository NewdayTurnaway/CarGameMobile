using Tool;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Services.IAP
{
    internal sealed class PurchaseRestorer
    {
        private readonly IExtensionProvider _extensionProvider;


        public PurchaseRestorer(IExtensionProvider extensionProvider) =>
            _extensionProvider = extensionProvider;


        public void Restore()
        {
            this.Log("RestorePurchases started ...");

            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.OSXPlayer:
                    _extensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(OnRestoredTransactions);
                    break;

                case RuntimePlatform.Android:
                    _extensionProvider.GetExtension<IGooglePlayStoreExtensions>().RestoreTransactions(OnRestoredTransactions);
                    break;

                default:
                    this.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
                    break;
            }
        }

        private void OnRestoredTransactions(bool result) =>
            this.Log("RestorePurchases continuing: " + result +
                ". If no further messages, no purchases available to restore.");
    }
}
