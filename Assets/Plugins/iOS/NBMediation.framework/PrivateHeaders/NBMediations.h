// Copyright 2020 ADTIMING TECHNOLOGY COMPANY LIMITED
// Licensed under the GNU Lesser General Public License Version 3

#import <Foundation/Foundation.h>
#import "NBMediationConstant.h"

typedef void(^OMMediationInitCompletionBlock)(NSError *_Nullable error);

NS_ASSUME_NONNULL_BEGIN

typedef NS_ENUM(NSInteger, NBAdnSDKInitState) {
    OMAdnSDKInitStateDefault = 0,
    OMAdnSDKInitStateInitializing = 1,
    OMAdnSDKInitStateInitialized = 2,
};

@interface NBMediations : NSObject

@property(nonatomic, strong)NSDictionary *adnNameMap;
@property(nonatomic, strong)NSDictionary *adnSdkClassMap;
@property (nonatomic, strong) NSMutableDictionary *adnSDKInitState;
@property(nonatomic, strong)NSDictionary *adNetworkInfo;

+ (BOOL)importAdnSDK:(NBAdNetwork)adnID;
+ (NSString*)adnName:(NBAdNetwork)adnID;
+ (void)validateIntegration;

+ (instancetype)sharedInstance;
- (void)initAdNetworkSDKWithId:(NBAdNetwork)adnID completionHandler:(OMMediationInitCompletionBlock)completionHandler;

- (BOOL)adnSDKInitialized:(NBAdNetwork)adnID;

@end

NS_ASSUME_NONNULL_END
