using System;
public interface NBRewardedVideoListener{
    void OnRewardedVideoAvailabilityChanged(bool available);
    void OnRewardedVideoAdShowed(string scene);
    void OnRewardedVideoAdShowFailed(string scene, string adTimingError);
    void OnRewardedVideoAdClicked(string scene);
    void OnRewardedVideoAdClosed(string scene);
    void OnRewardedVideoAdStarted(string scene);
    void OnRewardedVideoAdEnded(string scene);
    void OnRewardedVideoAdRewarded(string scene);
}