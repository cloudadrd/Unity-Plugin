using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CTServiceSDK;

public class CTRewardedVideo : MonoBehaviour {
	#if UNITY_ANDROID
	private string slot_id = "1601";
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
		CTService.loadRewardVideoWithSlotId (slot_id); 
	}

	//set delegate
	void OnEnable() {
		setupDelegates();
	}

	//set delegate
	void setupDelegates(){
		CTService.rewardVideoLoadSuccess += CTRewardVideoLoadSuccess;
		CTService.rewardVideoLoadingFailed += CTRewardVideoLoadingFailed;
		CTService.rewardVideoDidStartPlaying += CTRewardVideoDidStartPlaying;
		CTService.rewardVideoDidFinishPlaying += CTRewardVideoDidFinishPlaying;
		CTService.rewardVideoDidClickRewardAd += CTRewardVideoDidClickRewardAd;
		CTService.rewardVideoWillLeaveApplication += CTRewardVideoWillLeaveApplication;
		CTService.rewardVideoJumpfailed += CTRewardVideoJumpfailed;
		CTService.rewardVideoAdRewarded += CTRewardVideoAdRewarded;
		CTService.rewardVideoClosed += CTRewardVideoClosed;
	}

	void OnDisable(){
		CTService.rewardVideoLoadSuccess -= CTRewardVideoLoadSuccess;
		CTService.rewardVideoLoadingFailed -= CTRewardVideoLoadingFailed;
		CTService.rewardVideoDidStartPlaying -= CTRewardVideoDidStartPlaying;
		CTService.rewardVideoDidFinishPlaying -= CTRewardVideoDidFinishPlaying;
		CTService.rewardVideoDidClickRewardAd -= CTRewardVideoDidClickRewardAd;
		CTService.rewardVideoWillLeaveApplication -= CTRewardVideoWillLeaveApplication;
		CTService.rewardVideoJumpfailed -= CTRewardVideoJumpfailed;
		CTService.rewardVideoAdRewarded -= CTRewardVideoAdRewarded;
		CTService.rewardVideoClosed -= CTRewardVideoClosed;
	}

	void OnDestroy(){
		//do not forget to call release, otherwise android platform will casue memory leak.
		CTService.release ();
	}

	//Notice: You should call this api as soon as you can. For example, call it in Start function.(not in awake, beacause we must call CTService.loadRequestGetCTSDKConfigBySlot_id first in camera awake function)
	//For convenience test, we add a button to click.
	void loadBtnClick(){
		//load rewardvideo ad
		CTService.loadRewardVideoWithSlotId (slot_id);
	}

	void playBtnClick(){
		//you can also use this api to check if rewearded video is ready.
		if (CTService.checkRewardVideoIsReady ()) {
			setReady (true, null);
			CTService.showRewardVideo (slot_id);
		}
		else
			Debug.Log ("CT Rewarded Video is not ready");
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
	void CTRewardVideoLoadSuccess(){
		Debug.Log ("U3D delegate, CTRewardVideoLoadSuccess");
		setReady (true, null);
	}

	//video load failure
	void CTRewardVideoLoadingFailed(string error){
		setReady (false, error);
		Debug.Log ("U3D delegate, CTRewardVideoLoadingFailed. " + error);
	}
		
	//start playing video
	void CTRewardVideoDidStartPlaying(){
		Debug.Log ("U3D delegate, CTRewardVideoDidStartPlaying");
	}

	//finish playing video
	void CTRewardVideoDidFinishPlaying(){
		Debug.Log ("U3D delegate, CTRewardVideoDidFinishPlaying");
	}

	//click ad, only for iOS
	void CTRewardVideoDidClickRewardAd(){
		Debug.Log ("U3D delegate, CTRewardVideoDidClickRewardAd");
	}
		
	//will leave Application, only for iOS
	void CTRewardVideoWillLeaveApplication(){
		Debug.Log ("U3D delegate, CTRewardVideoWillLeaveApplication");
	}
		
	//jump to AppStroe failed, only for iOS
	void CTRewardVideoJumpfailed(){
		Debug.Log ("U3D delegate, CTRewardVideoWillLeaveApplication");
	}

	//players get rewarded here
	void CTRewardVideoAdRewarded(string rewardVideoNameAndAmount){
		Debug.Log ("U3D delegate, CTRewardVideoAdRewarded, " + rewardVideoNameAndAmount);
	}

	//close video ad
	void CTRewardVideoClosed(){
		Debug.Log ("U3D delegate, CTRewardVideoClosed");
		setReady (false, "video play end");
	}
}
