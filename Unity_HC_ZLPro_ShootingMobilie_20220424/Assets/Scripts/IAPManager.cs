using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace JACK
{
    public class IAPManager : MonoBehaviour
    {
        [SerializeField, Header("�ʶR�ֽ����s")]
        private IAPButton iapBuySkinRed;
        [SerializeField, Header("�ʶR���ܰT��")]
        private Text textIAPTip;

        private void Awake()
        {
            //����ֽ����ʫ��s �ʶR���\�� �K�[��ť�� (�ʶR���\��k)
            iapBuySkinRed.onPurchaseComplete.AddListener(PurchaseCompleteSkinRed);
            //����ֽ����ʫ��s �ʶR���ѫ� �K�[��ť�� (�ʶR���Ѥ�k)
            iapBuySkinRed.onPurchaseFailed.AddListener(PurchaseFailedSkinRed);
        }

        /// <summary>
        /// �ʶR���\
        /// </summary>
        private void PurchaseCompleteSkinRed(Product product)
        {
            textIAPTip.text = product.ToString() + "�ʶR���\�C";
        }

        /// <summary>
        /// �ʶR����
        /// </summary>
        private void PurchaseFailedSkinRed(Product product,PurchaseFailureReason reason)
        {
            textIAPTip.text = product.ToString() + "�ʶR���ѡA��]�G" + reason;
        }
    }
}