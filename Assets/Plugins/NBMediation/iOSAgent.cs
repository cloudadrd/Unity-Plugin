//#if UNITY_IPHONE || UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;



public class iOSAgent : NBMediationAgent
{
    private const int OnADChangeAvailable = 0;
    private const int OnADOpen = 1;
    private const int OnADShow = 2;
    private const int OnADClick = 3;
    private const int OnADClose = 4;
    private const int OnADEnd = 5;
    private const int OnADShowFail = 6;
    private const int OnADRewarded = 7;

    public  delegate void iOSAdTimingCallback(int type, int code , string extraData);

    //******************* SDK Init *******************//

    [DllImport("__Internal")]
    private static extern void adtSetUserConsent(string consent);

    [DllImport("__Internal")]
    private static extern void adtInitWithAppKey(string appKey);

    [DllImport("__Internal")]
    private static extern bool adtInitialized();

    [DllImport("__Internal")]
    private static extern void adtSetLogEnable(bool logEnable);



    //******************* Interstitial API *******************//

    [DllImport("__Internal")]
    private static extern void adtSetInterstitialDelegate(iOSAdTimingCallback callback);

    [DllImport("__Internal")]
    private static extern bool adtInterstitialIsReady();

    [DllImport("__Internal")]
    private static extern void adtShowInterstitial();

    [DllImport("__Internal")]
    private static extern void adtShowInterstitialWithScene(string scene);


    //******************* RewardedVideo API *******************//


    [DllImport("__Internal")]
    private static extern void adtSetRewardedVideoDelegate(iOSAdTimingCallback callback);

    [DllImport("__Internal")]
    private static extern bool adtRewardedVideoIsReady();

    [DllImport("__Internal")]
    private static extern void adtShowRewardedVideo();

    [DllImport("__Internal")]
    private static extern void adtShowRewardedVideoWithScene(string scene);

    [DllImport("__Internal")]
    private static extern void adtShowRewardedVideoWithExtraParams(string scene, string extraParams);

    private static NBInterstitialAdListener _insterstitialListener;
    private static NBRewardedVideoListener _rewardedVideoListener;

	//******************* Banner API *******************//

	[DllImport("__Internal")]
	private static extern bool adtIsBannerReady(string slotid);

	[DllImport("__Internal")]
	private static extern void adtLoadBanner(string slotid);

	[DllImport("__Internal")]
	private static extern void adtShowBanner(string slotid);

	[DllImport("__Internal")]
	private static extern void adtHideBanner(string slotid, bool isDestory);

    #region AdTimingAgent implementation


    public void setUserConsent(string consent)
    {
        NBMediationUtils.printLogI("setUserConsent:" + consent);
        adtSetUserConsent(consent);
    }

    public void init(string appkey, NBInitListener adtimingInitListener = null)
    {
        NBMediationUtils.printLogI("init with key: " + appkey);
        adtInitWithAppKey(appkey);
        adtSetInterstitialDelegate(iosAdTimingInterstitialCallback);
        adtSetRewardedVideoDelegate(iosAdTimingRewardedVideoCallback);
        if (adtimingInitListener != null)
        {
            adtimingInitListener.onSuccess(); 
        }
    }

    public bool isInitialized()
    {
        return adtInitialized();
    }

    public void debug(bool isDebug)
    {
        NBMediationUtils.isDebug = isDebug;
        adtSetLogEnable(isDebug);
    }

    public void setInterstitialListener(NBInterstitialAdListener interstitialAdListener)
    {
        NBMediationUtils.printLogI("set interstitial listener" + interstitialAdListener);
        _insterstitialListener = interstitialAdListener;
    }

    public bool isInterstitialReady()
    {
        bool isReady = false;

        NBMediationUtils.printLogI("isInterstitialReady");
        isReady = adtInterstitialIsReady();

        return isReady;
    }

    public void showInterstitial()
    {
        NBMediationUtils.printLogI("show interstitial");
        if (adtInterstitialIsReady())
        {
            adtShowInterstitial();
        }
    }

    public void showInterstitial(string scene)
    {

        NBMediationUtils.printLogI("show interstitial");

        if (scene == null || scene.Length == 0)
        {
            if (adtInterstitialIsReady())
            {
                adtShowInterstitialWithScene("");
            }
        }
        else
        {
            if (adtInterstitialIsReady())
            {
                adtShowInterstitialWithScene(scene);
            }
        }
    }

    [MonoPInvokeCallback(typeof(iOSAdTimingCallback))]
    public static void iosAdTimingInterstitialCallback(int type, int code, string extraData)
    {
        NBMediationUtils.printLogI("interstitial callback type " + type);
        if (_insterstitialListener != null)
        {
            switch (type)
            {
                case OnADChangeAvailable:
                    {
                        NBMediationUtils.printLogI("interstitial change available "+code);
                        _insterstitialListener.OnInterstitialAdAvailabilityChanged((code>0));
                    }
                    break;

                case OnADOpen:
                    {
                        NBMediationUtils.printLogI("interstitial show " + extraData);
                        _insterstitialListener.OnInterstitialAdShowed(extraData);
                    }
                    break;

                case OnADShow:
                    {
                        NBMediationUtils.printLogI("interstitial show " + extraData);
                        _insterstitialListener.OnInterstitialAdShowed(extraData);
                    }
                    break;

                case OnADClick:
                    {
                        NBMediationUtils.printLogI("interstitial click " + extraData);
                        _insterstitialListener.OnInterstitialAdClicked(extraData);
                    }
                    break;

                case OnADClose:
                    {
                        NBMediationUtils.printLogI("interstitial close " + extraData);
                        _insterstitialListener.OnInterstitialAdClosed(extraData);
                    }
                    break;

                case OnADShowFail:
                    {
                        NBMediationUtils.printLogI(string.Format("interstitial show faild {0} error {1}", extraData, code));
                        string errorMsg = "error code:" + code;
                        _insterstitialListener.OnInterstitialAdShowFailed(extraData, errorMsg);
                    }
                    break;
           
            }
        }

    }





