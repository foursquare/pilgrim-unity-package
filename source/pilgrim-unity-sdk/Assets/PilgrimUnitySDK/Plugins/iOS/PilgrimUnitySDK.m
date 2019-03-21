//
//  PilgrimUnitySDK.m
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import "PilgrimUnitySDK.h"

#import <Pilgrim/Pilgrim.h>

@implementation PilgrimUnitySDK

+ (void)initWithConsumerKey:(NSString *)consumerKey consumerSecret:(NSString *)consumerSecret {
    [FSQPPilgrimManager sharedManager].debugLogsEnabled = true;
    
    [[FSQPPilgrimManager sharedManager] configureWithConsumerKey:consumerKey
                                                          secret:consumerSecret
                                                        delegate:nil
                                                      completion:nil];
}

@end
