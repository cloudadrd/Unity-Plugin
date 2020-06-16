﻿using System;
using UnityEngine;

public class NBMediation : NBMediationAgent
{
    private NBMediationAgent _platformAgent;
    private static NBMediation _instance;
    private const string UNITY_PLUGIN_VERSION = "4.1.0";


    private NBMediation()
    {
#if (UNITY_IPHONE || UNITY_IOS)
        _platformAgent = new iOSAgent();
#elif UNITY_ANDROID
        _platformAgent = new AndroidAgent();
#endif
    }

    #region AdTimingAgent implementation
    public static NBMediation Agent
    {
        get
        {
            if (_instance == null)
            {
				Debug.Log("UnityApp Agent alloc");
                _instance = new NBMediation();
            }
            return _instance;
        }
    }

    public void init(string appkey, NBInitListener adtimingInitListener = null)
    {
        _platformAgent.init(appkey, adtimingInitListener);
    }

    public bool isInitialized()
    {
        return _platformAgent.isInitialized();
    }

    public void setUserConsent(string consent)
    {
        _platformAgent.setUserConsent(consent);
    }

    public void setRewardedVideoListener(NBRewardedVideoListener rewardedVideoListener)
    {
        _platformAgent.setRewardedVideoListener(rewardedVideoListener);
    }

    public void showRewardedVideo()
    {
        _platformAgent.showRewardedVideo();
    }

    public void showRewardedVideo(string scene)
    {
        _platformAgent.showRewardedVideo(scene);
    }

    public bool isRewardedVideoReady()
    {
        return _platformAgent.isRewardedVideoReady();
    }

    public void setInterstitialListener(NBInterstitialAdListener interstitialAdListener)
    {
        _platformAgent.setInterstitialListener(interstitialAdListener);
    }

    public void showInterstitial()
    {
        _platformAgent.showInterstitial();
    }

    public void showInterstitial(string scene)
    {
        _platformAgent.showInterstitial(scene);
    }

    public bool isInterstitialReady()
    {
        return _platformAgent.isInterstitialReady();
    }

    public string getVersion()
    {
        return UNITY_PLUGIN_VERSION;
    }

    public void debug(bool isDebug)
    {
        _platformAgent.debug(isDebug);
    }

	public void loadBanner(string slotid){
		_platformAgent.loadBanner (slotid);
	}

	public bool isBannerReady(string slotid){
		return _platformAgent.isBannerReady (slotid);
	}

	public void showBanner(string slotid){
		_platformAgent.showBanner (slotid);
	}

	public void hideBanner(string slotid, bool isDestory){
		_platformAgent.hideBanner (slotid, isDestory);
	}

    //rewardedvide interstitial指定slot加载
    public void loadRewardedVideo(String placementId)
    {
        _platformAgent.loadRewardedVideo(placementId);
    }

    public void setRewardedVideoListener(string placementId, NBRewardedVideoListener rewardedVideoListener)
    {
        _platformAgent.setRewardedVideoListener(placementId, rewardedVideoListener);
    }

    public void showRewardedVideo(string placementId, string scene)
    {
        _platformAgent.showRewardedVideo(placementId, scene);
    }

    public bool isRewardedVideoReady(string placementId)
    {
        return _platformAgent.isRewardedVideoReady(placementId);
    }

    public void loadInterstitial(string placementId)
    {
        _platformAgent.loadInterstitial(placementId);
    }

    public void setInterstitialListener(string placementId, NBInterstitialAdListener interstitialAdListener)
    {
        _platformAgent.setInterstitialListener(placementId, interstitialAdListener);
    }

    public void showInterstitial(string placementId, string scene)
    {
        _platformAgent.showInterstitial(placementId, scene);
    }

    public bool isInterstitialReady(string placementId)
    {
        return _platformAgent.isInterstitialReady(placementId);
    }
    #endregion
}