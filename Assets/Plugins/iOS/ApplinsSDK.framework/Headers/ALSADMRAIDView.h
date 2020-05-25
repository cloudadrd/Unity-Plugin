//
//  ALSADMRAIDView.h
//  ApplinsSDK
//
//  Created by Mirinda on 2018/5/11.
//  Copyright © 2018年 Mirinda. All rights reserved.
//

#import <UIKit/UIKit.h>
/** Ad placement type.
 */
typedef enum
{
    /// 内容型广告,如banner .
    ALSAdViewPlacementTypeInline = 0,
    
    //插屏
    ALSAdViewPlacementTypeInterstitial
    
} ALSAdViewPlacementType;

/** Event log types.
 */
typedef enum
{
    ALSAdViewLogEventTypeNone = 0,
    ALSAdViewLogEventTypeError = 1,
    ALSAdViewLogEventTypeDebug = 2,
} ALSAdViewLogEventType;


@protocol ALSAdViewDelegate;

@interface ALSADMRAIDView : UIView
@property (nonatomic, weak) id<ALSAdViewDelegate> delegate;
@property (nonatomic, readonly) ALSAdViewPlacementType placementType;
@property (nonatomic, readonly) UIView* expandView;
@property (nonatomic, assign) BOOL modalDismissAfterPresent;
@property (nonatomic, assign) BOOL isReady;
@property (nonatomic, readonly) NSString* slot;

/** Unregisters the protocol class used to intercept the MRAID bridge request
 from rich media ads.
 
 Note: The registered NSURLProtocol class used by the SDK only intercepts requests
 for "mraid.js" from a WebView.
 */
- (void)unregisterProtocolClass;
- (void)mraidInitializeBridge:(id)bridge forWebView:(id)wv;
@end


@protocol ALSAdViewDelegate <NSObject>
@optional

//banner ad
- (void)ALSLoadBannerSuccess:(ALSADMRAIDView*)adView;

//interstitial is ready, call showInterstitialAD to show it.
- (void)ALSLoadInterstitialSuccess;

//interstitial is ready, call showInterstitialAD to show it. include Request Slotid
- (void)ALSLoadInterstitialSuccessWithSlot:(NSString *)slot;

//error while request ads.
- (void)ALSAdView:(ALSADMRAIDView*)adView loadADFailedWithError:(NSError*)error;

//should jump to app store
- (BOOL)ALSAdView:(ALSADMRAIDView*)adView shouldOpenURL:(NSURL*)url;

//mraid ad clicked
- (void)ALSAdViewClicked:(ALSADMRAIDView*)adView;

//mraid ad show
- (void)ALSAdViewShow:(ALSADMRAIDView*)adView;

//did click close button
- (void)ALSAdViewCloseButtonPressed:(ALSADMRAIDView*)adView;

//mraid ad will expand
- (void)ALSAdViewWillExpand:(ALSADMRAIDView*)adView;

//mraid ad did expand
- (void)ALSAdViewDidExpand:(ALSADMRAIDView*)adView;

//mraid ad will resize frame
- (void)ALSAdView:(ALSADMRAIDView *)adView willResizeToFrame:(CGRect)frame;

//mraid ad did resize frame
- (void)ALSAdView:(ALSADMRAIDView *)adView didResizeToFrame:(CGRect)frame;

//mraid ad will collapse from expanded or resize state
- (void)ALSAdViewWillCollapse:(ALSADMRAIDView*)adView;

//mraid ad did collapse from expanded or resize state
- (void)ALSAdViewDidCollapse:(ALSADMRAIDView*)adView;

//will leave application
- (void)ALSAdViewWillLeaveApplication:(ALSADMRAIDView*)adView;

//specify if support sms
- (BOOL)ALSAdViewSupportsSMS:(ALSADMRAIDView*)adView;

//specify if support phone
- (BOOL)ALSAdViewSupportsPhone:(ALSADMRAIDView*)adView;

//specify if support calendar, default return no
//Privacy - Calendars Usage Description
//Privacy - Reminders Usage Description
//- (BOOL)ALSAdViewSupportsCalendar:(ALSADMRAIDView*)adView; // Close this method Temporary

//specify if support store picture, default return no. if return yes, add privilage below to plist
//Privacy - Photo Library Additions Usage Description
//Privacy - Photo Library Usage Description
//- (BOOL)ALSAdViewSupportsStorePicture:(ALSADMRAIDView*)adView;//Close this method Temporary

//specify if support location, default return no. if return yes, add privilage below to plist
//Privacy - Location Always and When In Use Usage Description
//Privacy - Location When In Use Usage Description
//- (BOOL)ALSAdViewSupportsLocation:(ALSADMRAIDView*)adView;//Close this method Temporary

//allow mraid ad play video using safari
- (BOOL)ALSAdView:(ALSADMRAIDView*)adView shouldPlayVideo:(NSString*)videoURL;

//mraid ad save calender event
//- (BOOL)ALSAdView:(ALSADMRAIDView*)adView shouldSaveCalendarEvent:(EKEvent*)event inEventStore:(EKEventStore*)eventStore; //Close this method Temporary

//mraid ad save photo to gallery
//- (BOOL)ALSAdView:(ALSADMRAIDView*)adView shouldSavePhotoToCameraRoll:(UIImage*)image;//Close this method Temporary

//specify mraid ad resize superview
- (UIView*)ALSAdViewResizeSuperview:(ALSADMRAIDView*)adView;

@end

