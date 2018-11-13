//
//  PilgrimClient.m
//  PilgrimUnitySDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import "PilgrimClient.h"
#import <CoreLocation/CoreLocation.h>
#import <Pilgrim/Pilgrim.h>

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

    [[NSUserDefaults standardUserDefaults] setObject:userInfoDict forKey:@"UserInfo"];
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

#pragma mark - CLLocationManagerDelegate methods

- (void)locationManager:(CLLocationManager *)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)status {
    if (status != kCLAuthorizationStatusNotDetermined) {
        BOOL granted = status == kCLAuthorizationStatusAuthorizedAlways || status == kCLAuthorizationStatusAuthorizedWhenInUse;
        self.locationPermissionsCallback(self.clientHandlePtr, granted);
    }
}

@end

