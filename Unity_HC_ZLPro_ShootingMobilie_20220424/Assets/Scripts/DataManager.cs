using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

namespace JACK
{
    /// <summary>
    /// 資料管理
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        //新增部屬作業內的網址，有變更 GAS 都要新增
        private string gasLink = "https://script.google.com/macros/s/AKfycbyavYtE0zDeTZ7B0dmgMEIBuS0tApCr2Rdm9KQtPojZJx49FC4wgfFxhHZQFippFPRP/exec";
        private WWWForm form;

        private Button btnGetData;
        private Text textPlayerName;

        private void Start()
        {
            textPlayerName = GameObject.Find("玩家名稱").GetComponent<Text>();
            btnGetData = GameObject.Find("取得玩家資料按鈕").GetComponent<Button>();
            btnGetData.onClick.AddListener(GetGASData);
        }

        /// <summary>
        /// 取得 GAS 資料
        /// </summary>
        private void GetGASData()
        {
            form = new WWWForm();
            form.AddField("method", "取得");

            StartCoroutine(StarGetGASData());
        }

        private IEnumerator StarGetGASData()
        {
            //新增網頁連線要求(gasLink，表單資料)
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink,form))
            {
                //等待網頁連線要求
                yield return www.SendWebRequest();
                //玩家名稱 = 連線要求下載的文字訊息
                textPlayerName.text = www.downloadHandler.text;
            }
        }
    }
}

