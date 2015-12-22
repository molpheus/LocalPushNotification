/*==================================================*/
/*!
 * @file LocalPushNotification.cs
 * @author toru yamaguchi
 * @date 2015/12/21
 * */
/*==================================================*/
/*--------------------------------------------------*/
/*
 * @brief using.
 * */
/*--------------------------------------------------*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*==================================================*/
/*!
 * @brief .
 * @note
 * 
 * Android
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * [java plugin] NativeLocalNotification. 
 * Local push notification Main AndroidManifest.xml add permissions and application
 * 
 * add main <application>
 * <receiver android:name="jp.co.zerodiv.localnotification.NotificationReceiver" android:process=":remote"/>
 * 
 * add permission
 * <uses-permission android:name="android.permission.INTERNET" />
 * <uses-permission android:name="android.permission.VIBRATE" />
 * <uses-permission android:name="android.permission.WAKE_LOCK" />
 * 
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * 
 * iOS
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * Unity LocalPushNotification service
 * [iOS 8+] It can not be used to not allow push
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * 
 * 
 * */
/*==================================================*/
public class LocalPushNotification
{
	public const int LowUniqID = 1;

	/// <summary> 制作したID. </summary>
	static public int createUniqID = LowUniqID;

	/// <summary> 制作したID. </summary>
	const string SaveKey = "LocalPushUniqID";

	/// <summary>
	/// .
	/// </summary>
	/// <returns> ユニークなID </returns>
	static public int createID()
	{
		createUniqID++;
		return createUniqID;
	}

	/// <summary>
	/// .
	/// </summary>
	static public void SaveUniqID()
	{
		PlayerPrefs.SetInt(SaveKey, createUniqID);
		PlayerPrefs.Save();
	}

	/// <summary>
	/// .
	/// </summary>
	static public void LoadUniqID()
	{
		if (PlayerPrefs.HasKey(SaveKey))
		{
			createUniqID = PlayerPrefs.GetInt(SaveKey);
			PlayerPrefs.DeleteKey(SaveKey);
			PlayerPrefs.Save();
		}
	}

	/// <summary>
	/// ローカルなプッシュ通知を行う.
	/// </summary>
	/// <param name="unixtime"> [unixtime]秒数後に通知を行う. </param>
	/// <param name="content_title"> タイトル名. </param>
	/// <param name="content_text"> 内容. </param>
	static public void SendLocalPush(long unixtime, string content_title, string content_text)
	{
		SendLocalPush(unixtime, createID(), content_title, content_text);
	}

	/// <summary>
	/// ローカルなプッシュ通知の設定を行う.
	/// </summary>
	/// <param name="unixtime"> [unixtime]秒数後に通知を行う. </param>
	/// <param name="primary_key"> ユニークな値を入れる. </param>
	/// <param name="content_title"> タイトル名. </param>
	static void SendLocalPush(long unixtime, int primary_key, string content_title)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaObject m_plugin = new AndroidJavaObject("jp.co.zerodiv.localnotification.LocalPushNotification");
		if(m_plugin != null)
		{ m_plugin.Call("sendNotification", unixtime, primary_key, content_title); }
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
		SendLocalPushiOS(unixtime, primary_key, content_title);
#endif
	}

	/// <summary>
	/// ローカルなプッシュ通知の設定を行う.
	/// </summary>
	/// <param name="unixtime"> [unixtime]秒数後に通知を行う. </param>
	/// <param name="primary_key"> ユニークな値を入れる. </param>
	/// <param name="content_title"> タイトル名. </param>
	/// <param name="content_text"> 内容. </param>
	static void SendLocalPush(long unixtime, int primary_key, string content_title, string content_text)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaObject m_plugin = new AndroidJavaObject("jp.co.zerodiv.localnotification.LocalPushNotification");
		if (m_plugin != null)
		{ m_plugin.Call("sendNotification", unixtime, primary_key, content_title, content_text); }
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
		SendLocalPushiOS(unixtime, primary_key, content_text);
#endif
	}

	/// <summary>
	/// ローカルなプッシュ通知の設定を行う.
	/// </summary>
	/// <param name="unixtime"> [unixtime]秒数後に通知を行う. </param>
	/// <param name="primary_key"> ユニークな値を入れる. </param>
	/// <param name="ticker"> ティッカー表示部分. </param>
	/// <param name="content_title"> タイトル名. </param>
	/// <param name="content_text"> 内容. </param>
	static void SendLocalPush(long unixtime, int primary_key, string ticker, string content_title, string content_text)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaObject m_plugin = new AndroidJavaObject("jp.co.zerodiv.localnotification.LocalPushNotification");
		if (m_plugin != null)
		{ m_plugin.Call("sendNotification", unixtime, primary_key, ticker, content_title, content_text); }
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
		SendLocalPushiOS(unixtime, primary_key, content_text);

#endif
	}

	/// <summary>
	/// ローカルなプッシュ通知の設定を行う.
	/// </summary>
	/// <param name="unixtime"> [unixtime]秒数後に通知を行う. </param>
	/// <param name="primary_key"> ユニークな値を入れる. </param>
	/// <param name="ticker"> ティッカー表示部分. </param>
	/// <param name="content_title"> タイトル名. </param>
	/// <param name="content_text"> 内容. </param>
	/// <param name="sound_path"> 鳴らすサウンドのパス. </param>
	static void SendLocalPush(long unixtime, int primary_key, string ticker, string content_title, string content_text, string sound_path)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaObject m_plugin = new AndroidJavaObject("jp.co.zerodiv.localnotification.LocalPushNotification");
		if (m_plugin != null)
		{ m_plugin.Call("sendNotification", unixtime, primary_key, ticker, content_title, content_text, sound_path); }
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
		SendLocalPushiOS(unixtime, primary_key, content_text);
#endif
	}

	/// <summary>
	/// ローカルなプッシュ通知の削除を行う.
#if UNITY_ANDROID
	/// 削除を行われるのは、通知を行っていない分のみ.
#endif
	/// </summary>
	/// <param name="primary_key"></param>
	static public void ClearNotification(int primary_key)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaObject m_plugin = new AndroidJavaObject("jp.co.zerodiv.localnotification.LocalPushNotification");
		if (m_plugin != null)
		{ m_plugin.Call("clearNotification", primary_key); }
#elif (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
		LocalNotification notification = NotificationServices.GetLocalNotification(primary_key);
		if(notification != null){
			NotificationServices.CancelLocalNotification(notification);
		}
#endif
	}

#if (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
	static public void SendLocalPushiOS(long unixtime, string context_text)
	{
		SendLocalPushiOS (unixtime, createID(), context_text);
	}

	static public void SendLocalPushiOS(long unixtime, int primary_key, string context_text)
	{
		LocalNotification notification = new LocalNotification ();
		notification.applicationIconBadgeNumber = primary_key;
		notification.fireDate = System.DateTime.Now.AddSeconds (unixtime);
		notification.alertBody = context_text;
		NotificationServices.ScheduleLocalNotification (notification);
	}
#endif

	static public void ClearBadge()
	{
#if (UNITY_IOS || UNITY_IPHONE)
		LocalNotification l = new LocalNotification();
		l.applicationIconBadgeNumber  = -1;		
		NotificationServices.PresentLocalNotificationNow(l);

		NotificationServices.CancelAllLocalNotifications();
		NotificationServices.ClearLocalNotifications();
#endif

	}

	static public void Initization()
	{
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
