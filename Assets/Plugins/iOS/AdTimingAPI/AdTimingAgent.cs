using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AdTimingAgent{
    //******************* SDK Init *******************//
    void init(string appkey, AdtInitListener adtimingInitListener = null);
    bool isInitialized();
    void setUserConsent(string consent);
    void debug(bool isDebug);
    //******************* RewardedVideo API *******************//
    void setRewardedVideoListener(AdtRewardedVideoListener rewardedVideoListener);
    void showRewardedVideo();
    void showRewardedVideo(string scene);
    void showRewardedVideo(string scene, string extraParams);
    bool isRewardedVideoReady();
    //******************* Interstitial API *******************//
    void setInterstitialListener(AdtInterstitialAdListener interstitialAdListener);
    void showInterstitial();
    void showInterstitial(string scene);
    bool isInterstitialReady();
}
