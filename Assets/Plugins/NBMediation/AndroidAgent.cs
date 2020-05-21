#if UNITY_ANDROID

using System;
using UnityEngine;

public class AndroidAgent : NBMediationAgent
{

    AndroidJavaClass mAdTiming = null;

    public AndroidAgent() {
        try
        {
			mAdTiming = new AndroidJavaClass("com.nbmediation.sdk.api.unity.NmSdk");
        }
        catch (Exception e) {
			NBMediationUtils.printLogE("com.nbmediation.sdk.api.unity.NmSdk not found" + e.Message);
			Debug.Log("com.nbmediation.sdk.api.unity.NmSdk not found");
        }
    }

    public void debug(bool isDebug)
    {
        NBMediationUtils.isDebug = isDebug;
        if(mAdTiming != null)
        {
            mAdTiming.CallStatic("Debug", isDebug);
        }
    }

    public void init(string appkey, NBInitListener adtimingInitListener = null)
    {
        if (mAdTiming != null)
        {
            if (adtimingInitListener != null)
            {
                mAdTiming.CallStatic("init", NBMediationUtils.currentActivity(), appkey, new AdtimingInitCallBack(adtimingInitListener));
            }
            else
            {
                mAdTiming.CallStatic("init", NBMediationUtils.currentActivity(), appkey);
            }
        }
    }

    public bool isInitialized()
    {
        bool isInit = false;
        if (mAdTiming != null)
        {
            isInit = mAdTiming.CallStatic<bool>("isInit");
        }
        return isInit;
    }

    public void setIap(float count, string currency){
        if(mAdTiming != null){
            mAdTiming.CallStatic("setIAP", count, currency);
        }
    }

    public void sendAFDeepLinkData(String conversionData) {
        if (mAdTiming != null) {
            mAdTiming.CallStatic("sendAFDeepLinkData",conversionData);
        }
    }

    public void sendAFConversionData(String conversionData){
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("sendAFConversionData", conversionData);
        }
    }

    public bool isInterstitialReady()
    {
        bool isReady = false;
        if (mAdTiming != null)
        {
            isReady = mAdTiming.CallStatic<bool>("isInterstitialReady");
        }
        return isReady;
    }

    public bool isRewardedVideoReady()
    {
        bool isReady = false;
        if (mAdTiming != null)
        {
            isReady = mAdTiming.CallStatic<bool>("isRewardedVideoReady");
        }
        return isReady;
    }

    public void setInterstitialListener(NBInterstitialAdListener interstitialAdListener)
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("setInterstitialListener", new AdtimingInterstitialCallBack(interstitialAdListener));
        }
    }

    public void setRewardedVideoListener(NBRewardedVideoListener rewardedVideoListener)
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("setRewardedVideoListener", new AdtimingRewardedVideoCallBack(rewardedVideoListener));
        }
    }

    public void setUserConsent(string userId)
    {
    }

    public void showInterstitial()
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("showInterstitial", "");
        }
    }

    public void showInterstitial(string scene) {
        if (mAdTiming != null) {
            mAdTiming.CallStatic("showInterstitial", scene);
        }
    }

    public void showRewardedVideo()
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("showRewardedVideo", "");
        }
    }

    public void showRewardedVideo(string scene) {
        if (mAdTiming != null) {
            mAdTiming.CallStatic("showRewardedVideo", scene);
        }
    }

    public void showRewardedVideo(string scene, string extraParams)
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("setExtId", scene, extraParams);
            mAdTiming.CallStatic("showRewardedVideo", scene);
        }
    }

    private class AdtimingInitCallBack : AndroidJavaProxy
    {
        private readonly NBInitListener adtimingInitListener;

		public AdtimingInitCallBack(NBInitListener listener) : base("com.nbmediation.sdk.InitCallback")
        {
            this.adtimingInitListener = listener;
        }

        public void onSuccess()
        {
            if (adtimingInitListener != null)
            {
                NBMediationUtils.printLogI("Init onSuccess");
                adtimingInitListener.onSuccess();
            }
        }

        public void onError(string message)
        {
            if (adtimingInitListener != null)
            {
                NBMediationUtils.printLogE("Init onError:" + message);
                adtimingInitListener.onError(message);
            }
        }
    }

    private class AdtimingInterstitialCallBack : AndroidJavaProxy
    {
        private readonly NBInterstitialAdListener adtimingInterstitialListener;

		public AdtimingInterstitialCallBack(NBInterstitialAdListener listener) : base("com.nbmediation.sdk.api.unity.InterstitialListener")
        {
            this.adtimingInterstitialListener = listener;
        }
        public void onInterstitialAdAvailabilityChanged(bool available) {
            this.adtimingInterstitialListener.OnInterstitialAdAvailabilityChanged(available);
        }

        public void onInterstitialAdShowed(string scene) {
            this.adtimingInterstitialListener.OnInterstitialAdShowed(scene);
        }

        public void onInterstitialAdShowFailed(string scene, string error) {
            this.adtimingInterstitialListener.OnInterstitialAdShowFailed(scene, error);
        }

        public void onInterstitialAdClosed(string scene) {
            this.adtimingInterstitialListener.OnInterstitialAdClosed(scene);
        }

        public void onInterstitialAdClicked(string scene) {
            this.adtimingInterstitialListener.OnInterstitialAdClicked(scene);
        }



    }

    private class AdtimingRewardedVideoCallBack : AndroidJavaProxy
    {
        private readonly NBRewardedVideoListener adtimingRewardedVideoListener;

		public AdtimingRewardedVideoCallBack(NBRewardedVideoListener listener) : base("com.nbmediation.sdk.api.unity.VideoListener")
        {
            this.adtimingRewardedVideoListener = listener;
        }
        public void onRewardedVideoAvailabilityChanged(bool avaiable) {
            this.adtimingRewardedVideoListener.OnRewardedVideoAvailabilityChanged(avaiable);
        }
        public void onRewardedVideoAdShowed(string scene) {
            this.adtimingRewardedVideoListener.OnRewardedVideoAdShowed(scene);
        }

        public void onRewardedVideoAdShowFailed(string scene, string error) {
            this.adtimingRewardedVideoListener.OnRewardedVideoAdShowFailed(scene, error);
        }

        public void onRewardedVideoAdClicked(string scene) {
            this.adtimingRewardedVideoListener.OnRewardedVideoAdClicked(scene);
        }

        public void onRewardedVideoAdClosed(string scene) {
            this.adtimingRewardedVideoListener.OnRewardedVideoAdClosed(scene);
        }

        public void onRewardedVideoAdStarted(string scene) {
            this.adtimingRewardedVideoListener.OnRewardedVideoAdStarted(scene);
        }

        public void onRewardedVideoAdEnded(string scene) {
            this.adtimingRewardedVideoListener.OnRewardedVideoAdEnded(scene);
        }

        public void onRewardedVideoAdRewarded(string scene) {
            this.adtimingRewardedVideoListener.OnRewardedVideoAdRewarded(scene);
        }
    }
}

#endif