//
//  NBBannerDelegate.h
//  OM SDK
//
//  Copyright 2017 OM Inc. All rights reserved.
//
#import <Foundation/Foundation.h>

@class NBBanner;

NS_ASSUME_NONNULL_BEGIN

/// The methods declared by the NBBannerDelegate protocol allow the adopting delegate to respond to messages from the NBBanner class and thus respond to operations such as whether the ad has been loaded, the person has clicked the ad.
@protocol NBBannerDelegate<NSObject>

@optional

/// Sent when an ad has been successfully loaded.
- (void)NBBannerDidLoad:(NBBanner *)banner;

/// Sent after an NBBanner fails to load the ad.
- (void)NBBanner:(NBBanner *)banner didFailWithError:(NSError *)error;

/// Sent immediately before the impression of an NBBanner object will be logged.
- (void)NBBannerWillExposure:(NBBanner *)banner;

/// Sent after an ad has been clicked by the person.
- (void)NBBannerDidClick:(NBBanner *)banner;

/// Sent when a banner is about to present a full screen content
- (void)NBBannerWillPresentScreen:(NBBanner *)banner;

/// Sent after a full screen content has been dismissed.
- (void)NBBannerDidDismissScreen:(NBBanner *)banner;

 /// Sent when a user would be taken out of the application context.
- (void)NBBannerWillLeaveApplication:(NBBanner *)banner;

@end

NS_ASSUME_NONNULL_END
