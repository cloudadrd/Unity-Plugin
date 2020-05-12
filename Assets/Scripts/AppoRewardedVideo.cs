using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppoRewardedVideo : MonoBehaviour {
//	#if UNITY_ANDROID
//	private string slot_id = "72666429";
//	#elif UNITY_IOS
//	private string slot_id = "30769964";
//	#endif
	//notice: attach your UI objcet here
	public Button loadBtn;
	public Button playBtn;
	public Text statusText;

	void Start () {
		playBtn.onClick.AddListener (playBtnClick);
		loadBtn.onClick.AddListener (loadBtnClick);
		//Notice: load rewardvideo ad when you init UI.
		AdTiming.Agent.setRewardedVideoListener(new AdTimingRewardedVideoListener());
	}

	//set delegate
	class AdTimingRewardedVideoListener : AdtRewardedVideoListener
	{
		/// Invoked when rewarded video is available.
		public void OnRewardedVideoAvailabilityChanged(bool available)
		{
			Debug.Log("UnityApp RewardedVideo OnRewardedVideoAvailabilityChanged"+available);
		}
		/// Sent immediately when a rewarded video starts to play. 
		public void OnRewardedVideoAdStarted(string scene)
		{
			Debug.Log("UnityApp RewardedVideo OnRewardedVideoAdStarted:" + scene);
		}
		/// Sent immediately when a rewarded video has been showed.
		public void OnRewardedVideoAdShowed(string scene)
		{
			Debug.Log("UnityApp RewardedVideo OnRewardedVideoAdShowed:" + scene);
		}
		/// Sent after a rewarded video has failed to play..
		public void OnRewardedVideoAdShowFailed(string scene, string adTimingError)
		{
			Debug.LogError("UnityApp RewardedVideo OnRewardedVideoAdShowFailed:" + scene);
		}
		/// Sent after a rewarded video has been clicked.
		public void OnRewardedVideoAdClicked(string scene)
		{
			Debug.Log("UnityApp RewardedVideo OnRewardedVideoAdClicked:" + scene);
		}
		/// Sent after a rewarded video has been closed.
		public void OnRewardedVideoAdClosed(string scene)
		{
			Debug.Log("UnityApp RewardedVideo OnRewardedVideoAdClosed:" + scene);
		}
		/// Sent immediately when a rewarded video has been completed.
		public void OnRewardedVideoAdEnded(string scene)
		{
			Debug.Log("UnityApp RewardedVideo OnRewardedVideoAdEnded:" + scene);
		}
		/// Sent after a user has been granted a reward.
		public void OnRewardedVideoAdRewarded(string scene)
		{
			Debug.Log("UnityApp RewardedVideo OnRewardedVideoAdRewarded:" + scene);
		}
	} 

	//Notice: You should call this api as soon as you can. For example, call it in Start function.(not in awake, beacause we must call AppoService.loadRequestGetAppoSDKConfigBySlot_id first in camera awake function)
	//For convenience test, we add a button to click.
	void loadBtnClick(){
		//load rewardvideo ad
		if (AdTiming.Agent.isRewardedVideoReady () == true) {
			setReady (true, null);
		} else {
			setReady (false, null);
		}
	}

	void playBtnClick(){
		//you can also use this api to check if rewearded video is ready.
		if (AdTiming.Agent.isRewardedVideoReady())
		{
			AdTiming.Agent.showRewardedVideo();
		}
		else
			Debug.Log ("Appo Rewarded Video is not ready");
	}

	void setReady(bool isReady, string msg){
		if (isReady) {
			statusText.color = Color.green; 
			statusText.text = "isReadyToPlay: Yes";
		} else {
			statusText.color = Color.red; 
			statusText.text = msg;
		}
	}
}
