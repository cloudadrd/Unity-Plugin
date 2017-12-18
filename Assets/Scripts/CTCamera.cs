using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CTServiceSDK;

public class CTCamera : MonoBehaviour {
	public string slot_id_android = "88408505";
	public string slot_id_ios = "82095565";

	void Awake () {
		//Debug.Log ("slot_id_ios: " + "82095565");
		CTService.loadRequestGetCTSDKConfigBySlot_id (slot_id_android);
	}
}
