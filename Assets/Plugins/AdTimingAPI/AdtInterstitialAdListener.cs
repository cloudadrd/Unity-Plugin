using System;

public interface AdtInterstitialAdListener {
    void OnInterstitialAdAvailabilityChanged(bool available);
    void OnInterstitialAdShowed(string scene);
    void OnInterstitialAdShowFailed(string scene, string error);
    void OnInterstitialAdClosed(string scene);
    void OnInterstitialAdClicked(string scene);
}
