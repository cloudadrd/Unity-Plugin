﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;  
using UnityEngine;
using System.Threading;

namespace CTServiceSDK {
	public class CTService : MonoBehaviour {
		
		private static string delegateName = "CTServiceDelegate";
		public const string version = "1.10";

	#if UNITY_ANDROID// && !UNITY_EDITOR
		private static AndroidJavaClass ctClass = null;
		private static string SDK_CLASS = "com.cloudtech.ads.core.CTService";
		private static AndroidJavaClass cmClass = null;
		private static string CM_CLASS = "com.cloudtech.videoads.unity.CTUnityService";
		private static AndroidJavaObject ctVideo = null;
		private static AndroidJavaClass unityPlayerClass = null;
		private static string UNITY_CLASS = "com.unity3d.player.UnityPlayer";
		private static AndroidJavaObject currentActivity = null;
		private static AndroidJavaObject adsClass = null;
		private static string CM_ADS_CLASS = "com.cloudtech.ads.unity.CTUnityService";
	#elif UNITY_IOS // && !UNITY_EDITOR
		[DllImport ("__Internal")]private static extern void CSetDelegateObjName(string delegateName);
		[DllImport ("__Internal")]private static extern void CLoadRequestGetCTSDKConfigBySlot_id(string slot_id);
		[DllImport ("__Internal")]private static extern void CLoadRewardVideoWithSlotId(string slot_id);
		[DllImport ("__Internal")]private static extern void CShowRewardVideo();
		[DllImport ("__Internal")]private static extern bool CCheckRewardVideoIsReady();
	#endif

		/**
 		Get CT AD Config in Appdelegate(didFinishLaunchingWithOptions:)

 		@param slot_id Ad
 		*/
		public static void loadRequestGetCTSDKConfigBySlot_id(string slot_id)
		{
			try
			{
				#if (UNITY_ANDROID)
					if(unityPlayerClass == null)
						unityPlayerClass = new AndroidJavaClass(UNITY_CLASS);
					
					//get activity
					currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
					
					//init sdk
					if(ctClass == null)
						ctClass = new AndroidJavaClass(SDK_CLASS);
					ctClass.CallStatic("init", currentActivity, slot_id); 
					
					//init video
					if(cmClass == null)
						cmClass = new AndroidJavaClass(CM_CLASS);
					
					if(cmClass != null)
						cmClass.CallStatic("createInstance");
					else
						Debug.Log("CTError: createInstance error ");

					ctVideo = cmClass.GetStatic<AndroidJavaObject>("sInstance");

					if(ctVideo != null)
						ctVideo.Call("setUnityDelegateObjName", delegateName); 
					else
						Debug.Log("CTError: setUnityDelegateObjName error ");

					//init interstitial
					if(adsClass == null)
						adsClass = new AndroidJavaObject(CM_ADS_CLASS);

					adsClass.Call("setUnityDelegateObjName", delegateName); 
				#elif UNITY_IOS
					CSetDelegateObjName(delegateName);
					CLoadRequestGetCTSDKConfigBySlot_id(slot_id);
				#endif
			}
			catch (Exception e)
			{
				Debug.Log(e.StackTrace);
			}
		}

		//create delegate obj if not found
		private static void createDelegateObj(){
			GameObject findObj = GameObject.Find(delegateName);
			if (findObj == null) {
				GameObject singleton = new GameObject(delegateName);
				singleton.AddComponent<CTService>();
			}
		}


		/**
 		Get RewardVideo Ad
 		First,you should Call (loadRewardVideoWithSlotId) method get RewardVideo Ad！
 		Then On success delegate method invokes (showRewardVideo） method
		@param slot_id         Cloud Tech AD ID
 		*/
		public static void loadRewardVideoWithSlotId(string slot_id)
		{
			try
			{
				createDelegateObj();
				#if (UNITY_ANDROID)
				if(ctVideo != null && unityPlayerClass!= null){
					currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
					ctVideo.Call("preloadRewardedVideo", currentActivity, slot_id);
				}else{
					Debug.Log("CTError: loadRewardVideoWithSlotId error ");
				}
				#elif UNITY_IOS
					CLoadRewardVideoWithSlotId(slot_id);
				#endif
			}
			catch (Exception e)
			{
				Debug.Log(e.StackTrace);
			}
		}
			
		/**
 		show RewardVideo	you should call it in the rewardVideoLoadSuccess delegate function.
		 */
		public static void showRewardVideo(string slot_id)
		{
			try
			{
				#if (UNITY_ANDROID)
				if(ctVideo != null){
					bool isReady = ctVideo.Call<bool>("isRewardedVideoAvailable");
					if(isReady)
						ctVideo.Call("showRewardedVideo");
					else
						Debug.Log("Rewarded video is not loaded");
				}else{
					Debug.Log("CTError: showRewardVideo error ");
				}
				#elif UNITY_IOS
					CShowRewardVideo();
				#endif
			}
			catch (Exception e)
			{
				Debug.Log(e.StackTrace);
			}
		}

		/**
 		Check if RewardVideo is read 

		if true, you can call showRewardVideo;
		 */

		public static bool checkRewardVideoIsReady()
		{
			try
			{
				#if (UNITY_ANDROID)
					if(ctVideo != null){
						return ctVideo.Call<bool>("isRewardedVideoAvailable");
					}
				#elif UNITY_IOS
					return CCheckRewardVideoIsReady();
				#endif
			}
			catch (Exception e)
			{
				Debug.Log(e.StackTrace);
			}
			return false;
		}

