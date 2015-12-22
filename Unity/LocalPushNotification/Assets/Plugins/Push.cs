/*==================================================*/
/*!
 * @file PushNotification.cs
 * @date 2015/12/22
 * */
/*==================================================*/
/*--------------------------------------------------*/
/*
 * @brief using.
 * */
/*--------------------------------------------------*/
using UnityEngine;
using System.Collections;

/*==================================================*/
/*!
 * @brief .
 * @note
 *
 * */
/*==================================================*/
[RequireComponent(typeof(RemotePushNotification))]
public class Push : MonoBehaviour
{
	#region EVENT
	/// <summary> . </summary>
	public delegate void PushCall();
	/// <summary> Push Call Action Event. </summary>
	static public event PushCall PushCallAction;

	#endregion

	// 取得してきたTokenを保持する変数.
	string NativeToken { get; set; }

	string deviceToken;
	bool EndGetToken = false;

	/// <summary> 外部参照用変数. </summary>
	static public string tokenID = "";

	/*--------------------------------------------------*/
	/*!
	 * @brief 開始時にランダム順番で呼ばれる.
	 * */
	/*--------------------------------------------------*/
	void Awake () {
#if (UNITY_IOS || UNITY_IPHONE)
		NotificationServices.RegisterForLocalNotificationTypes(
			LocalNotificationType.Alert |
			LocalNotificationType.Badge |
			LocalNotificationType.Sound);
		NotificationServices.RegisterForRemoteNotificationTypes(
			RemoteNotificationType.Alert |
			RemoteNotificationType.Badge |
			RemoteNotificationType.Sound); 
#endif
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief Awake()がすべて呼ばれた後に呼ばれる.
	 * */
	/*--------------------------------------------------*/
	void Start()
	{
		EndGetToken = false;
#if UNITY_ANDROID
		RemotePushNotification.GetDeviceToken(this.name, "CallbackGetDeviceToken");
#endif
	}

	/*--------------------------------------------------*/
	/*!
	 * @brief トークン取得のコールバック（プラグインで取得後）.
	 * */
	/*--------------------------------------------------*/
	void CallbackGetDeviceToken(string strText)
	{
		if (string.IsNullOrEmpty(strText))
		{
			// 取得の失敗.
			return;
		}

		NativeToken = strText;
		tokenID = strText;
		// 取得の成功.

		EndGetToken = true;
		UpdateDispDeviceToken();
	}

	/*--------------------------------------------------*/
	/// <summary>
	/// 取得したデバイストークンを表示する.
	/// </summary>
	/*--------------------------------------------------*/
	public void UpdateDispDeviceToken()
	{
		tokenID = NativeToken;
	}

#if (UNITY_IPHONE || UNITY_IOS) && UNITY_EDITOR

	/*--------------------------------------------------*/
	/*!
	 * @brief 毎フレーム必ず呼ばれる.
	 * */
	/*--------------------------------------------------*/
	void Update () {
		if (!EndGetToken) {
			byte[] token = NotificationServices.deviceToken;
			if (token != null) {
				// send token to a provider
				string hexToken = System.BitConverter.ToString(token).Replace("-", "");
				Debug.Log("get hexToken : " + hexToken);
				NativeToken = hexToken;
				UpdateDispDeviceToken ();
				EndGetToken = true;
			}
		}
	}

#endif

	/*--------------------------------------------------*/
	/*! 
	 * @brief	アプリが復帰した時.
	 * @param	pause	:	true.
	 * @return	なし
	 * */
	/*--------------------------------------------------*/
	void OnApplicationPause(bool pauseStatus)
	{
		// バッジ削除.
#if (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
		RemotePushNotification.Clean();
		{
			LocalNotification notification = new LocalNotification();
			NotificationServices.PresentLocalNotificationNow(notification);

			NotificationServices.CancelAllLocalNotifications();
			NotificationServices.ClearLocalNotifications();
			NotificationServices.ClearRemoteNotifications();
		}
#elif UNITY_ANDROID
		LocalPushNotification.LoadUniqID();
		while (LocalPushNotification.createUniqID > LocalPushNotification.LowUniqID)
		{
			LocalPushNotification.ClearNotification(LocalPushNotification.createUniqID);
			LocalPushNotification.createUniqID--;
		}
#endif
		if (pauseStatus == true)
		{
			PushCallAction.Invoke();
			LocalPushNotification.SaveUniqID();
		}
		else
		{
			LocalPushNotification.createUniqID = LocalPushNotification.LowUniqID;
		}
	}
}

/*==================================================*/
/*
 * @brief EOF.
 * */
/*==================================================*/

/*++++++++++++++++++++++++++++++++++++++++++++++++++
 * 飾り置き場.
 ++++++++++++++++++++++++++++++++++++++++++++++++++*/
/*==================================================*/
/*
 * @brief 大きなくくりの時（クラス宣言とか？）.
 * */
/*==================================================*/
/*--------------------------------------------------*/
/*
 * @brief 小さなくくりの時（関数宣言とか？）.
 * */
/*--------------------------------------------------*/
