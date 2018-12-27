//
//  AppoServiceU3D.m
//  AppoSDK
//
//  Created by applins on 2017/11/13.
//  Copyright © 2018年 t. All rights reserved.
//
#import <ApplinsSDK/ApplinsSDK.h>

//convert char* to Utf8 String
NSString* CreateNSString (const char* string)
{
    if (string)
        return [NSString stringWithUTF8String: string];
    else
        return [NSString stringWithUTF8String: ""];
}

extern "C"{
    void CSetDelegateObjName(const char* delegateName){
        [[Applins shareSDK] setDelegateObjName:CreateNSString(delegateName)];
    }
    
    void CLoadRequestGetAppoSDKConfigBySlot_id(const char* slot_id){
        [[Applins shareSDK] initSDK:CreateNSString(slot_id)];
    }
    
    void CLoadRewardVideoWithSlotId(const char* slot_id){
        [[Applins shareSDK] preloadRewardedVideoAD:CreateNSString(slot_id) delegate:nil];
    }
    
    void CShowRewardVideo(){
        [[Applins shareSDK] showRewardedVideo];
    }
    
    bool CCheckRewardVideoIsReady(){
        return [[Applins shareSDK] isRewardedVideoReady];
    }
    
    void CPreloadAdInterstitialWithSlotId(const char* slot_id){
        [[Applins shareSDK] preloadInterstitialAd:CreateNSString(slot_id) delegate:nil isTest:NO];
    }
    
    void CShowInterstitial(){
        [[Applins shareSDK] showInterstitialAD];
    }
    
    bool CCheckInterstitialIsReady(){
        return [[Applins shareSDK] isInterstitialReady];
    }
    
    void CUploadConsentValue(const char* consentValue, const char* consentType){
        [[Applins shareSDK] uploadConsentValue:CreateNSString(consentValue) consentType:CreateNSString(consentType) complete:nil];
    }
}

