//
//  CLLocation+JSON.m
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import "CLLocation+JSON.h"

@implementation CLLocation (JSON)

- (NSDictionary *)json {
    return @{@"_latitude": @(self.coordinate.latitude),
             @"_longitude": @(self.coordinate.longitude)};
}

@end
