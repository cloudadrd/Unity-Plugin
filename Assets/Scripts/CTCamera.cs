using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CTServiceSDK;

public class CTCamera : MonoBehaviour {
	#if UNITY_ANDROID
	public string slot_id = "88408505";
	#elif UNITY_IOS
	public string slot_id = "82095565";
	#endif

	void Awake () {
		CTService.loadRequestGetCTSDKConfigBySlot_id (slot_id);
	}
}
