using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CTServiceSDK;

public class CTCamera : MonoBehaviour {
	#if UNITY_ANDROID
	private string slot_id = "248";
	#elif UNITY_IOS
	private string slot_id = "30769964";
	#endif

	void Awake () {
		CTService.loadRequestGetCTSDKConfigBySlot_id (slot_id);
		CTService.uploadConsent ("yes", "GDPR");
	}
}
