using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppoCamera : MonoBehaviour {
	#if UNITY_ANDROID
	private string slot_id = "72666429";
	#elif UNITY_IOS
	private string slot_id = "30769964";
	#endif

	public void Start()
	{
		NBMediation.Agent.init("h0RnIKknnCa58pStdiqCWctETWkA1QL2", new AdTimingInitListener());
	}
	class AdTimingInitListener : NBInitListener
	{
		public void onError(string message)    
		{        
			Debug.LogError(message);
		}
		public void onSuccess() 
		{
			Debug.Log("Init onSuccess");
		}
	}
}
