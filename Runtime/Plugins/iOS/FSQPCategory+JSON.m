//
//  FSQPCategory+JSON.m
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import "FSQPCategory+JSON.h"

#import "FSQPCategoryIcon+JSON.h"

@implementation FSQPCategory (JSON)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];
    jsonDict[@"_id"] = self.foursquareID;
    jsonDict[@"_name"] = self.name;

    if (self.pluralName) {
        jsonDict[@"_pluralName"] = self.pluralName;
    }

    if (self.shortName) {
        jsonDict[@"_shortName"] = self.shortName;
    }

    if (self.icon) {
        jsonDict[@"_icon"] = [self.icon json];
    }

    jsonDict[@"_isPrimary"] = @(self.isPrimary);

    return jsonDict;
}

@end
