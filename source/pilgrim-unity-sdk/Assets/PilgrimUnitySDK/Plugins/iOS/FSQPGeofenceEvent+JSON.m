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
    jsonDict[@"_venueId"] = self.venueID;

    if (self.categoryIDs) {
        NSMutableArray *categoryIDsArray = [NSMutableArray array];
        for (NSString *categoryID in self.categoryIDs) {
            [categoryIDsArray addObject:categoryID];
        }
        jsonDict[@"_categoryIds"] = categoryIDsArray;
    }

    if (self.chainIDs) {
        NSMutableArray *chainIDsArray = [NSMutableArray array];
        for (NSString *chainID in self.chainIDs) {
            [chainIDsArray addObject:chainID];
        }
        jsonDict[@"_chainIds"] = chainIDsArray;
    }

    if (self.partnerVenueID) {
        jsonDict[@"_partnerVenueId"] = self.partnerVenueID;
    }

    jsonDict[@"_venue"] = [self.venue json];
    jsonDict[@"_location"] = [self.location json];
    jsonDict[@"_timestamp"] = @(self.timestamp.timeIntervalSince1970);
    return jsonDict;
}

@end
