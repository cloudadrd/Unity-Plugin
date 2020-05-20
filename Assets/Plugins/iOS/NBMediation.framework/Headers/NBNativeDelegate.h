//
//  OMNativeDelegate.h
//  OM SDK
//
//  Copyright 2017 OM Inc. All rights reserved.
//

#import <Foundation/Foundation.h>

@class NBNative;
@class NBNativeAd;

NS_ASSUME_NONNULL_BEGIN

/// The methods declared by the NBNativeDelegate protocol allow the adopting delegate to respond to messages from the NBNative class and thus respond to operations such as whether the native ad has been loaded.
@protocol NBNativeDelegate<NSObject>

@optional

/// Sent when an NBNative has been successfully loaded.
- (void)NBNative:(NBNative*)native didLoad:(NBNativeAd*)nativeAd;

/// Sent when an NBNative is failed to load.
- (void)NBNative:(NBNative*)native didFailWithError:(NSError*)error;

/// Sent immediately before the impression of an NBNative object will be logged.
- (void)NBNativeWillExposure:(NBNative*)native;

/// Sent after an ad has been clicked by the person.
- (void)NBNativeDidClick:(NBNative*)native;

@end

NS_ASSUME_NONNULL_END
