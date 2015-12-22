/*==================================================*/
/*!
 * @file RemotePushNotification.cs
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
using System.Runtime.InteropServices;

/*==================================================*/
/*!
 * @brief .
 * @note
 *
 *
 * ${ApplicationID} = application identifier
 *
 * Android
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * [jar plugin] androidplugin_push. 
 * Remote push notification Main AndroidManifest.xml add permissions and application
 * 
 * add main <application>
 * 
 * <receiver android:name="plugin.push.GcmBroadcastReceiver" android:permission="com.google.android.c2dm.permission.SEND">
 * <intent-filter>
 *	<action android:name="com.google.android.c2dm.intent.RECEIVE" />
 *	<category android:name="${ApplicationID}" />
 * </intent-filter>
 * </receiver>
 * <service android:name="plugin.push.GcmIntentService">
 * </service>
 * 
 * add permission
 * 
 * <permission android:name="${ApplicationID}.permission.C2D_MESSAGE" android:protectionLevel="signature" />
 * <uses-permission android:name="${ApplicationID}.permission.C2D_MESSAGE" />
 * <uses-permission android:name="com.google.android.c2dm.permission.RECEIVE" />
 * <uses-permission android:name="android.permission.GET_ACCOUNTS" />
 * 
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * 
 * iOS
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * [mm Plugin] PushNotificationDelegate.
 * 
 * Unity LocalPushNotification service
 * [iOS 8+] It can not be used to not allow push
 * 
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * 
 * */
/*==================================================*/
public class RemotePushNotification : MonoBehaviour
{
	// GoogleDeveloperCenterで取得したプロジェクト番号を入力.
	public static string GCM_PROJECT_NUMBER = "993610505289";//"993610505289";

	/*------------------------------------------------------------------------------------------*/
	/*! 
	 * @brief	Push通知のデバイストークン	取得
	 * @param	string objectName		オブジェクト名
	 * @param	string functionName		コールバック関数名
	 * @return	なし
	 * */
	/*------------------------------------------------------------------------------------------*/
	public static void GetDeviceToken(string objectName, string functionName)
	{
#if UNITY_EDITOR
		Debug.Log("GetDeviceToken( string , string ) : UNITY_EDITOR");
#elif UNITY_ANDROID
		Debug.Log("ts.com: NativePushPlugin.GetDeviceToken");
		/* Android上のUnityPlayerクラスのオブジェクトを取得 */
		AndroidJavaClass cUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

		/* 現在実行中のActivityのオブジェクトを取得 */
		AndroidJavaObject activity = cUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		/* 作成したプラグインのAlertDialogPluginクラスを取得 */
		AndroidJavaClass plugin = new AndroidJavaClass("plugin.push.AndroidPlugin_Push");

		/* メソッドを実行 */

		//plugin.CallStatic("GetGcmRegistrarID", activity, objectName, functionName, RemotePushNotification.GCM_PROJECT_NUMBER);

		Debug.Log("ts.com: NativePushPlugin.GetDeviceToken End");
#else
		Debug.Log("GetDeviceToken( string , string ) : no plugin");
#endif
	}

#if (UNITY_IOS || UNITY_IPHONE)
	[DllImport("__Internal")]
	static extern void CleanIconBadge_();
#endif

	/*------------------------------------------------------------------------------------------*/
	/*! 
	 * @brief Push通知で付くバッジを削除する.
	 * */
	/*------------------------------------------------------------------------------------------*/
	public static void Clean()
	{
#if (UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR
		CleanIconBadge_();
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
