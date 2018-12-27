//
//  ALSNativeModelDelegate.h
//  ApplinsSDK
//

#import <Foundation/Foundation.h>

@protocol ALSNativeModelDelegate <NSObject>

@optional
/**
 * Advertisement landing page will show.
 */
-(void)ALSNativeWillJumpToAppStore:(NSObject *)nativeModel;
/**
 * Leave App
 */
-(void)ALSNativeAdWillLeaveApplication:(NSObject *)nativeModel;
/**
 * Jump failure
 */
-(void)ALSNativeAdJumpfailed:(NSObject *)nativeModel;

@end
