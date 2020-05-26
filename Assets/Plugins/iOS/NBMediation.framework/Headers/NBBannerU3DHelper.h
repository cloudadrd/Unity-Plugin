//
//  NBBannerU3DHelper.h
//  NBMediation
//
//  Created by Mirinda on 2020/5/25.
//  Copyright © 2020 Mirinda. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface NBBannerU3DHelper : NSObject

+(instancetype)shareHelper;
- (void)loadBannerWithPlacementID:(NSString *)pid;
- (BOOL)bannerIsReaday:(NSString *)pid;
- (void)bannerShow:(NSString *)pid;
- (void)bannerHidden:(NSString *)pid destroyIt:(BOOL)destroy;
@end
NS_ASSUME_NONNULL_END
