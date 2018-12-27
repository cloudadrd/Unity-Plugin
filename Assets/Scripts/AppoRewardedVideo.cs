using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AppoServiceSDK;

public class AppoRewardedVideo : MonoBehaviour {
	#if UNITY_ANDROID
	private string slot_id = "72666429";
	#elif UNITY_IOS
	private string slot_id = "30769964";
	#endif
	//notice: attach your UI objcet here
	public Button loadBtn;
	public Button playBtn;
	public Text statusText;

	void Start () {
		playBtn.onClick.AddListener (playBtnClick);
		loadBtn.onClick.AddListener (loadBtnClick);
		//Notice: load rewardvideo ad when you init UI.
		AppoService.loadRewardVideoWithSlotId (slot_id); 
	}

	//set delegate
	void OnEnable() {
		AppoService.rewardVideoLoadSuccess += AppoRewardVideoLoadSuccess;
		AppoService.rewardVideoLoadingFailed += AppoRewardVideoLoadingFailed;
		AppoService.rewardVideoDidStartPlaying += AppoRewardVideoDidStartPlaying;
		AppoService.rewardVideoDidFinishPlaying += AppoRewardVideoDidFinishPlaying;
		AppoService.rewardVideoDidClickRewardAd += AppoRewardVideoDidClickRewardAd;
		AppoService.rewardVideoWillLeaveApplication += AppoRewardVideoWillLeaveApplication;
		AppoService.rewardVideoJumpfailed += AppoRewardVideoJumpfailed;
		AppoService.rewardVideoAdRewarded += AppoRewardVideoAdRewarded;
		AppoService.rewardVideoClosed += AppoRewardVideoClosed;
	}

	void OnDisable(){
		AppoService.rewardVideoLoadSuccess -= AppoRewardVideoLoadSuccess;
		AppoService.rewardVideoLoadingFailed -= AppoRewardVideoLoadingFailed;
		AppoService.rewardVideoDidStartPlaying -= AppoRewardVideoDidStartPlaying;
		AppoService.rewardVideoDidFinishPlaying -= AppoRewardVideoDidFinishPlaying;
		AppoService.rewardVideoDidClickRewardAd -= AppoRewardVideoDidClickRewardAd;
		AppoService.rewardVideoWillLeaveApplication -= AppoRewardVideoWillLeaveApplication;
		AppoService.rewardVideoJumpfailed -= AppoRewardVideoJumpfailed;
		AppoService.rewardVideoAdRewarded -= AppoRewardVideoAdRewarded;
		AppoService.rewardVideoClosed -= AppoRewardVideoClosed;
	}

	void OnDestroy(){
		//do not forget to call release, otherwise android platform will casue memory leak.
		AppoService.release ();
	}

	//Notice: You should call this api as soon as you can. For example, call it in Start function.(not in awake, beacause we must call AppoService.loadRequestGetAppoSDKConfigBySlot_id first in camera awake function)
	//For convenience test, we add a button to click.
	void loadBtnClick(){
		//load rewardvideo ad
		AppoService.loadRewardVideoWithSlotId (slot_id);
	}

	void playBtnClick(){
		//you can also use this api to check if rewearded video is ready.
		if (AppoService.checkRewardVideoIsReady ()) {
			setReady (true, null);
			AppoService.showRewardVideo (slot_id);
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
		
	/**
	 * 
	 * reward video delegate
	 * 
	 * 
	 * */

	//video load success. 
	//Do not show reward video in the function, for android sdk preloads ads, may call this function several times.
	void AppoRewardVideoLoadSuccess(){
		Debug.Log ("U3D delegate, AppoRewardVideoLoadSuccess");
		setReady (true, null);
	}

	//video load failure
	void AppoRewardVideoLoadingFailed(string error){
		setReady (false, error);
		Debug.Log ("U3D delegate, AppoRewardVideoLoadingFailed. " + error);
	}
		
	//start playing video
	void AppoRewardVideoDidStartPlaying(){
		Debug.Log ("U3D delegate, AppoRewardVideoDidStartPlaying");
	}

	//finish playing video
	void AppoRewardVideoDidFinishPlaying(){
		Debug.Log ("U3D delegate, AppoRewardVideoDidFinishPlaying");
	}

	//click ad, only for iOS
	void AppoRewardVideoDidClickRewardAd(){
		Debug.Log ("U3D delegate, AppoRewardVideoDidClickRewardAd");
	}
		
	//will leave Application, only for iOS
	void AppoRewardVideoWillLeaveApplication(){
		Debug.Log ("U3D delegate, AppoRewardVideoWillLeaveApplication");
	}
		
	//jump to AppStroe failed, only for iOS
	void AppoRewardVideoJumpfailed(){
		Debug.Log ("U3D delegate, AppoRewardVideoWillLeaveApplication");
	}

	//players get rewarded here
	void AppoRewardVideoAdRewarded(string rewardVideoNameAndAmount){
		Debug.Log ("U3D delegate, AppoRewardVideoAdRewarded, " + rewardVideoNameAndAmount);
	}

	//close video ad
	void AppoRewardVideoClosed(){
		Debug.Log ("U3D delegate, AppoRewardVideoClosed");
		setReady (false, "video play end");
	}
}
