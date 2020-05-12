//
//  OMCloudmobiAdapter.h
//  OMCloudmobiAdapter
//
//  Created by yeahmobi on 2020/5/12.
//  Copyright © 2020 AdTiming. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "OMMediationAdapter.h"
#import "OMCloudmobiClass.h"

//! Project version number for OMCloudmobiAdapter.
FOUNDATION_EXPORT double OMCloudmobiAdapterVersionNumber;

//! Project version string for OMCloudmobiAdapter.
FOUNDATION_EXPORT const unsigned char OMCloudmobiAdapterVersionString[];

// In this header, you should import all the public headers of your framework using statements like #import <OMCloudmobiAdapter/PublicHeader.h>
static NSString * const LASAdapterVersion = @"1.0.2";

@interface OMCloudmobiAdapter : NSObject <OMMediationAdapter>
+ (NSString*)adapterVerison;
+ (void)initSDKWithConfiguration:(NSDictionary *)configuration completionHandler:(OMMediationAdapterInitCompletionBlock)completionHandler;
@end
