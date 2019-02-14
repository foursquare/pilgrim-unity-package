//
//  PilgrimClient.m
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import "PilgrimClient.h"

#import <Pilgrim/Pilgrim.h>
#import "FSQPCurrentLocation+JSON.h"

NS_ASSUME_NONNULL_BEGIN

@interface PilgrimClient () <CLLocationManagerDelegate>

@property (nonatomic) PilgrimClientHandleRef clientHandlePtr;

@property (nonatomic) CLLocationManager *locationManager;
@property (nonatomic, getter=wasLocationRequested) BOOL locationRequested;

@end

@implementation PilgrimClient

- (instancetype)initWithClientHandle:(PilgrimClientHandleRef)clientHandlePtr {
    self = [super init];
    if (self) {
        _clientHandlePtr = clientHandlePtr;

        _locationManager = [[CLLocationManager alloc] init];
        _locationManager.delegate = self;
    }
    return self;
}

- (const char *)getUserInfo {
    FSQPUserInfo *userInfo = [FSQPPilgrimManager sharedManager].userInfo;
    if (!userInfo) {
        return nil;
    }

    NSMutableArray *keys = [NSMutableArray array];
    NSMutableArray *values = [NSMutableArray array];

    for (NSString *key in userInfo.source) {
        [keys addObject:key];
        [values addObject:userInfo.source[key]];
    }

    NSDictionary *userInfoDict = @{@"keys":keys, @"values":values};
    NSData *data = [NSJSONSerialization dataWithJSONObject:userInfoDict options:0 error:nil];
    if (!data) {
        return nil;
    }
    const char *userInfoJson = malloc(data.length);
    [data getBytes:(void *)userInfoJson length:data.length];
    return userInfoJson;
}

- (void)setUserInfo:(const char *)userInfoJson persisted:(BOOL)persisted {
    NSData *data = [NSData dataWithBytes:userInfoJson length:strlen(userInfoJson)];
    NSDictionary *userInfoDict = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    NSArray *keys = userInfoDict[@"keys"];
    NSArray *values = userInfoDict[@"values"];

    if (!keys || !values || keys.count != values.count) {
        [[FSQPPilgrimManager sharedManager] setUserInfo:nil persisted:persisted];
        return;
    }

    FSQPUserInfo *userInfo = [[FSQPUserInfo alloc] init];

    for (int i = 0; i < keys.count; ++i) {
        NSString *key = keys[i];
        NSString *value = values[i];

        if ([key isEqualToString:@"userId"]) {
            [userInfo setUserId:value];
        } else if ([key isEqualToString:@"birthday"]) {
            NSTimeInterval seconds = [value doubleValue];
            NSDate *birthday = [NSDate dateWithTimeIntervalSince1970:seconds];
            [userInfo setBirthday:birthday];
        } else if ([key isEqualToString:@"gender"]) {
            [userInfo setGender:value];
        } else {
            [userInfo setUserInfo:value forKey:key];
        }
    }

    [[FSQPPilgrimManager sharedManager] setUserInfo:userInfo persisted:persisted];
}

- (void)requestLocationPermissions {
    CLAuthorizationStatus status = [CLLocationManager authorizationStatus];
    if (status == kCLAuthorizationStatusNotDetermined) {
        self.locationRequested = YES;
        [self.locationManager requestAlwaysAuthorization];
    } else if (status == kCLAuthorizationStatusAuthorizedAlways || status == kCLAuthorizationStatusAuthorizedWhenInUse) {
        self.locationPermissionsCallback(self.clientHandlePtr, YES);
    } else {
        self.locationPermissionsCallback(self.clientHandlePtr, NO);
    }
}

- (void)start {
    [[FSQPPilgrimManager sharedManager] start];
    [[NSUserDefaults standardUserDefaults] setBool:YES forKey:@"Started"];
}

- (void)stop {
    [[FSQPPilgrimManager sharedManager] stop];
    [[NSUserDefaults standardUserDefaults] setBool:NO forKey:@"Started"];
}

- (void)clearAllData {
    [[FSQPPilgrimManager sharedManager] clearAllData:nil];
}

- (void)getCurrentLocation {
    [[FSQPPilgrimManager sharedManager] getCurrentLocationWithCompletion:^(FSQPCurrentLocation * _Nullable currentLocation, NSError * _Nullable error) {
        if (error) {
            self.getCurrentLocationCallback(self.clientHandlePtr, NO, nil, [self getErrorMessage:error]);
            return;
        }

        NSError *jsonError = nil;
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:[currentLocation json] options:0 error:&jsonError];
        if (!jsonError) {
            NSString *json = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            self.getCurrentLocationCallback(self.clientHandlePtr, YES, [json cStringUsingEncoding:NSUTF8StringEncoding], nil);
        } else {
            self.getCurrentLocationCallback(self.clientHandlePtr, NO, nil, [self getErrorMessage:jsonError]);
        }
    }];
}

- (const char *)getErrorMessage:(NSError *)error {
    return [([error localizedDescription] ?: @"Unkown Error") cStringUsingEncoding:NSUTF8StringEncoding];
}

- (void)showDebugScreen {
    UIViewController *viewController = [UIApplication sharedApplication].keyWindow.rootViewController;
    [[FSQPPilgrimManager sharedManager] presentDebugViewController:viewController];
}

- (void)fireTestVisitWithLatitude:(double)latitude longitude:(double)longitude {
    [[FSQPPilgrimManager sharedManager].visitTester fireTestVisit:[[CLLocation alloc] initWithLatitude:latitude longitude:longitude]];
}

#pragma mark - CLLocationManagerDelegate methods

- (void)locationManager:(CLLocationManager *)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)status {
    if (!self.wasLocationRequested) {
        return;
    }

    if (status != kCLAuthorizationStatusNotDetermined) {
        self.locationRequested = NO;

        BOOL granted = status == kCLAuthorizationStatusAuthorizedAlways || status == kCLAuthorizationStatusAuthorizedWhenInUse;
        self.locationPermissionsCallback(self.clientHandlePtr, granted);
    }
}

@end

NS_ASSUME_NONNULL_END
