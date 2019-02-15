//
//  FSQPCategoryIcon+JSON.m
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import "FSQPCategoryIcon+JSON.h"

@implementation FSQPCategoryIcon (JSON)

- (NSDictionary *)json {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];
    jsonDict[@"_prefix"] = self.prefix;
    jsonDict[@"_suffix"] = self.suffix;
    return jsonDict;
}

@end
