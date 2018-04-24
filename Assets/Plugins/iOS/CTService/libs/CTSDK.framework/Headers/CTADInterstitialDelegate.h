//
//  CTADInterstitialDelegate.h
//  CTSDK
//
//  Created by Mirinda on 2018/4/4.
//  Copyright © 2018年 Mirinda. All rights reserved.
//

#import <Foundation/Foundation.h>

@protocol CTADInterstitialDelegate <NSObject>

@optional

/**
 * Get Ad Success.
 */
-(void)CTADInterstitialGetAdSuccess;

/**
 * Get Ad Error.
 */
-(void)CTADInterstitialGetAdFailed:(NSError *)error;

/**
 * User click the advertisement.
 */
-(void)CTADInterstitialDidClick;

/**
 * Ad show error.
 */
- (void)CTADInterstitialAdShowFailed:(NSError *)error;

/**
 * jump to LandingPage.
 */
- (void)CTADInterstitialDidIntoLandingPage;

/**
 * jump to LandingPage failed.
 */
- (void)CTADInterstitialJumpFailed;

/**
 * User close the advertisement.
 */
-(void)CTADInterstitialClosed;
@end
