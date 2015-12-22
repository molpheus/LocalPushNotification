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
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
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
