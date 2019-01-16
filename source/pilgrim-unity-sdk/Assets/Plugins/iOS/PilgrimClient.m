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

@interface CLLocation (Json)
- (NSDictionary *)json;
@end

@interface FSQPCurrentLocation (Json)
- (NSDictionary *)json;
@end

@interface FSQPVisit (Json)
- (NSDictionary *)json;
@end

@interface FSQPGeofenceEvent (Json)
- (NSDictionary *)json;
@end

@interface FSQPChain (Json)
- (NSDictionary *)json;
@end

@interface FSQPCategory (Json)
- (NSDictionary *)json;
@end

@interface FSQPCategoryIcon (Json)
- (NSDictionary *)json;
@end

@interface FSQPVenue (Json)
- (NSDictionary *)json;
+ (NSArray<NSDictionary *> *)categoriesArrayJson:(NSArray<FSQPCategory *> *)categories;
@end

@implementation CLLocation (Json)

- (NSDictionary *)json {
    return @{@"latitude": @(self.coordinate.latitude),
             @"longitude": @(self.coordinate.longitude)};
}

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

    jsonDict[@"location"] = [self.arrivalLocation json];
    jsonDict[@"locationType"] = @(self.locationType);
    jsonDict[@"confidence"] = @(self.confidence);
    jsonDict[@"arrivalTime"] = @(self.arrivalDate.timeIntervalSince1970);

    if (self.venue) {
        jsonDict[@"venue"] = [self.venue json];
    }

    if (self.otherPossibleVenues) {
        NSMutableArray *otherPossibleVenuesArray = [NSMutableArray array];
        for (FSQPVenue *venue in self.otherPossibleVenues) {
            [otherPossibleVenuesArray addObject:[venue json]];
        }
        jsonDict[@"otherPossibleVenues"] = otherPossibleVenuesArray;
    }

    return jsonDict;
}

@end

@implementation FSQPGeofenceEvent (Json)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];
    jsonDict[@"venueId"] = self.venueID;

    if (self.categoryIDs) {
        NSMutableArray *categoryIDsArray = [NSMutableArray array];
        for (NSString *categoryID in self.categoryIDs) {
            [categoryIDsArray addObject:categoryID];
        }
        jsonDict[@"categoryIds"] = categoryIDsArray;
    }

    if (self.chainIDs) {
        NSMutableArray *chainIDsArray = [NSMutableArray array];
        for (NSString *chainID in self.chainIDs) {
            [chainIDsArray addObject:chainID];
        }
        jsonDict[@"chainIds"] = chainIDsArray;
    }

    if (self.partnerVenueID) {
        jsonDict[@"partnerVenueId"] = self.partnerVenueID;
    }

    jsonDict[@"venue"] = [self.venue json];
    jsonDict[@"location"] = [self.location json];
    jsonDict[@"timestamp"] = @(self.timestamp.timeIntervalSince1970);
    return jsonDict;
}

@end

@implementation FSQPChain (Json)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];
    jsonDict[@"id"] = self.foursquareID;
    jsonDict[@"name"] = self.name;
    return jsonDict;
}

@end

@implementation FSQPCategory (Json)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];
    jsonDict[@"id"] = self.foursquareID;
    jsonDict[@"name"] = self.name;

    if (self.pluralName) {
        jsonDict[@"pluralName"] = self.pluralName;
    }

    if (self.shortName) {
        jsonDict[@"shortName"] = self.shortName;
    }

    if (self.icon) {
        jsonDict[@"icon"] = [self.icon json];
    }

    jsonDict[@"isPrimary"] = @(self.isPrimary);

    return jsonDict;
}

@end

@implementation FSQPCategoryIcon (Json)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];
    jsonDict[@"prefix"] = self.prefix;
    jsonDict[@"suffix"] = self.suffix;
    return jsonDict;
}

@end

