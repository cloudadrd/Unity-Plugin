//
//  ADTUnityBridge.m
//  Unity-iPhone
//
//  Created by ylm on 2019/7/31.
//

#import "NBUnityBridge.h"

typedef void (*completionHandler)(const char* error);

typedef void (*interstitialCallback)(int type, int code, const char* sceneName);

typedef void (*videoCallback)(int type, int code, const char* sceneName);


void adtLog(const char* log){
    NSLog(@"%s",log);
}

void adtSetLogEnable(bool logEnable)
{
    
    [NBMediation setLogEnable:logEnable];
}


void adtInitWithAppKey(const char* appKey){
    NSString* nsAppKey = [NSString stringWithUTF8String:appKey];
    [NBMediation initWithAppKey:nsAppKey];
}

bool adtInitialized(){
    return [NBMediation isInitialized];
}

void adtSetUserConsent(const char* consent){
    NSString* c = [NSString stringWithUTF8String:consent];
    [NBMediation setUserConsent:c];
}


void adtSetIap(float count, const char* currency){
    NSString* curr = [NSString stringWithUTF8String:currency];
    [NBMediation userPurchase:count currency:curr];
}

/*
void adtSendAFConversionData(const char* conversionData){
    if (conversionData) {
        NSString *cd = [NSString stringWithUTF8String:conversionData];
        if ([cd length] > 0) {
            NSData *data = [cd dataUsingEncoding:NSUTF8StringEncoding];
            NSError *jsonErr = nil;
            NSDictionary *dict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:&jsonErr];
            if (!jsonErr && [dict isKindOfClass:[NSDictionary class]]) {
                [NBMediation sendAFConversionData:dict];
            }
        }
    }

}

void adtSendAFDeepLinkData(const char* attributionData){
    if (attributionData) {
        NSString *cd = [NSString stringWithUTF8String:attributionData];
        if ([cd length] > 0) {
            NSData *data = [cd dataUsingEncoding:NSUTF8StringEncoding];
            NSError *jsonErr = nil;
            NSDictionary *dict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:&jsonErr];
            if (!jsonErr && [dict isKindOfClass:[NSDictionary class]]) {
                [NBMediation sendAFDeepLinkData:dict];
            }
        }
    }
}
*/

//interstitial

void adtSetInterstitialDelegate(interstitialCallback callback){
    [ADTUnityBridge sharedInstance].interstitialBlock = ^(ADTForUnityAdState state, NSInteger code, NSString *extraData) {
        callback((int)state,(int)code,[extraData UTF8String]);
    };
}

void adtShowInterstitial(){
    if ([[ADTUnityBridge sharedInstance] interstitialIsReady]) {
        [[ADTUnityBridge sharedInstance] showInterstitial];
    }
}

void adtShowInterstitialWithScene(const char* scene){
    NSString* adtScene = [NSString stringWithUTF8String:scene];
    if ([[ADTUnityBridge sharedInstance] interstitialIsReady]) {
        [[ADTUnityBridge sharedInstance] showInterstitialWithScene:adtScene];
    }
}

bool adtInterstitialIsReady(){
    return [[ADTUnityBridge sharedInstance] interstitialIsReady];
}


//video
void adtSetRewardedVideoDelegate(videoCallback callback){
    [ADTUnityBridge sharedInstance].videoBlock = ^(ADTForUnityAdState state, NSInteger code, NSString *extraData) {
        callback((int)state,(int)code,[extraData UTF8String]);
    };
}

void adtShowRewardedVideo(){
    if ([[ADTUnityBridge sharedInstance] videoIsReady]) {
        [[ADTUnityBridge sharedInstance] showVideo];
    }
}

void adtShowRewardedVideoWithScene(const char* scene){
    NSString* adtScene = [NSString stringWithUTF8String:scene];
    if ([[ADTUnityBridge sharedInstance] videoIsReady]) {
        [[ADTUnityBridge sharedInstance] showVideoWithScene:adtScene];
    }
}

void adtShowRewardedVideoWithExtraParams(const char* scene, const char* extraParams){
    NSString* adtScene = [NSString stringWithUTF8String:scene];
    NSString* adtParams = [NSString stringWithUTF8String:extraParams];
    if ([[ADTUnityBridge sharedInstance] videoIsReady]) {
        [[ADTUnityBridge sharedInstance] showVideoWithExtraParams:adtScene extraParams:adtParams];
    }
}

bool adtRewardedVideoIsReady(){
    return [[ADTUnityBridge sharedInstance] videoIsReady];
}

//banner
void adtLoadBanner(const char* slotid){
    NSString* adtSlotId = [NSString stringWithUTF8String:slotid];
    [[ADTUnityBridge sharedInstance] loadBanner:adtSlotId];
}

bool adtIsBannerReady(const char* slotid){
    NSString* adtSlotId = [NSString stringWithUTF8String:slotid];
    return [[ADTUnityBridge sharedInstance] isBannerReady:adtSlotId];
}

void adtShowBanner(const char* slotid){
    NSString* adtSlotId = [NSString stringWithUTF8String:slotid];
    [[ADTUnityBridge sharedInstance] showBanner:adtSlotId];
}

