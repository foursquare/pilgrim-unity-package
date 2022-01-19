//
//  FSQPGeofenceEvent+JSON.m
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import "FSQPGeofenceEvent+JSON.h"

#import "CLLocation+JSON.h"
#import "FSQPVenue+JSON.h"

@implementation FSQPGeofenceEvent (JSON)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];
    jsonDict[@"_id"] = self.geofenceID;

    if (self.venue != nil) {
        FSQPVenue *venue = self.venue;
        jsonDict[@"_venueId"] = venue.foursquareID;
        jsonDict[@"_venue"] = [self.venue json];
    }

    if (self.partnerVenueID) {
        jsonDict[@"_partnerVenueId"] = self.partnerVenueID;
    }

    jsonDict[@"_location"] = [self.location json];
    jsonDict[@"_timestamp"] = @(self.timestamp.timeIntervalSince1970);
    return jsonDict;
}

@end
