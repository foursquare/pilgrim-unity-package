//
//  FSQPCurrentLocation+JSON.m
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import "FSQPCurrentLocation+JSON.h"

#import "FSQPGeofenceEvent+JSON.h"
#import "FSQPVisit+JSON.h"

@implementation FSQPCurrentLocation (JSON)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];
    jsonDict[@"_currentPlace"] = [self.currentPlace json];

    NSMutableArray *geofences = [NSMutableArray array];
    for (FSQPGeofenceEvent *event in self.matchedGeofences) {
        [geofences addObject:[event json]];
    }
    jsonDict[@"_matchedGeofences"] = geofences;

    return jsonDict;
}

@end
