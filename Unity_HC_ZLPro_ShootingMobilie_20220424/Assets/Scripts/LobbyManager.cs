using UnityEngine;

/// <summary>
/// 大廳管理器
/// 玩家按下對戰按鈕後開始匹配房間
/// </summary>
public class LobbyManager : MonoBehaviour
{
    //GameObject 遊戲物件:存放Unity場景內所有物件
    //SerializeFied 將私人欄位顯示在屬性面板上
    //Heder 標題，在屬性面板上顯示粗體字標題
    [SerializeField,Header("連線中畫面")]
    private GameObject goConnectView;

    //註解:說明
    //讓按鈕跟程式溝通之流程
    //1.提供公開的方法 public Method
    //2.按鈕在點擊 On Click 後設定呼叫此方法

    public void StartConnect()
    {
        print("開始連線...");

        //遊戲物件，啟動設定(布林值) - true 顯示，false 隱藏
        goConnectView.SetActive(true);
    }
    
}
