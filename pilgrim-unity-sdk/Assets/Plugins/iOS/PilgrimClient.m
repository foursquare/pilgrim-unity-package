//
//  PilgrimClient.m
//  PilgrimUnitySDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import "PilgrimClient.h"
#import <CoreLocation/CoreLocation.h>
#import <Pilgrim/Pilgrim.h>
#import "FSQPCurrentLocation+Json.h"

@interface PilgrimClient () <CLLocationManagerDelegate>

@property (nonatomic) PilgrimClientHandleRef clientHandlePtr;

@property (nonatomic) CLLocationManager *locationManager;

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

- (void)setUserInfo:(const char *)userInfoJson {
    NSData *data = [NSData dataWithBytes:userInfoJson length:strlen(userInfoJson)];
    NSDictionary *userInfoDict = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];
    [[NSUserDefaults standardUserDefaults] setObject:userInfoDict forKey:@"UserInfo"];

    NSString *userId = userInfoDict[@"userId"];
    [[FSQPPilgrimManager sharedManager].userInfo setUserId:userId];

    NSString *birthday = userInfoDict[@"birthday"];
    if (birthday) {
        NSTimeInterval seconds = [birthday doubleValue];
        NSDate *birthday = [NSDate dateWithTimeIntervalSince1970:seconds];
        [[FSQPPilgrimManager sharedManager].userInfo setBirthday:birthday];
    } else {
        [[FSQPPilgrimManager sharedManager].userInfo setBirthday:nil];
    }

    NSString *gender = userInfoDict[@"gender"];
    [[FSQPPilgrimManager sharedManager].userInfo setGender:gender];

    NSMutableDictionary *customUserInfoDict = [userInfoDict mutableCopy];
    [customUserInfoDict removeObjectsForKeys:@[@"userId", @"birthday", @"gender"]];

    // Add custom keys that are nonnull in C#
    for (NSString *key in customUserInfoDict) {
        NSString *value = userInfoDict[key];
        [[FSQPPilgrimManager sharedManager].userInfo setUserInfo:value forKey:key];
    }

    // Remove custom keys set to null in C#
    NSDictionary *existingCustomUserInfoDict = [FSQPPilgrimManager sharedManager].userInfo.source;
    for (NSString *key in existingCustomUserInfoDict) {
        if ([@[@"userId", @"birthday", @"gender"] containsObject:key]) {
            continue;
        }
        if (![[customUserInfoDict allKeys] containsObject:key]) {
            [[FSQPPilgrimManager sharedManager].userInfo removeKey:key];
        }
    }
}

- (void)requestLocationPermissions {
    if ([CLLocationManager authorizationStatus] != kCLAuthorizationStatusAuthorizedAlways ||
        [CLLocationManager authorizationStatus] != kCLAuthorizationStatusAuthorizedWhenInUse) {
        [self.locationManager requestAlwaysAuthorization];
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
            self.getCurrentLocationCallback(self.clientHandlePtr, NO, nil);
            return;
        }
        
        self.getCurrentLocationCallback(self.clientHandlePtr, YES, [[currentLocation json] cStringUsingEncoding:NSUTF8StringEncoding]);
    }];
}

#pragma mark - CLLocationManagerDelegate methods

- (void)locationManager:(CLLocationManager *)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)status {
    if (status != kCLAuthorizationStatusNotDetermined) {
        BOOL granted = status == kCLAuthorizationStatusAuthorizedAlways || status == kCLAuthorizationStatusAuthorizedWhenInUse;
        self.locationPermissionsCallback(self.clientHandlePtr, granted);
    }
}

@end

