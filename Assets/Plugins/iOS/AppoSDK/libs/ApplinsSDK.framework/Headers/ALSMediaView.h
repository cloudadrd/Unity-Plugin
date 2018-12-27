//
//  ALSMediaView.h
//  ApplinsSDK
//
//  Created by Applins on 2018/5/2.
//  Copyright © 2018年 Mirinda. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ALSVideoViewController.h"
#import "ALSNativeVideoModel.h"

@interface ALSMediaView : UIView
@property (nonatomic, strong) ALSVideoViewController *videoController;
@property (nonatomic, strong) ALSNativeVideoModel *NativeVideoAd;
@property (nonatomic, assign) BOOL EnableAutoPlay; //auto controll video play or pause, default : NO
@property (nonatomic, assign) BOOL EnableWWANPlay; //allow video play in mobile network (3g/4g etc.) , default : NO
@end
