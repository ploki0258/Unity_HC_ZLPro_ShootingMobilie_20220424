using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements; //�ޥμs�i API

namespace JACK
{
    public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener,IUnityAdsShowListener
    {
        [SerializeField, Header("�ݧ��s�i������"), Range(0, 1000)]
        private int addCoinValue = 100;

        private int coinPlayer;
        /// <summary>
        ///�s�i���s�K�[���� 
        /// </summary>
        private Button btnAdsAddCoin;

        private string gameIdAndroid = "4754881"; //��x Android ID
        private string gameIdIos = "4754880"; //��x ios ID
        private string gameId;

        private string adsIdAndroid = "AddCoin";
        private string adsIdios = "AddCoin";
        private string adsId;

        //��l�Ʀ��\�|���檺��k
        public void OnInitializationComplete()
        {
            print("<color=green>1. �s�i��l�Ʀ��\</color>");
        }

        //��l�ƥ��ѷ|���檺��k
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            print("<color=red>2. �s�i��l�ƥ��ѡA��]:" + message + "</color>");
        }

        /// <summary>
        /// ���a�����ƶq
        /// </summary>
        private Text textCoin;

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            print("<color=red>3. �s�i��ܥ���" + message + "</color>");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            print("<color=green>3. �s�i��ܶ}�l" + placementId + "</color>");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            print("<color=green>3. �s�i����I��" + placementId + "</color>");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            print("<color=green>3. �s�i��ܧ���" + placementId + "</color>");

            coinPlayer += addCoinValue;
            textCoin.text = coinPlayer.ToString();
        }

        //���J���\�|���檺��k
        public void OnUnityAdsAdLoaded(string placementId)
        {
            print("<color=green>2. �s�i���J���\</color>");
        }

        //���J���ѷ|���檺��k
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            print("<color=red>�s�i���J���ѡA��]:" + message + "</color>");
        }

        /// <summary>
        /// ���J�s�i
        /// </summary>
        private void LoadAds()
        {
            print("���J�s�i�AID : " + adsId);
            Advertisement.Load(adsId, this);
            ShowAds();
        }

        private void ShowAds()
        {
            Advertisement.Show(adsId, this);
        }

        private void Awake()
        {
            textCoin = GameObject.Find("���a�����ƶq").GetComponent<Text>();
            btnAdsAddCoin = GameObject.Find("�s�i����K�[����").GetComponent<Button>();
            btnAdsAddCoin.onClick.AddListener(LoadAds);

            InitializeAds();

            //#if�{���϶��P�_���A����F���~�|����Ӱ϶�
            //�p�G ���a �@�~�t�� �O ios �N���w�� ios �s�i
            //�_�h�p�G ���a �@�~�t�� �O Android �N���w�� Android �s�i
#if UNITY_IOS
            adsId = adsIdIos;

#elif UNITY_ANDROID
             adsId = adsIdAndroid;
#endif
            //PC �ݴ���
            adsId = adsIdAndroid;
        }

        /// <summary>
        /// ��l�Ƽs�i�t��
        /// </summary>
        private void InitializeAds()
        {
            gameId = gameIdAndroid;
            Advertisement.Initialize(gameId, true, this);
        }

        
    }
}


