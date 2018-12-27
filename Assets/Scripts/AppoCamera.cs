using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppoServiceSDK;

public class AppoCamera : MonoBehaviour {
	#if UNITY_ANDROID
	private string slot_id = "72666429";
	#elif UNITY_IOS
	private string slot_id = "30769964";
	#endif

	void Awake () {
		AppoService.initSDK (slot_id);
		AppoService.uploadConsent ("yes", "GDPR");
	}
}
