using System;

public class AdTiming : AdTimingAgent
{
    private AdTimingAgent _platformAgent;
    private static AdTiming _instance;
    private const string UNITY_PLUGIN_VERSION = "4.1.0";


    private AdTiming()
    {
#if (UNITY_IPHONE || UNITY_IOS)
        _platformAgent = new iOSAgent();
#elif UNITY_ANDROID
        _platformAgent = new AndroidAgent();
#endif
    }

    #region AdTimingAgent implementation
    public static AdTiming Agent
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AdTiming();
            }
            return _instance;
        }
    }

    public void init(string appkey, AdtInitListener adtimingInitListener = null)
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

    public void setRewardedVideoListener(AdtRewardedVideoListener rewardedVideoListener)
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

    public void showRewardedVideo(string scene, string extraParams)
    {
        _platformAgent.showRewardedVideo(scene, extraParams);
    }

    public bool isRewardedVideoReady()
    {
        return _platformAgent.isRewardedVideoReady();
    }

    public void setInterstitialListener(AdtInterstitialAdListener interstitialAdListener)
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
    #endregion
}