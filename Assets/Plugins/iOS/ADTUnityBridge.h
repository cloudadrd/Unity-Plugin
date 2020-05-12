//
//  ADTUnityBridge.h
//  Unity-iPhone
//
//  Created by ylm on 2019/7/31.
//

#import <Foundation/Foundation.h>
#import <OpenMediation/OpenMediation.h>
#import <OpenMediation/OMInterstitial.h>
#import <OpenMediation/OMInterstitialDelegate.h>
#import <OpenMediation/OMRewardedVideo.h>
#import <OpenMediation/OMRewardedVideoDelegate.h>

NS_ASSUME_NONNULL_BEGIN

typedef NS_ENUM(NSInteger, ADTForUnityAdState) {
    ADTForUnityAdStateAvailable = 0,
    ADTForUnityAdStateOpen = 1,
    ADTForUnityAdStateShow = 2,
    ADTForUnityAdStateClick = 3,
    ADTForUnityAdStateClose = 4,
    ADTForUnityAdStateEnd = 5,
    ADTForUnityAdStateShowFail = 6,
    ADTForUnityAdStateRewarded = 7,
};

typedef void (^interstitialCallbackBlock)(ADTForUnityAdState state, NSInteger code, NSString *extraData);
typedef void (^videoCallbackBlock)(ADTForUnityAdState state, NSInteger code, NSString *extraData);

@interface ADTUnityBridge : NSObject<OMInterstitialDelegate,OMRewardedVideoDelegate>
@property (nonatomic, strong) NSMutableDictionary *interstitialMap;
@property (nonatomic, strong) interstitialCallbackBlock interstitialBlock;
@property (nonatomic, strong) videoCallbackBlock videoBlock;
+ (instancetype)sharedInstance;
//interstitial
- (BOOL)interstitialIsReady;

- (void)showInterstitial;

- (void)showInterstitialWithScene:(NSString *)scene;

//video
- (BOOL)videoIsReady;

- (void)showVideo;

- (void)showVideoWithScene:(NSString *)scene;

- (void)showVideoWithExtraParams:(NSString *)scene extraParams:(NSString *)extraParams;

@end


NS_ASSUME_NONNULL_END
