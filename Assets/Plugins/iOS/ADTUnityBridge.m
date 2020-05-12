//
//  ADTUnityBridge.m
//  Unity-iPhone
//
//  Created by ylm on 2019/7/31.
//

#import "ADTUnityBridge.h"

typedef void (*completionHandler)(const char* error);

typedef void (*interstitialCallback)(int type, int code, const char* sceneName);

typedef void (*videoCallback)(int type, int code, const char* sceneName);


void adtLog(const char* log){
    NSLog(@"%s",log);
}

void adtSetLogEnable(bool logEnable)
{
    
    [OpenMediation setLogEnable:logEnable];
}


void adtInitWithAppKey(const char* appKey){
    NSString* nsAppKey = [NSString stringWithUTF8String:appKey];
    [OpenMediation initWithAppKey:nsAppKey];
}

bool adtInitialized(){
    return [OpenMediation isInitialized];
}

void adtSetUserConsent(const char* consent){
    NSString* c = [NSString stringWithUTF8String:consent];
    [OpenMediation setUserConsent:c];
}

/*
void adtSetIap(float count, const char* currency){
    NSString* curr = [NSString stringWithUTF8String:currency];
    [OpenMediation userPurchase:count currency:curr];
}


void adtSendAFConversionData(const char* conversionData){
    if (conversionData) {
        NSString *cd = [NSString stringWithUTF8String:conversionData];
        if ([cd length] > 0) {
            NSData *data = [cd dataUsingEncoding:NSUTF8StringEncoding];
            NSError *jsonErr = nil;
            NSDictionary *dict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:&jsonErr];
            if (!jsonErr && [dict isKindOfClass:[NSDictionary class]]) {
                [OpenMediation sendAFConversionData:dict];
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
                [OpenMediation sendAFDeepLinkData:dict];
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
    [[OMInterstitial sharedInstance] addDelegate:self];
    [[OMRewardedVideo sharedInstance] addDelegate:self];
    return self;
}

- (BOOL)interstitialIsReady{
    return [[OMInterstitial sharedInstance] isReady];
}

- (void)showInterstitial{
    [[OMInterstitial sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:@""];
}

- (void)showInterstitialWithScene:(NSString *)scene{
    [[OMInterstitial sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:scene];
}

/// Invoked when a interstitial video is available.
- (void)OMInterstitialChangedAvailability:(BOOL)available
{
    if (_interstitialBlock) {
        _interstitialBlock(ADTForUnityAdStateAvailable,(int)available,@"");
    }
}

/// Sent immediately when a interstitial video is opened.
- (void)OMInterstitialDidOpen:(OMScene*)scene
{
    if(_interstitialBlock){
        _interstitialBlock(ADTForUnityAdStateOpen,0,scene.sceneName);
    }
}
/////////////
/// Sent immediately when a interstitial video starts to play.
- (void)OMInterstitialDidShow:(OMScene*)scene
{
    if(_interstitialBlock){
        _interstitialBlock(ADTForUnityAdStateShow,0,scene.sceneName);
    }
}

/// Sent after a interstitial video has been clicked.
- (void)OMInterstitialDidClick:(OMScene*)scene
{
    if(_interstitialBlock){
        _interstitialBlock(ADTForUnityAdStateClick,0,scene.sceneName);
    }
}

/// Sent after a interstitial video has been closed.
- (void)OMInterstitialDidClose:(OMScene*)scene
{
    if(_interstitialBlock){
        _interstitialBlock(ADTForUnityAdStateClose,0,scene.sceneName);
    }
}

/// Sent after a interstitial video has failed to play.
- (void)OMInterstitialDidFailToShow:(OMScene*)scene withError:(NSError *)error
{
    if(_interstitialBlock){
        _interstitialBlock(ADTForUnityAdStateShowFail,error.code,scene.sceneName);
    }
}


#pragma mark -- video
- (BOOL)videoIsReady{
    return [[OMRewardedVideo sharedInstance] isReady];
}

- (void)showVideo{
    [[OMRewardedVideo sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:@""];
}

- (void)showVideoWithScene:(NSString *)scene{
    [[OMRewardedVideo sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:scene];
}

- (void)showVideoWithExtraParams:(NSString *)scene extraParams:(NSString *)extraParams{
    [[OMRewardedVideo sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:scene extraParams:extraParams];
}

#pragma mark -- AdTimingRewardedVideoDelegate

- (void)omRewardedVideoChangedAvailability:(BOOL)available{
    if (available && _videoBlock) {
        _videoBlock(ADTForUnityAdStateAvailable,(int)available,@"");
    }
}

- (void)omRewardedVideoDidOpen:(OMScene*)scene{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateOpen,0,scene.sceneName);
    }
}

- (void)omRewardedVideoPlayStart:(OMScene*)scene{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateShow,0,scene.sceneName);
    }
}

- (void)omRewardedVideoDidClick:(OMScene*)scene{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateClick,0,scene.sceneName);
    }
}

- (void)omRewardedVideoDidClose:(OMScene *)scene{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateClose,0,scene.sceneName);
    }
}

- (void)omRewardedVideoPlayEnd:(OMScene*)scene{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateEnd,0,scene.sceneName);
    }
}

- (void)omRewardedVideoDidReceiveReward:(OMScene*)scene{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateRewarded,0,scene.sceneName);
    }
}

- (void)omRewardedVideoDidFailToShow:(OMScene*)scene withError:(NSError *)error{
    if(_videoBlock){
        _videoBlock(ADTForUnityAdStateShowFail,error.code,scene.sceneName);
    }
}

@end