//
//  FSQPCurrentLocation+Json.m
//  PilgrimUnitySDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import "FSQPCurrentLocation+Json.h"

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

    currentPlaceDict[@"arrivalTime"] = @([@(currentPlace.arrivalDate.timeIntervalSince1970) longValue]);

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
        [matchedGeofencesArray addObject:geofenceEventDict];
    }

    return matchedGeofencesArray;
}

+ (NSDictionary *)venueDict:(FSQPVenue *)venue {
    NSMutableDictionary *venueDict = [NSMutableDictionary dictionary];

    venueDict[@"name"] = venue.name;

    return venueDict;
}

@end

NS_ASSUME_NONNULL_END
