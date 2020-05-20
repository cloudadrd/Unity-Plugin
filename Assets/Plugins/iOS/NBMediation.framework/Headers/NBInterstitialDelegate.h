// Copyright 2020 ADTIMING TECHNOLOGY COMPANY LIMITED
// Licensed under the GNU Lesser General Public License Version 3

#import <Foundation/Foundation.h>
#import "NBScene.h"

NS_ASSUME_NONNULL_BEGIN

@protocol NBInterstitialDelegate <NSObject>

@optional

/// Invoked when a interstitial video is available.
- (void)NBInterstitialChangedAvailability:(BOOL)available;

/// Sent immediately when a interstitial video is opened.
- (void)NBInterstitialDidOpen:(NBScene*)scene;

/// Sent immediately when a interstitial video starts to play.
- (void)NBInterstitialDidShow:(NBScene*)scene;

/// Sent after a interstitial video has been clicked.
- (void)NBInterstitialDidClick:(NBScene*)scene;

/// Sent after a interstitial video has been closed.
- (void)NBInterstitialDidClose:(NBScene*)scene;

/// Sent after a interstitial video has failed to play.
- (void)NBInterstitialDidFailToShow:(NBScene*)scene withError:(NSError *)error;

@end

NS_ASSUME_NONNULL_END
