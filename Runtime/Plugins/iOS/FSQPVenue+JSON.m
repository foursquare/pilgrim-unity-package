//
//  FSQPVenue+JSON.m
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import "FSQPVenue+JSON.h"

#import "FSQPCategory+JSON.h"
#import "FSQPChain+JSON.h"

@implementation FSQPVenue (JSON)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];

    jsonDict[@"_id"] = self.foursquareID;
    jsonDict[@"_name"] = self.name;

    if (self.locationInformation) {
        NSMutableDictionary *locationInformationDict = [NSMutableDictionary dictionary];

        if (self.locationInformation.address) {
            locationInformationDict[@"_address"] = self.locationInformation.address;
        }
        if (self.locationInformation.crossStreet) {
            locationInformationDict[@"_crossStreet"] = self.locationInformation.crossStreet;
        }
        if (self.locationInformation.city) {
            locationInformationDict[@"_city"] = self.locationInformation.city;
        }
        if (self.locationInformation.state) {
            locationInformationDict[@"_state"] = self.locationInformation.state;
        }
        if (self.locationInformation.postalCode) {
            locationInformationDict[@"_postalCode"] = self.locationInformation.postalCode;
        }
        if (self.locationInformation.country) {
            locationInformationDict[@"_country"] = self.locationInformation.country;
        }
        locationInformationDict[@"_location"] = @{@"_latitude": @(self.locationInformation.coordinate.latitude),
                                                 @"_longitude": @(self.locationInformation.coordinate.longitude)};

        jsonDict[@"_locationInformation"] = locationInformationDict;
    }

    if (self.partnerVenueId) {
        jsonDict[@"_partnerVenueId"] = self.partnerVenueId;
    }

    if (self.probability) {
        jsonDict[@"_probability"] = self.probability;
    }

    NSMutableArray *chainsArray = [NSMutableArray array];
    for (FSQPChain *chain in self.chains) {
        [chainsArray addObject:[chain json]];
    }
    jsonDict[@"_chains"] = chainsArray;

    jsonDict[@"_categories"] = [FSQPVenue categoriesArrayJson:self.categories];

    NSMutableArray *hierarchyArray = [NSMutableArray array];
    for (FSQPVenue *venueParent in self.hierarchy) {
        NSMutableDictionary *venueParentDict = [NSMutableDictionary dictionary];
        venueParentDict[@"_id"] = venueParent.foursquareID;
        venueParentDict[@"_name"] = venueParent.name;
        venueParentDict[@"_categories"] = [FSQPVenue categoriesArrayJson:venueParent.categories];
    }
    jsonDict[@"_hierarchy"] = hierarchyArray;

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
