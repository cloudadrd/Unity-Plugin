//
//  ALSRewardVideoDelegate.h
//  ApplinsSDK
//
//  Created by 兰旭平 on 2017/3/10.
//  Copyright © 2017年 Mirinda. All rights reserved.
//
#ifndef ALS_NOREWARDEDVIDEO
#import <Foundation/Foundation.h>

@protocol ALSRewardVideoDelegate <NSObject>
@optional
/**
 *  ALSRewardVideo Load success
 **/
- (void)ALSRewardedVideoLoadSuccess;

/**
 *  ALSRewardVideo loading failed
 **/
- (void)ALSRewardVideoLoadingFailed:(NSError *)error;

/**
 *  ALSRewardVideo begin playing Ad
 **/
- (void)ALSRewardedVideoStart;

/**
 *  ALSRewardVideo playing Ad finish
 **/
- (void)ALSRewardedVideoFinish;

/**
 *  ALSRewardVideo Click Ads
 **/
- (void)ALSRewardedVideoClicked;

/**
 * ALSRewardVideo will jump to appstore
 **/
- (void)ALSRewardedVideoWillJumpToAppStore;

/**
 *  ALSRewardVideo jump AppStroe failed
 **/
- (void)ALSRewardedVideoJumpFailed;

/**
 *  ALSRewardVideo closed
 **/
- (void)ALSRewardVideoClosed;

/**
 *  ALSRewardVideo Reward Interface
 **/
- (void)ALSRewardedName:(NSString *)rewardName rewardedAmount:(NSString *)rewardedAmount customParams:(NSString*) customParams;
@end
#endif
