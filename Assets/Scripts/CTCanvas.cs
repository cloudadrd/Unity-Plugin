using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CTServiceSDK;

public class CTCanvas : MonoBehaviour {
	public string slot_id_android = "88408505";
	public string slot_id_ios = "82095565";
	public Button playBtn;
	private bool isRewardVideoAvalable = false;
	void Awake () {
		//CTService.loadRequestGetCTSDKConfigBySlot_id (slot_id_android);
	}

	void Start () {
		playBtn.onClick.AddListener (playBtnClick);
		//set delegate
		setupDelegates ();
		//load rewardvideo ad
		CTService.loadRewardVideoWithSlotId (slot_id_android);
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

	void playBtnClick(){
		//show reward video if it's ready
		if(isRewardVideoAvalable)
			CTService.showRewardVideo (slot_id_android);
	}
		
	/**
	 * 
	 * reward video delegate
	 * 
	 * delegate method names should be the same as follows
	 * 
	 * */

	//video load success. 
	//Do not show reward video in the function, for android sdk preloads ads, may call this function several times.
	void CTRewardVideoLoadSuccess(){
		Debug.Log ("U3D delegate, CTRewardVideoLoadSuccess");
		isRewardVideoAvalable = true;
	}

	//video load failure
	void CTRewardVideoLoadingFailed(string error){
		Debug.Log ("U3D delegate, CTRewardVideoLoadingFailed");
		Debug.Log (error);
		isRewardVideoAvalable = false;
	}
		
	//start playing video
	void CTRewardVideoDidStartPlaying(){
		Debug.Log ("U3D delegate, CTRewardVideoDidStartPlaying");
	}

	//finish playing video
	void CTRewardVideoDidFinishPlaying(){
		Debug.Log ("U3D delegate, CTRewardVideoDidFinishPlaying");
	}

	//click ad
	void CTRewardVideoDidClickRewardAd(){
		Debug.Log ("U3D delegate, CTRewardVideoDidClickRewardAd");
	}
		
 	//will leave Application
	void CTRewardVideoWillLeaveApplication(){
		Debug.Log ("U3D delegate, CTRewardVideoWillLeaveApplication");
	}
		
	//jump to AppStroe failed
	void CTRewardVideoJumpfailed(){
		Debug.Log ("U3D delegate, CTRewardVideoWillLeaveApplication");
	}

	//get rewardvideo message
	void CTRewardVideoAdRewarded(string rewardVideoNameAndAmount){
		Debug.Log ("U3D delegate, CTRewardVideoAdRewarded");
		Debug.Log (rewardVideoNameAndAmount);
	}

	//close video ad
	void CTRewardVideoClosed(){
		Debug.Log ("U3D delegate, CTRewardVideoClosed");
	}
}
