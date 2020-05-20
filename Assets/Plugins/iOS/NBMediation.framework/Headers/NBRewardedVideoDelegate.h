// Copyright 2020 ADTIMING TECHNOLOGY COMPANY LIMITED
// Licensed under the GNU Lesser General Public License Version 3

#import <Foundation/Foundation.h>
#import "NBScene.h"

NS_ASSUME_NONNULL_BEGIN

@protocol NBRewardedVideoDelegate <NSObject>

@optional

/// Invoked when a rewarded video is available.
- (void)NBRewardedVideoChangedAvailability:(BOOL)available;

/// Sent immediately when a rewarded video is opened.
- (void)NBRewardedVideoDidOpen:(NBScene*)scene;

/// Sent immediately when a rewarded video starts to play.
- (void)NBRewardedVideoPlayStart:(NBScene*)scene;

/// Send after a rewarded video has been completed.
- (void)NBRewardedVideoPlayEnd:(NBScene*)scene;

/// Sent after a rewarded video has been clicked.
- (void)NBRewardedVideoDidClick:(NBScene*)scene;

/// Sent after a user has been granted a reward.
- (void)NBRewardedVideoDidReceiveReward:(NBScene*)scene;

/// Sent after a rewarded video has been closed.
- (void)NBRewardedVideoDidClose:(NBScene*)scene;

/// Sent after a rewarded video has failed to play.
- (void)NBRewardedVideoDidFailToShow:(NBScene*)scene withError:(NSError *)error;

@end

NS_ASSUME_NONNULL_END
