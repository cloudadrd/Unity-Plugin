//
//  NBBanner.h
//  OpenMediation SDK
//
//  Copyright 2017 OM Inc. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>
#import "NBBannerDelegate.h"

NS_ASSUME_NONNULL_BEGIN

/// Banner Ad Size
typedef NS_ENUM(NSInteger, NBBannerType) {
    NBBannerTypeDefault = 0,        ///ad size: 320 x 50
    NBBannerTypeMediumRectangle = 1,///ad size: 300 x 250
    NBBannerTypeLeaderboard = 2     ///ad size: 728x90
};

/// Banner Ad layout attribute
typedef NS_ENUM(NSInteger, NBBannerLayoutAttribute) {
    NBBannerLayoutAttributeTop = 0,
    NBBannerLayoutAttributeLeft = 1,
    NBBannerLayoutAttributeBottom = 2,
    NBBannerLayoutAttributeRight = 3,
    NBBannerLayoutAttributeHorizontally = 4,
    NBBannerLayoutAttributeVertically = 5
};

/// A customized UIView to represent a OpenMediation ad (banner ad).
@interface NBBanner : UIView

@property(nonatomic, readonly, nullable) NSString *placementID;

/// the delegate
@property (nonatomic, weak)id<NBBannerDelegate> delegate;

/// The banner's ad placement ID.
- (NSString*)placementID;


/// This is a method to initialize an NBBanner.
/// type: The size of the ad. Default is OMBannerTypeDefault.
/// placementID: Typed access to the id of the ad placement.
- (instancetype)initWithBannerType:(NBBannerType)type placementID:(NSString *)placementID;

/// set the banner position.
- (void)addLayoutAttribute:(NBBannerLayoutAttribute)attribute constant:(CGFloat)constant;

/// Begins loading the NBBanner content. And to show with default controller([UIApplication sharedApplication].keyWindow.rootViewController) when load success.
- (void)loadAndShow;

@end

NS_ASSUME_NONNULL_END
