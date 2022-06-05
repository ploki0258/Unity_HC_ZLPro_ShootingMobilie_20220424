using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

namespace JACK
{
    /// <summary>
    /// ��ƺ޲z
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        //�s�W���ݧ@�~�������}�A���ܧ� GAS ���n�s�W
        private string gasLink = "https://script.google.com/macros/s/AKfycbyavYtE0zDeTZ7B0dmgMEIBuS0tApCr2Rdm9KQtPojZJx49FC4wgfFxhHZQFippFPRP/exec";
        private WWWForm form;

        private Button btnGetData;
        private Text textPlayerName;

        private void Start()
        {
            textPlayerName = GameObject.Find("���a�W��").GetComponent<Text>();
            btnGetData = GameObject.Find("���o���a��ƫ��s").GetComponent<Button>();
            btnGetData.onClick.AddListener(GetGASData);
        }

        /// <summary>
        /// ���o GAS ���
        /// </summary>
        private void GetGASData()
        {
            form = new WWWForm();
            form.AddField("method", "���o");

            StartCoroutine(StarGetGASData());
        }

        private IEnumerator StarGetGASData()
        {
            //�s�W�����s�u�n�D(gasLink�A�����)
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink,form))
            {
                //���ݺ����s�u�n�D
                yield return www.SendWebRequest();
                //���a�W�� = �s�u�n�D�U������r�T��
                textPlayerName.text = www.downloadHandler.text;
            }
        }
    }
}

