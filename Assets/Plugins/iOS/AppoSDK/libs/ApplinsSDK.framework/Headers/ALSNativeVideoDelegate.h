//
//  ALSNativeVideoDelegate.h
//  ApplinsSDK
//
//  Created by Applins on 2018/2/27.
//  Copyright © 2018年 Mirinda. All rights reserved.
//

#ifndef ALSNativeVideoDelegate_h
#define ALSNativeVideoDelegate_h

@class ALSNativeVideoModel;

@protocol ALSNativeVideoDelegate <NSObject>

@optional
/**
 * Advertisement load success.
 */
-(void)ALSNativeVideoLoadSuccess:(ALSNativeVideoModel *)nativeVideoModel;

/**
 * Advertisement load failed.
 */
-(void)ALSNativeVideoLoadFailed:(NSError *)error;


@end

#endif /* ALSNativeVideoDelegate_h */
