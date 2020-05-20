//
//  OMNativeView.h
//  OM SDK
//
//  Copyright 2017 OM Inc. All rights reserved.
//

#import <UIKit/UIKit.h>
@class NBNativeAd;
#import "NBNativeMediaView.h"

NS_ASSUME_NONNULL_BEGIN

/// A customized UIView to represent a ad (native ad).
@interface NBNativeView : UIView

@property (nonatomic, strong) NBNativeAd *nativeAd;
@property (nonatomic, strong) NBNativeMediaView *mediaView;

/// This is a method to initialize an OMNativeView.
/// Parameter frame: the OMNativeView frame.
- (instancetype)initWithFrame:(CGRect)frame;

- (void)setClickableViews:(NSArray<UIView *> *)clickableViews; //for Facebook TencentAd

@end

NS_ASSUME_NONNULL_END
