//
//  CTServiceU3D.m
//  CTSDK
//
//  Created by yeahmobi on 2017/11/13.
//  Copyright © 2017年 Mirinda. All rights reserved.
//
#import <CTSDK/CTSDK.h>

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
        [[CTService shareManager] setDelegateObjName:CreateNSString(delegateName)];
    }
    
    void CLoadRequestGetCTSDKConfigBySlot_id(const char* slot_id){
        [[CTService shareManager] loadRequestGetCTSDKConfigBySlot_id:CreateNSString(slot_id)];
    }
    
    void CLoadRewardVideoWithSlotId(const char* slot_id){
        [[CTService shareManager] loadRewardVideoWithSlotId:CreateNSString(slot_id) delegate:nil];
    }
    
    void CShowRewardVideo(){
        [[CTService shareManager] showRewardVideo];
    }
    
    bool CCheckRewardVideoIsReady(){
        return [[CTService shareManager] checkRewardVideoIsReady];
    }
    
    void CPreloadAdInterstitialWithSlotId(const char* slot_id){
        [[CTService shareManager] preloadInterstitialAdWithSlotId:CreateNSString(slot_id) delegate:nil isTest:NO];
    }
    
    void CShowInterstitial(){
        [[CTService shareManager] interstitialAdShow];
    }
    
    bool CCheckInterstitialIsReady(){
        return [[CTService shareManager] interstitialAdIsReady];
    }
}

