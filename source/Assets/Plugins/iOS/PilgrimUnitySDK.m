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
    [self setUserInfo];
    [self restartIfPreviouslyStarted];
}

+ (void)setUserInfo {
    NSDictionary *userInfoDict = [[NSUserDefaults standardUserDefaults] objectForKey:@"UserInfo"];

    if ([userInfoDict count] == 0) {
        return;
    }

    for (NSString *key in userInfoDict) {
        NSString *value = userInfoDict[key];

        if ([key isEqualToString:@"userId"]) {
            [[FSQPPilgrimManager sharedManager].userInfo setUserId:value];
        } else if ([key isEqualToString:@"birthday"]) {
            NSTimeInterval seconds = [value doubleValue];
            NSDate *birthday = [NSDate dateWithTimeIntervalSince1970:seconds];
            [[FSQPPilgrimManager sharedManager].userInfo setBirthday:birthday];
        } else if ([key isEqualToString:@"gender"]) {
            [[FSQPPilgrimManager sharedManager].userInfo setGender:value];
        } else {
            [[FSQPPilgrimManager sharedManager].userInfo setUserInfo:value forKey:key];
        }
    }
}

+ (void)restartIfPreviouslyStarted {
    if ([[NSUserDefaults standardUserDefaults] boolForKey:@"Started"]) {
        [[FSQPPilgrimManager sharedManager] start];
    }
}

@end