		/**
 		release memory on andorid platform
		 */
		public static void release(){
			#if UNITY_ANDROID
			ctClass = null;
			cmClass = null;
			unityPlayerClass = null;
			currentActivity = null;
			ctVideo = null;
			#endif
		}
			
		/**
		CTReward video ad delegate
		*/

		/**
		*  video is loaded successfully, you can call showRewardVideo() in this function.
		**/
		public static event Action rewardVideoLoadSuccess;
		/**
		*  video is loaded failed, you cannot showRewardVideo();
		**/
		public static event Action<string> rewardVideoLoadingFailed;
		/**
		*  begin playing Ad
		**/
		public static event Action rewardVideoDidStartPlaying;
		/**
		*   playing Ad finish
		**/
		public static event Action rewardVideoDidFinishPlaying;
		/**
		*   user click Ads
		**/
		public static event Action rewardVideoDidClickRewardAd;
		/**
		*  will leave Application
		**/
		public static event Action rewardVideoWillLeaveApplication;
		/**
		*  jump AppStroe failed
		**/
		public static event Action rewardVideoJumpfailed;
		/**
		*  reward the user
		**/
		public static event Action<string> rewardVideoAdRewarded;
		/**
		*  reward video ad is closed
		**/
		public static event Action rewardVideoClosed;

		/**
 		CTReward video ad delegate implement
 		*/

		public void rewardVideoLoadSuccessEvent() {
			if(rewardVideoLoadSuccess != null){
				rewardVideoLoadSuccess();
			}
		}
			
		public void rewardVideoLoadingFailedEvent(string message) {
			if(rewardVideoLoadingFailed != null){
				rewardVideoLoadingFailed(message);
			}
		}

		public void rewardVideoDidStartPlayingEvent() {
			if(rewardVideoDidStartPlaying != null){
				rewardVideoDidStartPlaying();
			}
		}
			
		public void rewardVideoDidFinishPlayingEvent() {
			if(rewardVideoDidFinishPlaying != null){
				rewardVideoDidFinishPlaying();
			}
		}
			
		public void rewardVideoDidClickRewardAdEvent() {
			if(rewardVideoDidClickRewardAd != null){
				rewardVideoDidClickRewardAd();
			}
		}

		public void rewardVideoWillLeaveApplicationEvent() {
			if(rewardVideoWillLeaveApplication != null){
				rewardVideoWillLeaveApplication();
			}
		}
			
		public void rewardVideoJumpfailedEvent() {
			if(rewardVideoJumpfailed != null){
				rewardVideoJumpfailed();
			}
		}
			
		public void rewardVideoAdRewardedNameEvent(string message) {
			if(rewardVideoAdRewarded != null){
				rewardVideoAdRewarded(message);
			}
		}
			
		public void rewardVideoClosedEvent() {
			if(rewardVideoClosed != null){
				rewardVideoClosed();
			}
		}

		/**
 		Get Interstitial Ad
 		First,you should Call (loadInterstitialWithSlotId) method get Interstitial！
 		Then On his success delegate method invokes (showInterstitia） method
		@param slot_id         Cloud Tech AD ID
 		*/
		public static void preloadInterstitialWithSlotId(string slot_id)
		{
			try
			{
				createDelegateObj();
				#if (UNITY_ANDROID)
				if(adsClass != null && unityPlayerClass != null){
					currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
					adsClass.Call("preloadInterstitial", currentActivity, slot_id, true, true);
				}
				#elif UNITY_IOS

				#endif
			}
			catch (Exception e)
			{
				Debug.Log(e.StackTrace);
			}
		}

		/**
 		show showInterstitial	you should call it in the loadInterstitialWithSlotId delegate function.
		 */
		public static void showInterstitial()
		{
			try
			{
				#if (UNITY_ANDROID)
				if(adsClass != null){
					adsClass.Call("showInterstitial");
				}
				#elif UNITY_IOS

				#endif
			}
			catch (Exception e)
			{
				Debug.Log(e.StackTrace);
			}
		}

		public static bool isInterstitialAvailable(){
			#if (UNITY_ANDROID)
			if(adsClass != null && unityPlayerClass != null){
				return adsClass.Call<bool>("isInterstitialAvailable");
			}
			return false;
			#elif UNITY_IOS
			return false;
			#endif
		}

		/**
		*  Interstitial is loaded successfully, you can call showInterstitial() in this function.
		**/
		public static event Action interstitialLoadSuccess;
		/**
		*  Interstitial is loaded failed;
		**/
		public static event Action<string> interstitialLoadFailed;
		/**
		*   user click Ads
		**/
		public static event Action interstitialDidClickRewardAd;
		/**
		*  jump AppStroe failed
		**/
		public static event Action interstitialJumpfailed;
		/**
		*  Interstitial is hidden
		**/
		public static event Action interstitialClose;

		public void interstitialLoadSuccessEvent() {
			Debug.Log ("U3D delegate, interstitialLoadSuccessEvent");
			if(interstitialLoadSuccess != null){
				interstitialLoadSuccess();
			}
		}

		public void interstitialLoadingFailedEvent(string message) {
			Debug.Log ("U3D delegate, interstitialLoadingFailedEvent");
			if(interstitialLoadFailed != null){
				interstitialLoadFailed(message);
			}
		}

		public void interstitialDidClickAdEvent() {
			if(interstitialDidClickRewardAd != null){
				interstitialDidClickRewardAd();
			}
		}

		public void interstitialJumpfailedEvent() {
			if(interstitialJumpfailed != null){
				interstitialJumpfailed();
			}
		}

		public void interstitialCloseEvent() {
			if(interstitialClose != null){
				interstitialClose();
			}
		}
	}
}
