//
//  ALSSplashAdDelegate.h
//  ApplinsSDK
//
//  Created by Mirinda on 2020/1/13.
//  Copyright © 2020 Mirinda. All rights reserved.
//

#ifndef ALSSplashAdDelegate_h
#define ALSSplashAdDelegate_h
@protocol ALSSplashAdDelegate <NSObject>
- (void)ALSSplashAdIsShow;
- (void)ALSSplashAdFailed:(NSError*)error;
- (void)ALSSplashAdClicked;
- (void)ALSSplashAdJumpedFailed;
@end

#endif /* ALSSplashAdDelegate_h */
