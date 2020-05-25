//
//  ALS ElementAd.h
//  ApplinsSDK
//

#import <UIKit/UIKit.h>
#import "ALSElementModel.h"
@protocol ALSNativeAdDelegate;

@interface ALSNativeAd : UIView
/**
 Ad source, you must set it ！
 */
@property (nonatomic, strong) ALSNativeAdModel* adNativeModel;
/**
 Ad callback delegate
 */
@property (nonatomic, weak)   id<ALSNativeAdDelegate> delegate;
/**
 if removeloading is yes, click ad will remove loading animation！
 */
@property (nonatomic, assign) BOOL removeLoading;
/**
 Init method

 @return self
 */
- (instancetype)init;
/**
 Init method

 @param frame Ad size

 @return self
 */
- (instancetype)initWithFrame:(CGRect)frame;

@end
