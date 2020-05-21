using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppoCamera : MonoBehaviour {
	#if UNITY_ANDROID
	private string slot_id = "kXDlKvOwFYf0inXBd65Pzo0vpF2utBim";
	#elif UNITY_IOS
	private string slot_id = "h0RnIKknnCa58pStdiqCWctETWkA1QL2";
	#endif

	public void Start()
	{
		NBMediation.Agent.init(slot_id, new AdTimingInitListener());
	}
	class AdTimingInitListener : NBInitListener
	{
		public void onError(string message)    
		{     
			Debug.LogError("UnityApp" + message);
		}
		public void onSuccess() 
		{
			Debug.Log("UnityApp Init onSuccess");
		}
	}
}