    public void setRewardedVideoListener(NBRewardedVideoListener rewardedVideoListener)
    {
        NBMediationUtils.printLogI("set rewardedVideo listener" + rewardedVideoListener);
        _rewardedVideoListener = rewardedVideoListener;
    }

    public bool isRewardedVideoReady()
    {
        bool isReady = false;
        NBMediationUtils.printLogI("isRewardedVideoReady");
        isReady = adtRewardedVideoIsReady();
        return isReady;
    }

    public void showRewardedVideo()
    {
        NBMediationUtils.printLogI("show rewardedVideo");
        if (adtRewardedVideoIsReady())
        {
            adtShowRewardedVideo();
        }
    }

    public void showRewardedVideo(string scene)
    {

        NBMediationUtils.printLogI("show rewardedVideo");

        if (scene == null || scene.Length == 0)
        {
            if (adtRewardedVideoIsReady())
            {
                adtShowRewardedVideoWithScene("");
            }
        }
        else
        {
            if (adtRewardedVideoIsReady())
            {
                adtShowRewardedVideoWithScene(scene);
            }
        }
    }

    public void showRewardedVideo(string scene, string extraParams)
    {

        NBMediationUtils.printLogI("show rewardedVideo");

        if (scene == null || scene.Length == 0)
        {
            if (extraParams == null || extraParams.Length == 0)
            {
                if (adtRewardedVideoIsReady())
                {
                    adtShowRewardedVideoWithExtraParams("", "");
                }
            }
            else
            {
                if (adtRewardedVideoIsReady())
                {
                    adtShowRewardedVideoWithExtraParams("", extraParams);
                }
            }
        }
        else
        {
            if (extraParams == null || extraParams.Length == 0)
            {
                if (adtRewardedVideoIsReady())
                {
                    adtShowRewardedVideoWithExtraParams(scene, "");
                }
            }
            else
            {
                if (adtRewardedVideoIsReady())
                {
                    adtShowRewardedVideoWithExtraParams(scene, extraParams);
                }
            }
        }
    }

    [MonoPInvokeCallback(typeof(iOSAdTimingCallback))]
    public static void iosAdTimingRewardedVideoCallback(int type, int code, string extraData)
    {
        if (_rewardedVideoListener != null)
        {
            switch (type)
            {
                case OnADChangeAvailable:
                    {
                        NBMediationUtils.printLogI("rewardedVideo change available "+code);
                        _rewardedVideoListener.OnRewardedVideoAvailabilityChanged((code>0));
                    }
                    break;

                case OnADOpen:
                    {
                        NBMediationUtils.printLogI("rewardedVideo open " + extraData);
                        _rewardedVideoListener.OnRewardedVideoAdStarted(extraData);
                    }
                    break;

                case OnADShow:
                    {
                        NBMediationUtils.printLogI("rewardedVideo show " + extraData);
                        _rewardedVideoListener.OnRewardedVideoAdShowed(extraData);
                    }
                    break;

                case OnADClick:
                    {
                        NBMediationUtils.printLogI("rewardedVideo click " + extraData);
                        _rewardedVideoListener.OnRewardedVideoAdClicked(extraData);
                    }
                    break;

                case OnADEnd:
                    {
                        NBMediationUtils.printLogI(string.Format("rewardedVideo end " + extraData));
                        _rewardedVideoListener.OnRewardedVideoAdEnded(extraData);
                    }
                    break;

                case OnADClose:
                    {
                        NBMediationUtils.printLogI(string.Format("rewardedVideo close " + extraData));
                        _rewardedVideoListener.OnRewardedVideoAdClosed(extraData);
                    }
                    break;

                case OnADRewarded:
                    {
                        NBMediationUtils.printLogI(string.Format("user get rewarded " + extraData));
                        _rewardedVideoListener.OnRewardedVideoAdRewarded(extraData);
                    }
                    break;

                case OnADShowFail:
                    {
                        NBMediationUtils.printLogI(string.Format("rewardedVideo show faild {0} error {1}", extraData, code));
                        string errorMsg = "error code:" + code;
                        _rewardedVideoListener.OnRewardedVideoAdShowFailed(extraData, errorMsg);
                    }
                    break;
            }
        }

    }

	public bool isBannerReady(string slotid)
	{
		bool isReady = false;
		NBMediationUtils.printLogI("isBannerReady");
		isReady = adtIsBannerReady(slotid);
		return isReady;
	}

	public void loadBanner(string slotid)
	{
		NBMediationUtils.printLogI("loadBanner");
		adtLoadBanner (slotid);
	}

	public void showBanner(string slotid)
	{
		NBMediationUtils.printLogI("showBanner");
		if (adtIsBannerReady(slotid))
		{
			adtShowBanner (slotid);
		}
	}

	public void hideBanner(string slotid, bool isDestory)
	{
		NBMediationUtils.printLogI("hideBanner");
		adtHideBanner (slotid, isDestory);
	}


    #endregion
}

//#endif
