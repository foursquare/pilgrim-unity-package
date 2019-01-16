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

@property (assign) PilgrimGetCurrentLocationCallback getCurrentLocationCallback;

- (void)setUserInfo:(const char *)userInfoJson persisted:(BOOL)persisted;

- (void)requestLocationPermissions;

- (void)start;

- (void)stop;

- (void)clearAllData;

- (void)getCurrentLocation;

@end

NS_ASSUME_NONNULL_END
