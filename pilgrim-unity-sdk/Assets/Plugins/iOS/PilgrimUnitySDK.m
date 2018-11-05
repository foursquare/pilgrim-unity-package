//
//  PilgrimUnitySDK.m
//  PilgrimUnitySDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import "PilgrimUnitySDK.h"

#import <Pilgrim/Pilgrim.h>

@interface Delegate : NSObject <FSQPPilgrimManagerDelegate>
@end

@implementation Delegate
- (void)pilgrimManager:(FSQPPilgrimManager *)pilgrimManager handleVisit:(FSQPVisit *)visit {

}
@end

@implementation PilgrimUnitySDK

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
