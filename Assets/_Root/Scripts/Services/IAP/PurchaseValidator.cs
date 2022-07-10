using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using Tool;

namespace Services.IAP
{
    internal sealed class PurchaseValidator
    {
        public bool Validate(PurchaseEventArgs args)
        {
            var isValid = true;

            // нужно заполнить сначала данные из магазина приложений
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX)
            // var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
            //     AppleTangle.Data(), Application.identifier);
            //
            // try
            // {
            //     var result = validator.Validate(args.purchasedProduct.receipt);
            // }
            // catch (IAPSecurityException)
            // {
            //     isValid = false;
            // }
#endif

            string logMessage = isValid ?
                $"Receipt is valid. Contents: {args.purchasedProduct.receipt}" :
                "Invalid receipt, not unlocking content";

            this.Log(logMessage);
            return isValid;
        }
    }
}
