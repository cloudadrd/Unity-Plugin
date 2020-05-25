//
//  ALSManager.h
//  Applins
//  You should call [[Applins shareManager] loadRequestGetALSSDKConfigBySlot_id:@"slotID"] in didFinishLaunchingWithOptions Method

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "ALSElementModel.h"
#import "ALSNativeVideoModel.h"
#import "ALSADMRAIDView.h"

typedef enum : NSUInteger {
    ALSImageWHRateOneToOne= 0,           //Width:Hight  = 1:1
    ALSImageWHRateOnePointNineToOne      //Width:Hight  = 1.9:1
} ALSImageWHRate;

typedef enum : NSUInteger {
    ALSBannerSizeW320H50= 0,      //Width = 320  Hight = 50
    ALSBannerSizeW320H100,        //Width = 320  Hight = 100
    ALSBannerSizeW300H250         //Width = 300  Hight = 250
} ALSBannerSize;


@interface Applins : NSObject

#pragma mark - ApplinsSDK config Method
/**
 You should pass the singleton method to create the object, then calls the requests of the different types of ads.

 @return returns a global instance of Applins
 */
+ (instancetype)shareSDK;

/**
 Set using https request
 */
- (void)setSchemaHttps;

/**
 Get Applins AD Config in Appdelegate(didFinishLaunchingWithOptions:)

 @param slot_id Ad
 */
- (void)initSDK:(NSString *)slot_id;

/**
 For GDPR

 @param consentValue  yes/no/other
 @param consentType   content type is the agreement name you signed with users
 @param complete      state
 */
- (void)uploadConsentValue:(NSString *)consentValue
               consentType:(NSString *)consentType
                  complete:(void(^)(BOOL state))complete;
/*
Set whether this user is a child
@param child  if the user is a child, set YES, else set NO
*/
- (void)setIsChildDirected:(BOOL)child;


/*
Get whether this user is a child
*/

- (BOOL)isChildDirected;
#pragma mark - Native Ad Interface（Return Ad Elements）
/**
 We recommend use ALSNative Interface！！！
 Using inheritance ALSNativeAd advertising View customize layout, in prior to add to the parent View will return to the frame and successful nativeModel assigned to a custom View.
 
 @param slot_id         Native AD ID
 @param delegate        Set Delegate of Ad event(<ALSNativeAdDelegate>)
 @param WHRate          Set Image Rate
 @param isTest          Use test advertisement or not
 @param success         The request is successful Block, return Native Element Ad
 @param failure         The request failed Block, retuen error
 */
- (void)getNativeAD:(NSString *)slot_id
                      delegate:(id)delegate
           imageRate:(ALSImageWHRate)WHRate
                        isTest:(BOOL)isTest
                       success:(void (^)(ALSNativeAdModel *nativeModel))success
                       failure:(void (^)(NSError *error))failure;


/**
 Preload native ADs with image
 Using inheritance ALSNativeAd advertising View customize layout, in prior to add to the parent View will return to the frame and successful nativeModel assigned to a custom View.
 
 @param slot_id         Native AD ID
 @param delegate        Set Delegate of Ad event(<ALSNativeAdDelegate>)
 @param WHRate          Set Image Rate
 @param preloadImage    preload AD images if afferent YES
 @param isTest          Use test advertisement or not
 @param success         The request is successful Block, return Native Element Ad
 @param failure         The request failed Block, retuen error
 */
- (void)preloadNativeAD:(NSString *)slot_id
               delegate:(id)delegate
              imageRate:(ALSImageWHRate)WHRate
           preloadImage:(BOOL)preloadImage
                 isTest:(BOOL)isTest
                success:(void (^)(ALSNativeAdModel *nativeModel))success
                failure:(void (^)(NSError *error))failure;
/**
 Get Keywords Element Native ADs
 Using inheritance ALSNativeAd advertising View customize layout, in prior to add to the parent View will return to the frame and successful nativeModel assigned to a custom View.
 
 @param slot_id         Native AD ID
 @param delegate        Set Delegate of Ad event(<ALSNativeAdDelegate>)
 @param WHRate          Set Image Rate
 @param cat             ad type
 @param keyWords        Set Ad Keywords
 @param isTest          Use test advertisement or not
 @param success         The request is successful Block, return Native Element Ad
 @param failure         The request failed Block, retuen error
 */
- (void)getNativeADwithCatogaryOrKeywords:(NSString *)slot_id
                      delegate:(id)delegate
           imageRate:(ALSImageWHRate)WHRate
                         adcat:(NSInteger)cat
                      keyWords:(NSArray *)keyWords
                        isTest:(BOOL)isTest
                       success:(void (^)(ALSNativeAdModel *nativeModel))success
                       failure:(void (^)(NSError *error))failure;


/**
 Get Multiterm Element Native ADs
 Using inheritance ALSNativeAd advertising View customize layout, in prior to add to the parent View will return to the frame and successful nativeModel assigned to a custom View.
 
 @param slot_id         Native AD ID
 @param num             Ad numbers
 @param delegate        Set Delegate of Ad event(<ALSNativeAdDelegate>)
 @param WHRate          Set Image Rate
 @param isTest          Use test advertisement or not
 @param success         The request is successful Block, return Native Element Ad
 @param failure         The request failed Block, retuen error
 */
-(void)getMultiNativeADs:(NSString *)slot_id
               adNumbers:(NSInteger)num
                delegate:(id)delegate
                imageRate:(ALSImageWHRate)WHRate
                   isTest:(BOOL)isTest
                  success:(void (^)(NSArray *nativeArr))success
                  failure:(void (^)(NSError *error))failure;


