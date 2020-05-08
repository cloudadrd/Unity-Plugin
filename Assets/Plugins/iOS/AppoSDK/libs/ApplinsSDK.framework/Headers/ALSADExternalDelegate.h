//
//  ApplinsSDK
//

#import <UIKit/UIKit.h>

@class ALSNativeAd;

#pragma mark ElementAd Delegate

@protocol ALSNativeAdDelegate <NSObject>

@optional
/**
 * User click the advertisement.
 */
-(void)ALSNativeADClicked:(UIView *)nativeAd;

-(void)ALSNativeWillJumpToAppStore:(UIView *)nativeAd;
/**
 * Advertisement landing page will show.
 */
-(void)ALSNativeAdDidIntoLandingPage:(UIView *)nativeAd;
/**
 * User left the advertisement landing page.
 */
-(void)ALSNativeAdDidLeaveLandingPage:(UIView *)nativeAd;
/**
 * Leave App
 */
-(void)ALSNativeAdWillLeaveApplication:(UIView *)nativeAd;
/**
 * Jump failure
 */
-(void)ALSNativeAdJumpfailed:(UIView*)nativeAd;
@end


#pragma mark ALSAppWall Delegate

@protocol ALSAppWallDelegate <NSObject>

@optional
/**
 * User click the advertisement.
 */
-(void)ALSAppWallDidClick:(ALSNativeAd *)nativeAd;
/**
 * Advertisement landing page will show.
 */
-(void)ALSAppWallDidIntoLandingPage:(ALSNativeAd *)nativeAd;
/**
 * User left the advertisement landing page.
 */
-(void)ALSAppWallDidLeaveLandingPage:(ALSNativeAd *)nativeAd;
/**
 * Leave App
 */
-(void)ALSAppWallWillLeaveApplication:(ALSNativeAd *)nativeAd;
/**
 * User close the advertisement.
 */
-(void)ALSAppWallClosed;
/**
 * Jump failure
 */
-(void)ALSAppWallJumpfail:(ALSNativeAd*)nativeAd;
@end
