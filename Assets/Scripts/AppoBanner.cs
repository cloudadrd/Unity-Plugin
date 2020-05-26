using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppoBanner : MonoBehaviour {
	#if UNITY_ANDROID
	private string slot_id = "259";
	#elif UNITY_IOS
	private string slot_id = "250";
	#endif

	// Use this for initialization
	void Start () {
		NBMediation.Agent.loadBanner (slot_id);
		StartCoroutine (ShowBannerWhenReady ());
	}

	IEnumerator ShowBannerWhenReady () {
		while (!NBMediation.Agent.isBannerReady (slot_id)) {
			yield return new WaitForSeconds (0.5f);
		}
		NBMediation.Agent.showBanner (slot_id);
	}

	void OnDestroy () {
		NBMediation.Agent.hideBanner (slot_id, true);
	}
}
