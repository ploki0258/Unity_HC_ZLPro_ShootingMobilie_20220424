using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;  //引用Phton Pun  API
using Photon.Realtime; //引用Phton 即時  API

/// <summary>
/// 大廳管理器
/// 玩家按下對戰按鈕後開始匹配房間
/// </summary>
/// MonoBehaviourPunCallbacks 連線功能能回乎類別
/// 例如:登入大廳後回乎你指定的程式
public class LobbyManager : MonoBehaviourPunCallbacks
{
	//GameObject 遊戲物件:存放Unity場景內所有物件
	//SerializeFied 將私人欄位顯示在屬性面板上
	//Heder 標題，在屬性面板上顯示粗體字標題
	[SerializeField, Header("連線中畫面")]
	private GameObject goConnectView;
	[SerializeField, Header("對戰按鈕")]
	private Button btnBattle;
	[SerializeField, Header("連線人數")]
	private Text textCountPlayer;
	[SerializeField, Header("連線最大人數")]
	private byte maxPlayer = 3;

	//喚醒事件:播放遊戲時執行一次。初始化設定
	private void Awake()
	{
		//螢幕.設定解析度(寬，高，是全螢幕)
		Screen.SetResolution(1024, 576, false);

		//Photon 連線的連線使用設定
		PhotonNetwork.ConnectUsingSettings();
	}

	//override 允許複寫繼承的父類類別成員
	//連線至控制台，在ConnectUsingSettinigs 執行後會自動連線
	public override void OnConnectedToMaster()
	{
		base.OnConnectedToMaster();
		print("<color=yellow>1. 已經進入控制台</color>");

		//Photon 連線.加入大廳
		PhotonNetwork.JoinLobby();
	}

	//連線至大廳成功後會執行此方法
	public override void OnJoinedLobby()
	{
		base.OnJoinedLobby();
		print("<color=yellow>2. 已經進入大廳</color>");

		//對戰按鈕.互動=啟動
		btnBattle.interactable = true;
	}

	//註解:說明
	//讓按鈕跟程式溝通之流程
	//1.提供公開的方法 public Method
	//2.按鈕在點擊 On Click 後設定呼叫此方法

	//開始連線對戰
	public void StartConnect()
	{
		print("<color=yellow>3. 開始連線</color>");

		//遊戲物件，啟動設定(布林值) - true 顯示，false 隱藏
		goConnectView.SetActive(true);

		//Photon連線的加入隨機房間
		PhotonNetwork.JoinRandomRoom();
	}
	//加入隨機房間失敗...
	//1. 連線品質差導致失敗
	//2. 還沒有房間
	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		base.OnJoinRandomFailed(returnCode, message);
		print("<color=red>4. 加入隨機房間失敗</color>");
		RoomOptions ro = new RoomOptions(); //新增房間設定物件
		ro.MaxPlayers = maxPlayer;          //指定房間最大人數
		PhotonNetwork.CreateRoom("", ro);   //建立房間並給予房間物件
	}

	//加入房間
	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();
		print("<color=yellow> 5. 開房者進入房間</color>");
		int currentCount = PhotonNetwork.CurrentRoom.PlayerCount; //當前房間人數
		int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;      //當前房間最大人數

		textCountPlayer.text = "連線人數" + currentCount + " / " + maxCount;

		LoadGameScene(currentCount, maxCount);
	}

	//其他玩家進入房間
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		base.OnPlayerEnteredRoom(newPlayer);
		print("<color=yellow> 6. 玩家進入房間</color>");
		int currentCount = PhotonNetwork.CurrentRoom.PlayerCount; //當前房間人數
		int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;      //當前房間最大人數

		textCountPlayer.text = "連線人數" + currentCount + " / " + maxCount;

		LoadGameScene(currentCount, maxCount);
	}

	/// <summary>
	/// 載入遊戲場景
	/// </summary>
	private void LoadGameScene(int currentCount, int maxCount)
	{
		//Clean Code 乾淨程式
		//1.不重複 - 問題:影響維護性
		//當進入房間的玩家 等於 最大房間人數時 就進入遊戲場景
		if (currentCount == maxCount)
		{
			//透過 Photon 連線讓玩家 仔入指定場景(場景名稱)
			//場景必須放在 Build Settings 內
			PhotonNetwork.LoadLevel("遊戲場景");
		}
	}
}