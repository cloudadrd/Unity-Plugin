using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppoBanner : MonoBehaviour {
	#if UNITY_ANDROID
	private string slot_id = "123";
	#elif UNITY_IOS
	private string slot_id = "456";
	#endif

	// Use this for initialization
	void Start () {

	}

	void OnDestroy () {

	}
}
