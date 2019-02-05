//
//  PilgrimUnitySDK.m
//  PilgrimUnitySDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import "PilgrimUnitySDK.h"
#import <Pilgrim/Pilgrim.h>

@implementation PilgrimUnitySDK

+ (void)initWithConsumerKey:(NSString *)consumerKey consumerSecret:(NSString *)consumerSecret {
    [[FSQPPilgrimManager sharedManager] configureWithConsumerKey:consumerKey
                                                          secret:consumerSecret
                                                        delegate:nil
                                                      completion:^(BOOL didSucceed, NSError * _Nullable error) {
                                                          NSLog(@"");
                                                      }];
    [self restartIfPreviouslyStarted];
}

+ (void)restartIfPreviouslyStarted {
    if ([[NSUserDefaults standardUserDefaults] boolForKey:@"Started"]) {
        [[FSQPPilgrimManager sharedManager] start];
    }
}

@end
