//
//  FSQPVisit+JSON.m
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import "FSQPVisit+JSON.h"

#import "CLLocation+JSON.h"
#import "FSQPVenue+JSON.h"

@implementation FSQPVisit (JSON)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];

    jsonDict[@"_location"] = [self.arrivalLocation json];
    jsonDict[@"_locationType"] = @(self.locationType);
    jsonDict[@"_confidence"] = @(self.confidence);
    jsonDict[@"_arrivalTime"] = @(self.arrivalDate.timeIntervalSince1970);

    if (self.venue) {
        jsonDict[@"_venue"] = [self.venue json];
    }

    NSMutableArray *otherPossibleVenuesArray = [NSMutableArray array];
    if (self.otherPossibleVenues) {
        for (FSQPVenue *venue in self.otherPossibleVenues) {
            [otherPossibleVenuesArray addObject:[venue json]];
        }
    }
    jsonDict[@"_otherPossibleVenues"] = otherPossibleVenuesArray;

    return jsonDict;
}

@end
