//
//  PilgrimUnitySDK.m
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import "PilgrimUnitySDK.h"

#import <Pilgrim/Pilgrim.h>

@interface PilgrimUnitySDKDelegate : NSObject <FSQPPilgrimManagerDelegate>

@end

@implementation PilgrimUnitySDKDelegate

- (void)pilgrimManager:(nonnull FSQPPilgrimManager *)pilgrimManager handleVisit:(nonnull FSQPVisit *)visit {

}

@end

static PilgrimUnitySDKDelegate * delegate = nil;

@implementation PilgrimUnitySDK

+ (void)load {
    delegate = [[PilgrimUnitySDKDelegate alloc] init];
}

+ (void)initWithConsumerKey:(NSString *)consumerKey consumerSecret:(NSString *)consumerSecret {
    [FSQPPilgrimManager sharedManager].debugLogsEnabled = true;

    [[FSQPPilgrimManager sharedManager] configureWithConsumerKey:consumerKey
                                                          secret:consumerSecret
                                                        delegate:delegate
                                                      completion:nil];

    [self restartIfPreviouslyStarted];
}

+ (void)restartIfPreviouslyStarted {
    if ([[NSUserDefaults standardUserDefaults] boolForKey:@"Started"]) {
        [[FSQPPilgrimManager sharedManager] start];
    }
}

@end
