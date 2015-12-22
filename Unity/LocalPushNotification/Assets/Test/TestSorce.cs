/*==================================================*/
/*!
 * @file TestSorce.cs
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

/*==================================================*/
/*!
 * @brief .
 * @note
 *
 * */
/*==================================================*/
public class TestSorce : MonoBehaviour
{
	#region EVENT
	/// <summary> . </summary>
	public delegate void PushCall();
	/// <summary> Push Call Action Event. </summary>
	public event PushCall PushCallAction;

	#endregion

#if true//UNITY_EDITOR

	void Awake()
	{
		LocalPushNotification.Initization ();
		PushCallAction += call;
	}

	void OnDestroy()
	{
		PushCallAction -= call;
	}

	/// <summary>
	/// .
	/// </summary>
	void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, 100, 100), "SetAlarm"))
		{
			LocalPushNotification.SendLocalPush(10, "Local Push Title", "Local Push Text");
		}
	}

	/// <summary>
	/// .
	/// </summary>
	void call()
	{
		for (int i = 0; i < 10; i++)
		{
			LocalPushNotification.SendLocalPush(10 + i, "Local Push Title" + i, "Local Push Text" + i);
		}
	}
#endif

	/// <summary>
	/// .
	/// </summary>
	/// <param name="pauseStatus"></param>
	void OnApplicationPause(bool pauseStatus)
	{
		bool pause = pauseStatus;
		Debug.Log("pause action -> " + pause);

		if (pause == true)
		{
			PushCallAction.Invoke();
			LocalPushNotification.SaveUniqID();
		}
		else
		{
			LocalPushNotification.LoadUniqID();
			while (LocalPushNotification.createUniqID > LocalPushNotification.LowUniqID)
			{
				LocalPushNotification.ClearNotification(LocalPushNotification.createUniqID);
				LocalPushNotification.createUniqID--;
			}
			LocalPushNotification.ClearBadge();
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