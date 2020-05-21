using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppoInterstitial : MonoBehaviour {
	#if UNITY_ANDROID
	private string slot_id = "11546588";
	#elif UNITY_IOS
	private string slot_id = "53479848";
	#endif
	//notice: attach your UI objcet here
	public Button loadBtn;
	public Button showBtn;
	public Text statusText;

	// Use this for initialization
	void Start () {
		showBtn.onClick.AddListener (showBtnClick);
		loadBtn.onClick.AddListener (loadBtnClick);
		//Notice: load Interstitial ad when you init UI.
		NBMediation.Agent.setInterstitialListener(new InterstitialAdListener());
	}

	//set delegate
	class InterstitialAdListener : NBInterstitialAdListener
	{
		/// Invoked when rewarded video is available.
		public void OnInterstitialAdAvailabilityChanged(bool available)
		{
			Debug.Log("UnityApp Interstitial OnInterstitialAdAvailabilityChanged"+available);
		}
		/// Sent immediately when a rewarded video has been showed.
		public void OnInterstitialAdShowed(string scene)
		{
			Debug.Log("UnityApp Interstitial OnInterstitialAdShowed:" + scene);
		}
		/// Sent after a rewarded video has failed to play..
		public void OnInterstitialAdShowFailed(string scene, string error)
		{
			Debug.LogError("UnityApp Interstitial OnInterstitialAdShowFailed:" + scene);
		}
		/// Sent after a rewarded video has been clicked.
		public void OnInterstitialAdClicked(string scene)
		{
			Debug.Log("UnityApp Interstitial OnInterstitialAdClicked:" + scene);
		}
		/// Sent after a rewarded video has been closed.
		public void OnInterstitialAdClosed(string scene)
		{
			Debug.Log("UnityApp Interstitial OnInterstitialAdClosed:" + scene);
		}

	} 

	void setReady(bool isReady, string msg){
		if (isReady) {
			statusText.color = Color.green; 
			statusText.text = "isReadyToShow: Yes";
		} else {
			statusText.color = Color.red; 
			statusText.text = "error";
		}
	}

	void loadBtnClick(){
		//load Interstitial ad
		if (NBMediation.Agent.isInterstitialReady () == true) {
			setReady (true, null);
		} else {
			setReady (false, null);
		}
		Debug.Log ("Appo Interstitial loadBtnClick");
	}

	void showBtnClick(){
		//you can also use this api to check if Interstitial is ready.
		if (NBMediation.Agent.isInterstitialReady()) {
			NBMediation.Agent.showInterstitial ();
		}
		else
			Debug.Log ("Appo Interstitial is not ready");
	}
}
