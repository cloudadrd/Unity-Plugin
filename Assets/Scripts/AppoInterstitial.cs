using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AppoServiceSDK;

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
		AppoService.preloadInterstitialWithSlotId (slot_id); 
	}
	
	//set delegate
	void OnEnable() {
		setupDelegates();
	}

	//set delegate
	void setupDelegates(){
		AppoService.interstitialLoadSuccess += AppoInterstitialLoadSuccess;
		AppoService.interstitialLoadFailed += AppoInterstitialLoadingFailed;
		AppoService.interstitialDidClickRewardAd += AppoInterstitialDidClickRewardAd;
		AppoService.interstitialClose += AppoInterstitialClose;
	}

	void OnDisable(){
		AppoService.interstitialLoadSuccess -= AppoInterstitialLoadSuccess;
		AppoService.interstitialLoadFailed -= AppoInterstitialLoadingFailed;
		AppoService.interstitialDidClickRewardAd -= AppoInterstitialDidClickRewardAd;
		AppoService.interstitialClose -= AppoInterstitialClose;
	}

	void OnDestroy(){
		//do not forget to call release, otherwise android platform will casue memory leak.
		AppoService.release ();
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
		AppoService.preloadInterstitialWithSlotId (slot_id);
		Debug.Log ("Appo Interstitial loadBtnClick");
	}

	void showBtnClick(){
		//you can also use this api to check if Interstitial is ready.
		if (AppoService.isInterstitialAvailable ()) {
			setReady (true, null);
			AppoService.showInterstitial ();
		}
		else
			Debug.Log ("Appo Interstitial is not ready");
	}


	/**
	 * 
	 * Interstitial delegate
	 * 
	 * 
	 * */
	void AppoInterstitialLoadSuccess(){
		Debug.Log ("U3D delegate, AppoInterstitialLoadSuccess");
		setReady (true, null);
	}

	void AppoInterstitialLoadingFailed(string error){
		setReady (false, error);
		Debug.Log ("U3D delegate, AppoInterstitialLoadingFailed. " + error);
	}

	//click ad, only for iOS
	void AppoInterstitialDidClickRewardAd(){
		Debug.Log ("U3D delegate, AppoInterstitialDidClickAd");
	}

	void AppoInterstitialClose(){
		Debug.Log ("U3D delegate, AppoInterstitialClose");
		setReady (false, @"isReadyToShow: NO");
	}
}
