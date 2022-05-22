using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace JACK
{
    public class IAPManager : MonoBehaviour
    {
        [SerializeField, Header("購買皮膚按鈕")]
        private IAPButton iapBuySkinRed;
        [SerializeField, Header("購買提示訊息")]
        private Text textIAPTip;

        private void Awake()
        {
            //紅色皮膚內購按鈕 購買成功後 添加監聽器 (購買成功方法)
            iapBuySkinRed.onPurchaseComplete.AddListener(PurchaseCompleteSkinRed);
            //紅色皮膚內購按鈕 購買失敗後 添加監聽器 (購買失敗方法)
            iapBuySkinRed.onPurchaseFailed.AddListener(PurchaseFailedSkinRed);
        }

        /// <summary>
        /// 購買成功
        /// </summary>
        private void PurchaseCompleteSkinRed(Product product)
        {
            textIAPTip.text = product.ToString() + "購買成功。";
        }

        /// <summary>
        /// 購買失敗
        /// </summary>
        private void PurchaseFailedSkinRed(Product product,PurchaseFailureReason reason)
        {
            textIAPTip.text = product.ToString() + "購買失敗，原因：" + reason;
        }
    }
}