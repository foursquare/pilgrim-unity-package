//
//  FSQPChain+JSON.m
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import "FSQPChain+JSON.h"

@implementation FSQPChain (JSON)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];
    jsonDict[@"_id"] = self.foursquareID;
    jsonDict[@"_name"] = self.name;
    return jsonDict;
}

@end
