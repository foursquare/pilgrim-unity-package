//
//  PilgrimClient.h
//  PilgrimUnitySDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "PilgrimTypes.h"

NS_ASSUME_NONNULL_BEGIN

@interface PilgrimClient : NSObject

- (instancetype)initWithClientHandle:(PilgrimClientHandleRef)clientHandleRef;

@property (assign) PilgrimLocationPermissionsCallback locationPermissionsCallback;

- (void)setUserInfo:(const char *)userInfoJson;

- (void)requestLocationPermissions;

- (void)start;

- (void)stop;

- (void)clearAllData;

@end

NS_ASSUME_NONNULL_END
