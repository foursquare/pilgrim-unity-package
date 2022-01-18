//
//  PilgrimClient.h
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import "PilgrimTypes.h"

NS_ASSUME_NONNULL_BEGIN

@interface PilgrimClient : NSObject

- (instancetype)initWithClientHandle:(PilgrimClientHandleRef)clientHandleRef;

@property (assign) PilgrimLocationPermissionsCallback locationPermissionsCallback;

@property (assign) PilgrimGetCurrentLocationCallback getCurrentLocationCallback;

- (const char *)getUserInfo;

- (void)setUserInfo:(const char *)userInfoJson persisted:(BOOL)persisted;

- (void)requestLocationPermissions;

- (void)start;

- (void)stop;

- (void)clearAllData;

- (void)getCurrentLocation;

- (void)showDebugScreen;

- (void)fireTestVisitWithLatitude:(double)latitude longitude:(double)longitude;

@end

NS_ASSUME_NONNULL_END