void adtHideBanner(const char* slotid, BOOL isDestory){
    NSString* adtSlotId = [NSString stringWithUTF8String:slotid];
    [[ADTUnityBridge sharedInstance] hideBanner:adtSlotId isDestory:isDestory];
}


static ADTUnityBridge * _instance = nil;

@implementation ADTUnityBridge

+ (instancetype)sharedInstance{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _instance = [[self alloc] init];
    });
    return _instance;
}

- (instancetype)init{
    if (self = [super init]) {
        _interstitialMap = [NSMutableDictionary dictionary];
    }
    [[NBInterstitial sharedInstance] addDelegate:self];
    [[NBRewardedVideo sharedInstance] addDelegate:self];
    return self;
}

- (BOOL)interstitialIsReady{
    return [[NBInterstitial sharedInstance] isReady];
}

- (void)showInterstitial{
    [[NBInterstitial sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:@""];
}

- (void)showInterstitialWithScene:(NSString *)scene{
    [[NBInterstitial sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:scene];
}

/// Invoked when a interstitial video is available.
- (void)NBInterstitialChangedAvailability:(BOOL)available
{
    if (_interstitialBlock) {
        _interstitialBlock(ADTForUnityAdStateAvailable,(int)available,@"");
    }
}

/// Sent immediately when a interstitial video is opened.
- (void)NBInterstitialDidOpen:(NBScene*)scene
{
    if(_interstitialBlock){
        _interstitialBlock(ADTForUnityAdStateOpen,0,scene.sceneName);
    }
}
/////////////
/// Sent immediately when a interstitial video starts to play.
- (void)NBInterstitialDidShow:(NBScene*)scene
{
    if(_interstitialBlock){
        _interstitialBlock(ADTForUnityAdStateShow,0,scene.sceneName);
    }
}

/// Sent after a interstitial video has been clicked.
- (void)NBInterstitialDidClick:(NBScene*)scene
{
    if(_interstitialBlock){
        _interstitialBlock(ADTForUnityAdStateClick,0,scene.sceneName);
    }
}

/// Sent after a interstitial video has been closed.
- (void)NBInterstitialDidClose:(NBScene*)scene
{
    if(_interstitialBlock){
        _interstitialBlock(ADTForUnityAdStateClose,0,scene.sceneName);
    }
}

/// Sent after a interstitial video has failed to play.
- (void)NBInterstitialDidFailToShow:(NBScene*)scene withError:(NSError *)error
{
    if(_interstitialBlock){
        _interstitialBlock(ADTForUnityAdStateShowFail,error.code,scene.sceneName);
    }
}


#pragma mark -- video
- (BOOL)videoIsReady{
    return [[NBRewardedVideo sharedInstance] isReady];
}

- (void)showVideo{
    [[NBRewardedVideo sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:@""];
}

- (void)showVideoWithScene:(NSString *)scene{
    [[NBRewardedVideo sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:scene];
}

- (void)showVideoWithExtraParams:(NSString *)scene extraParams:(NSString *)extraParams{
    [[NBRewardedVideo sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:scene extraParams:extraParams];
}

#pragma mark -- AdTimingRewardedVideoDelegate

- (void)NBRewardedVideoChangedAvailability:(BOOL)available{
    if (available && _videoBlock) {
        _videoBlock(ADTForUnityAdStateAvailable,(int)available,@"");
    }
}

- (void)NBRewardedVideoDidOpen:(NBScene*)scene{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateOpen,0,scene.sceneName);
    }
}

- (void)NBRewardedVideoPlayStart:(NBScene*)scene{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateShow,0,scene.sceneName);
    }
}

- (void)NBRewardedVideoDidClick:(NBScene*)scene{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateClick,0,scene.sceneName);
    }
}

- (void)NBRewardedVideoDidClose:(NBScene *)scene{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateClose,0,scene.sceneName);
    }
}

- (void)NBRewardedVideoPlayEnd:(NBScene*)scene{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateEnd,0,scene.sceneName);
    }
}

- (void)NBRewardedVideoDidReceiveReward:(NBScene*)scene{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateRewarded,0,scene.sceneName);
    }
}

- (void)NBRewardedVideoDidFailToShow:(NBScene*)scene withError:(NSError *)error{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateShowFail,error.code,scene.sceneName);
    }
}

#pragma mark -- banner

- (BOOL)isBannerReady:(NSString *)slotid{
    return [[NBBannerU3DHelper shareHelper] bannerIsReaday:slotid];
}

- (void)loadBanner:(NSString *)slotid{
    [[NBBannerU3DHelper shareHelper] loadBannerWithPlacementID:slotid];
}

- (void)showBanner:(NSString *)slotid{
    [[NBBannerU3DHelper shareHelper] bannerShow:slotid];
}

- (void)hideBanner:(NSString *)slotid isDestory:(BOOL) isDestory{
    [[NBBannerU3DHelper shareHelper] bannerHidden:slotid destroyIt:isDestory];
}

@end
