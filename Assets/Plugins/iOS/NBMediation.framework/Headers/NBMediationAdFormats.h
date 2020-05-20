// Copyright 2020 ADTIMING TECHNOLOGY COMPANY LIMITED
// Licensed under the GNU Lesser General Public License Version 3

#ifndef NBMediationAdFormats_h
#define NBMediationAdFormats_h

typedef NS_ENUM(NSInteger, NBMediationAdFormat) {
    NBMediationAdFormatBanner = (1 << 0),
    NBMediationAdFormatNative = (1 << 1),
    NBMediationAdFormatRewardedVideo = (1 << 2),
    NBMediationAdFormatInterstitial = (1 << 3),
};

#endif /* NBMediationAdFormats_h */
