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

@implementation FSQPCurrentLocation (Json)

- (NSString *)json {
    NSMutableDictionary *currentLocationDict = [NSMutableDictionary dictionary];
    currentLocationDict[@"currentPlace"] = [[self class] currentPlaceDict:self.currentPlace];
    currentLocationDict[@"matchedGeofences"] = [[self class] matchedGeofencesArray:self.matchedGeofences];

    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:currentLocationDict options:0 error:nil];
    return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
}

+ (NSDictionary *)currentPlaceDict:(FSQPVisit *)currentPlace {
    NSMutableDictionary *currentPlaceDict = [NSMutableDictionary dictionary];

    currentPlaceDict[@"location"] = @{@"latitude": @(currentPlace.arrivalLocation.coordinate.latitude),
                                      @"longitude": @(currentPlace.arrivalLocation.coordinate.longitude)};

    currentPlaceDict[@"arrivalTime"] = @(currentPlace.arrivalDate.timeIntervalSince1970);

    if (currentPlace.venue) {
        currentPlaceDict[@"venue"] = [self venueDict:currentPlace.venue];
    }

    return currentPlaceDict;
}

+ (NSArray *)matchedGeofencesArray:(NSArray<FSQPGeofenceEvent *> *)matchedGeofences {
    NSMutableArray *matchedGeofencesArray = [NSMutableArray array];

    for (FSQPGeofenceEvent *event in matchedGeofences) {
        NSMutableDictionary *geofenceEventDict = [NSMutableDictionary dictionary];

        geofenceEventDict[@"venue"] = [self venueDict:event.venue];
        geofenceEventDict[@"location"] = @{@"latitude": @(event.location.coordinate.latitude),
                                           @"longitude": @(event.location.coordinate.longitude)};
        geofenceEventDict[@"timestamp"] = @(event.timestamp.timeIntervalSince1970);

        [matchedGeofencesArray addObject:geofenceEventDict];
    }

    return matchedGeofencesArray;
}

+ (NSDictionary *)venueDict:(FSQPVenue *)venue {
    NSMutableDictionary *venueDict = [NSMutableDictionary dictionary];

    venueDict[@"id"] = venue.foursquareID;
    venueDict[@"name"] = venue.name;

    if (venue.locationInformation) {
        NSMutableDictionary *venueLocationDict = [NSMutableDictionary dictionary];

        if (venue.locationInformation.address) {
            venueLocationDict[@"address"] = venue.locationInformation.address;
        }
        if (venue.locationInformation.crossStreet) {
            venueLocationDict[@"crossStreet"] = venue.locationInformation.crossStreet;
        }
        if (venue.locationInformation.city) {
            venueLocationDict[@"city"] = venue.locationInformation.city;
        }
        if (venue.locationInformation.state) {
            venueLocationDict[@"state"] = venue.locationInformation.state;
        }
        if (venue.locationInformation.postalCode) {
            venueLocationDict[@"postalCode"] = venue.locationInformation.postalCode;
        }
        if (venue.locationInformation.country) {
            venueLocationDict[@"country"] = venue.locationInformation.country;
        }
        venueLocationDict[@"coordinate"] = @{@"latitude": @(venue.locationInformation.coordinate.latitude),
                                             @"longitude": @(venue.locationInformation.coordinate.longitude)};

        venueDict[@"location"] = venueLocationDict;
    }

    return venueDict;
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
            self.getCurrentLocationCallback(self.clientHandlePtr, NO, nil);
            return;
        }
        
        self.getCurrentLocationCallback(self.clientHandlePtr, YES, [[currentLocation json] cStringUsingEncoding:NSUTF8StringEncoding]);
    }];
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