#pragma mark - Banner AD Interface
/**
 Get Banner Ad View (Deprecated)
 
 @param slotid          Banner AD ID
 @param delegate        Set Delegate of Ad event(<ALSAdViewDelegate>)
 @param size          requre Ad Size
 @param containerView   the view which shows ads on
 @param isTest          Use test advertisement or not
 */
- (void)getBannerAD:(NSString*)slotid delegate:(id)delegate adSize:(ALSBannerSize)size container:(UIView*)containerView isTest:(BOOL)isTest NS_DEPRECATED_IOS(7.0, 12.0);


/**
 Get Banner Ad View
 
 @param slotid          Banner AD ID
 @param delegate        Set Delegate of Ad event(<ALSAdViewDelegate>)
 @param size          requre Ad Size
 @param isTest          Use test advertisement or not
 */

- (void)getBannerAD:(NSString*)slotid delegate:(id)delegate adSize:(ALSBannerSize)size isTest:(BOOL)isTest;
//
//
//#pragma mark - AppWall Ad Interface
///**
// Get AppWall ViewController
// First,you should Call preloadAppWall method,then call showAppWallViewController method show Appwall.
// 
// @param slot_id         Native AD ID
// @param customColor     If you want set custom UI,you should create ALSCustomColor object
// @param delegate        Set Delegate of Ads event (<ALSAppWallDelegate>)
// @param isTest          Use test advertisement or not
// @param success         The request is successful Block
// @param failure         The request failed Block, retuen error
// */
//- (void)preloadAppWall:(NSString *)slot_id
//           customColor:(ALSCustomColor *)customColor
//              delegate:(id)delegate
//                isTest:(BOOL)isTest
//               success:(void(^)())success
//                failure:(void(^)(NSError *error))failure;
//
///**
// Get App Wall ViewController
//
// @return AppWallViewController
// */
//- (UIViewController *)showAppWallViewController;


#pragma mark - RewardVideo Ad Interface
//Get Reward Video Ads

/**
 Set Rewarded Video Custom Parameters

  @param customParams         custom Params
 
 */
- (void)setCustomParameters:(NSString *)customParams;


/**
 Get RewardVideo Ad
 First,you must should Call (preloadRewardedVideoAD:delegate:) method get RewardVideo Ad！Then On his return to the success of the proxy method invokes the （showRewardedVideo） method

 @param slot_id         Rewarded Video slot ID
 @param delegate        Set Delegate of Ads event (<ALSRewardVideoDelegate>)
 */
- (void)preloadRewardedVideoAD:(NSString *)slot_id delegate:(id)delegate;

/**
 show RewardVideo      // We recommendation use [ showRewardedVideoWithCustomViewController: ] interface !
 */
- (void)showRewardedVideo;

/**
 show RewardVideo

 @param viewController The view controller on which the interstitial will display
 */
- (void)showRewardedVideoWithCustomViewController:(UIViewController *)viewController;

/**
 ALS Reward video is ready to play

 @return YES:you can call show rewardvideo interface / NO:don't call show rewardvideo interface 
 */
- (BOOL)isRewardedVideoReady;

#pragma mark - U3D Delegate

/**
 Set U3D Delegate
 */

- (void)setDelegateObjName:(NSString*) delegateName;

#pragma mark - ApplinsSDK Version
/**
 Get SDK Version

 @return SDK Version (NSString class)
 */
- (NSString*)getSDKVersion;


#pragma mark - Native Video Ad Interface
/**
 Get Native video Ad
 Call this interface to get Native Video AD.
 
 @param slot_id         Native Video slot ID
 @param delegate        Set Delegate of Ads event (<ALSNativeVideoDelegate>)
 @param WHRate          Set Image Rate
 @param isTest          Use test advertisement or not
 */
- (void)getNativeVideoAD:(NSString*)slot_id
                delegate:(id)delegate
               imageRate:(ALSImageWHRate)WHRate
                  isTest:(BOOL)isTest;

- (BOOL)isWifi;

#pragma mark -  New Interstitial Ad Interface
/**
 Preload Interstitial Ad
 Call this interface preload Interstitial AD.
 
 @param slotid          interstital slot ID
 @param delegate        Set Delegate of Ads event (<ALSAdViewDelegate>)
 @param isTest          Use test advertisement or not
 */
- (void)preloadInterstitialAd:(NSString *)slotid delegate:(id)delegate isTest:(BOOL)isTest;

/**
 Show interstitial ad
 Call this method after preload Interstitial ad success
 */
- (void)showInterstitialAD;

/**
 Check interstitial ad to be Ready
 Call this method before show ad
 */
- (BOOL)isInterstitialReady;

#pragma mark - splash ad
/**
Preload Splash Ad
Call this interface preload Splash AD.
 
@param slotid          Splash slot ID
@param delegate      Set Delegate of Ads event
@param window          Set UIWindow
@param launchImage          Set LaunchImage
@param customAdView        Set bottom custom view if needed
@param waitAdTime        Set loading time, ad will not show if not ready during waitAdTime
@param isTest          Use test advertisement or not
 */
- (void)preloadaAndShowSplashAd:(NSString *)slotid delegate:(id)delegate window:(UIWindow*)window launchImage:(UIImage*)launchImage customAdView:(UIView*)view waitAdTime:(float)waitTime isTest:(BOOL)isTest;

/**
 Show Splash ad
 Call this method after preload Splash ad success
 */
//- (void)showSplashlAd;


@end
