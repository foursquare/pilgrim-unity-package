//
//  PilgrimClient.m
//  PilgrimUnitySDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import "PilgrimClient.h"
#import <CoreLocation/CoreLocation.h>
#import <Pilgrim/Pilgrim.h>

NS_ASSUME_NONNULL_BEGIN

@interface FSQPCurrentLocation (Json)
- (NSDictionary *)json;
@end

@interface FSQPVisit (Json)
- (NSDictionary *)json;
@end

@interface FSQPGeofenceEvent (Json)
- (NSDictionary *)json;
@end

@interface FSQPVenue (Json)
- (NSDictionary *)json;
@end

@implementation FSQPCurrentLocation (Json)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];
    jsonDict[@"currentPlace"] = [self.currentPlace json];

    NSMutableArray *geofences = [NSMutableArray array];
    for (FSQPGeofenceEvent *event in self.matchedGeofences) {
        [geofences addObject:[event json]];
    }
    jsonDict[@"matchedGeofences"] = geofences;

    return jsonDict;
}

@end

@implementation FSQPVisit (Json)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];

    jsonDict[@"location"] = @{@"latitude": @(self.arrivalLocation.coordinate.latitude),
                              @"longitude": @(self.arrivalLocation.coordinate.longitude)};
    jsonDict[@"arrivalTime"] = @(self.arrivalDate.timeIntervalSince1970);
    if (self.venue) {
        jsonDict[@"venue"] = [self.venue json];
    }

    return jsonDict;
}

@end

@implementation FSQPGeofenceEvent (Json)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];

    jsonDict[@"venue"] = [self.venue json];
    jsonDict[@"location"] = @{@"latitude": @(self.location.coordinate.latitude),
                              @"longitude": @(self.location.coordinate.longitude)};
    jsonDict[@"timestamp"] = @(self.timestamp.timeIntervalSince1970);

    return jsonDict;
}

@end

@implementation FSQPVenue (Json)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];

    jsonDict[@"id"] = self.foursquareID;
    jsonDict[@"name"] = self.name;

    if (self.locationInformation) {
        NSMutableDictionary *venueLocationDict = [NSMutableDictionary dictionary];

        if (self.locationInformation.address) {
            venueLocationDict[@"address"] = self.locationInformation.address;
        }
        if (self.locationInformation.crossStreet) {
            venueLocationDict[@"crossStreet"] = self.locationInformation.crossStreet;
        }
        if (self.locationInformation.city) {
            venueLocationDict[@"city"] = self.locationInformation.city;
        }
        if (self.locationInformation.state) {
            venueLocationDict[@"state"] = self.locationInformation.state;
        }
        if (self.locationInformation.postalCode) {
            venueLocationDict[@"postalCode"] = self.locationInformation.postalCode;
        }
        if (self.locationInformation.country) {
            venueLocationDict[@"country"] = self.locationInformation.country;
        }
        venueLocationDict[@"coordinate"] = @{@"latitude": @(self.locationInformation.coordinate.latitude),
                                             @"longitude": @(self.locationInformation.coordinate.longitude)};

        jsonDict[@"location"] = venueLocationDict;
    }

    return jsonDict;
}

@end

@interface PilgrimClient () <CLLocationManagerDelegate>

@property (nonatomic) PilgrimClientHandleRef clientHandlePtr;

@property (nonatomic) CLLocationManager *locationManager;
@property (nonatomic, getter=isInitialPermissionCallback) BOOL initialPermissionCallback;

@end

@implementation PilgrimClient

- (instancetype)initWithClientHandle:(PilgrimClientHandleRef)clientHandlePtr {
    self = [super init];
    if (self) {
        _clientHandlePtr = clientHandlePtr;

        _initialPermissionCallback = YES;

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
    if (!([CLLocationManager authorizationStatus] == kCLAuthorizationStatusAuthorizedAlways ||
        [CLLocationManager authorizationStatus] == kCLAuthorizationStatusAuthorizedWhenInUse)) {
        [self.locationManager requestAlwaysAuthorization];
    } else {
        self.locationPermissionsCallback(self.clientHandlePtr, YES);
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

#pragma mark - CLLocationManagerDelegate methods

- (void)locationManager:(CLLocationManager *)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)status {
    if (self.isInitialPermissionCallback) {
        self.initialPermissionCallback = NO;
        return;
    }

    if (status != kCLAuthorizationStatusNotDetermined) {
        BOOL granted = status == kCLAuthorizationStatusAuthorizedAlways || status == kCLAuthorizationStatusAuthorizedWhenInUse;
        self.locationPermissionsCallback(self.clientHandlePtr, granted);
    }
}

@end

NS_ASSUME_NONNULL_END
