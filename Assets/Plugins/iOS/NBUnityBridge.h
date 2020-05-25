//
//  ADTUnityBridge.h
//  Unity-iPhone
//
//  Created by ym on 2020/5/12.
//

#import <Foundation/Foundation.h>
#import <NBMediation/NBMediation.h>
#import <NBMediation/NBInterstitial.h>
#import <NBMediation/NBInterstitialDelegate.h>
#import <NBMediation/NBRewardedVideo.h>
#import <NBMediation/NBRewardedVideoDelegate.h>

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

@interface ADTUnityBridge : NSObject<NBInterstitialDelegate,NBRewardedVideoDelegate>
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

//banner
- (BOOL)isBannerReady:(NSString *)slotid;

- (void)loadBanner:(NSString *)slotid;

- (void)showBanner:(NSString *)slotid;

- (void)hideBanner:(NSString *)slotid isDestory:(BOOL) isDestory;
@end


NS_ASSUME_NONNULL_END
