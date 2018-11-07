//
//  PilgrimUnitySDK.m
//  PilgrimUnitySDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import "PilgrimUnitySDK.h"

#import <Pilgrim/Pilgrim.h>

static NSString * const PilgrimConsumerKey = @"Pilgrim_ConsumerKey";
static NSString * const PilgrimConsumerSecret = @"Pilgrim_ConsumerSecret";

@interface Delegate : NSObject <FSQPPilgrimManagerDelegate>
@end

@implementation Delegate
- (void)pilgrimManager:(FSQPPilgrimManager *)pilgrimManager handleVisit:(FSQPVisit *)visit {

}
@end

@implementation PilgrimUnitySDK

+ (void)init {
    NSDictionary<NSString *, NSString *> *info = [[NSBundle mainBundle] infoDictionary];
    NSString *consumerKey = info[PilgrimConsumerKey];
    NSString *consumerSecret = info[PilgrimConsumerSecret];
    [self initWithConsumerKey:consumerKey consumerSecret:consumerSecret];
}

+ (void)initWithConsumerKey:(NSString *)consumerKey consumerSecret:(NSString *)consumerSecret {
    [[FSQPPilgrimManager sharedManager] configureWithConsumerKey:consumerKey
                                                          secret:consumerSecret
                                                        delegate:[[Delegate alloc] init]
                                                      completion:nil];
    [self setUserInfo];
    [self restartIfPreviouslyStarted];
}

+ (void)setUserInfo {

}

+ (void)restartIfPreviouslyStarted {
    
}

@end
