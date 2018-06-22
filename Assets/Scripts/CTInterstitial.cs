using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CTServiceSDK;

public class CTInterstitial : MonoBehaviour {
	#if UNITY_ANDROID
	private string slot_id = "248";
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
		CTService.preloadInterstitialWithSlotId (slot_id); 
	}
	
	//set delegate
	void OnEnable() {
		setupDelegates();
	}

	//set delegate
	void setupDelegates(){
		CTService.interstitialLoadSuccess += CTInterstitialLoadSuccess;
		CTService.interstitialLoadFailed += CTInterstitialLoadingFailed;
		CTService.interstitialDidClickRewardAd += CTInterstitialDidClickRewardAd;
		CTService.interstitialClose += CTInterstitialClose;
	}

	void OnDisable(){
		CTService.interstitialLoadSuccess -= CTInterstitialLoadSuccess;
		CTService.interstitialLoadFailed -= CTInterstitialLoadingFailed;
		CTService.interstitialDidClickRewardAd -= CTInterstitialDidClickRewardAd;
		CTService.interstitialClose -= CTInterstitialClose;
	}

	void OnDestroy(){
		//do not forget to call release, otherwise android platform will casue memory leak.
		CTService.release ();
	}

	void setReady(bool isReady, string msg){
		if (isReady) {
			statusText.color = Color.green; 
			statusText.text = "isReadyToShow: Yes";
		} else {
			statusText.color = Color.red; 
			statusText.text = msg;
		}
	}

	void loadBtnClick(){
		//load Interstitial ad
		CTService.preloadInterstitialWithSlotId (slot_id);
		Debug.Log ("CT Interstitial loadBtnClick");
	}

	void showBtnClick(){
		//you can also use this api to check if Interstitial is ready.
		if (CTService.isInterstitialAvailable ()) {
			setReady (true, null);
			CTService.showInterstitial ();
		}
		else
			Debug.Log ("CT Interstitial is not ready");
	}


	/**
	 * 
	 * Interstitial delegate
	 * 
	 * 
	 * */
	void CTInterstitialLoadSuccess(){
		Debug.Log ("U3D delegate, CTInterstitialLoadSuccess");
		setReady (true, null);
	}

	void CTInterstitialLoadingFailed(string error){
		setReady (false, error);
		Debug.Log ("U3D delegate, CTInterstitialLoadingFailed. " + error);
	}

	//click ad, only for iOS
	void CTInterstitialDidClickRewardAd(){
		Debug.Log ("U3D delegate, CTInterstitialDidClickAd");
	}

	void CTInterstitialClose(){
		Debug.Log ("U3D delegate, CTInterstitialClose");
		setReady (false, @"isReadyToShow: NO");
	}
}
