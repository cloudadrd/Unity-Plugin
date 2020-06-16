using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface NBMediationAgent{
    //******************* SDK Init *******************//
    void init(string appkey, NBInitListener adtimingInitListener = null);
    bool isInitialized();
    void setUserConsent(string consent);
    void debug(bool isDebug);
    //******************* RewardedVideo API *******************//
    void setRewardedVideoListener(NBRewardedVideoListener rewardedVideoListener);
    void showRewardedVideo();
    void showRewardedVideo(string scene);
    bool isRewardedVideoReady();
    //自定义placementid加载
    void loadRewardedVideo(string placementId);
    void setRewardedVideoListener(string placementId, NBRewardedVideoListener rewardedVideoListener);
    void showRewardedVideo(string placementId, string scene);
    bool isRewardedVideoReady(string placementId);
    //******************* Interstitial API *******************//
    void setInterstitialListener(NBInterstitialAdListener interstitialAdListener);
    void showInterstitial();
    void showInterstitial(string scene);
    bool isInterstitialReady();
    //自定义placementid加载
    void loadInterstitial(string placementId);
    void setInterstitialListener(string placementId, NBInterstitialAdListener interstitialAdListener);
    void showInterstitial(string placementId, string scene);
    bool isInterstitialReady(string placementId);
    //******************* Banner API *******************//
    bool isBannerReady(string slotid);
	void loadBanner (string slotid);
	void showBanner (string slotid);
	void hideBanner(string slotid, bool isDestory);
}
