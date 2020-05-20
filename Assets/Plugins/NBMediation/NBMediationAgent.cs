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
    void showRewardedVideo(string scene, string extraParams);
    bool isRewardedVideoReady();
    //******************* Interstitial API *******************//
    void setInterstitialListener(NBInterstitialAdListener interstitialAdListener);
    void showInterstitial();
    void showInterstitial(string scene);
    bool isInterstitialReady();
}