@implementation FSQPVenue (Json)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];

    jsonDict[@"id"] = self.foursquareID;
    jsonDict[@"name"] = self.name;

    if (self.locationInformation) {
        NSMutableDictionary *locationInformationDict = [NSMutableDictionary dictionary];

        if (self.locationInformation.address) {
            locationInformationDict[@"address"] = self.locationInformation.address;
        }
        if (self.locationInformation.crossStreet) {
            locationInformationDict[@"crossStreet"] = self.locationInformation.crossStreet;
        }
        if (self.locationInformation.city) {
            locationInformationDict[@"city"] = self.locationInformation.city;
        }
        if (self.locationInformation.state) {
            locationInformationDict[@"state"] = self.locationInformation.state;
        }
        if (self.locationInformation.postalCode) {
            locationInformationDict[@"postalCode"] = self.locationInformation.postalCode;
        }
        if (self.locationInformation.country) {
            locationInformationDict[@"country"] = self.locationInformation.country;
        }
        locationInformationDict[@"location"] = @{@"latitude": @(self.locationInformation.coordinate.latitude),
                                                 @"longitude": @(self.locationInformation.coordinate.longitude)};

        jsonDict[@"locationInformation"] = locationInformationDict;
    }

    if (self.partnerVenueId) {
        jsonDict[@"partnerVenueId"] = self.partnerVenueId;
    }

    if (self.probability) {
        jsonDict[@"probability"] = self.probability;
    }

    NSMutableArray *chainsArray = [NSMutableArray array];
    for (FSQPChain *chain in self.chains) {
        [chainsArray addObject:[chain json]];
    }
    jsonDict[@"chains"] = chainsArray;

    jsonDict[@"categories"] = [FSQPVenue categoriesArrayJson:self.categories];

    NSMutableArray *hierarchyArray = [NSMutableArray array];
    for (FSQPVenue *venueParent in self.hierarchy) {
        NSMutableDictionary *venueParentDict = [NSMutableDictionary dictionary];
        venueParentDict[@"id"] = venueParent.foursquareID;
        venueParentDict[@"name"] = venueParent.name;
        venueParentDict[@"categories"] = [FSQPVenue categoriesArrayJson:venueParent.categories];
    }
    jsonDict[@"hierarchy"] = hierarchyArray;

    return jsonDict;
}

+ (NSArray<NSDictionary *> *)categoriesArrayJson:(NSArray<FSQPCategory *> *)categories {
    NSMutableArray *categoriesArray = [NSMutableArray array];
    for (FSQPCategory *category in categories) {
        [categoriesArray addObject:[category json]];
    }
    return categoriesArray;
}

@end

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

- (void)setUserInfo:(const char *)userInfoJson persisted:(BOOL)persisted {
    NSData *data = [NSData dataWithBytes:userInfoJson length:strlen(userInfoJson)];
    NSDictionary *userInfoDict = [NSJSONSerialization JSONObjectWithData:data options:0 error:nil];

    if ([userInfoDict count] == 0) {
        [[FSQPPilgrimManager sharedManager] setUserInfo:nil persisted:persisted];
        return;
    }

    FSQPUserInfo *userInfo = [[FSQPUserInfo alloc] init];

    NSString *userId = userInfoDict[@"userId"];
    [userInfo setUserId:userId];

    NSString *birthday = userInfoDict[@"birthday"];
    if (birthday) {
        NSTimeInterval seconds = [birthday doubleValue];
        NSDate *birthday = [NSDate dateWithTimeIntervalSince1970:seconds];
        [userInfo setBirthday:birthday];
    } else {
        [userInfo setBirthday:nil];
    }

    NSString *gender = userInfoDict[@"gender"];
    [userInfo setGender:gender];

    NSMutableDictionary *customUserInfoDict = [userInfoDict mutableCopy];
    [customUserInfoDict removeObjectsForKeys:@[@"userId", @"birthday", @"gender"]];

    for (NSString *key in customUserInfoDict) {
        NSString *value = userInfoDict[key];
        [userInfo setUserInfo:value forKey:key];
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

#pragma mark - CLLocationManagerDelegate methods

- (void)locationManager:(CLLocationManager *)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)status {
    if (!self.wasLocationRequested) {
        return;
    }
    self.locationRequested = NO;

    if (status != kCLAuthorizationStatusNotDetermined) {
        BOOL granted = status == kCLAuthorizationStatusAuthorizedAlways || status == kCLAuthorizationStatusAuthorizedWhenInUse;
        self.locationPermissionsCallback(self.clientHandlePtr, granted);
    }
}

@end

NS_ASSUME_NONNULL_END
