//
//  FSQPGeofenceRegion.h
//  Pilgrim
//
//  Created by Brian Rojas on 5/10/18.
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>

@protocol FSQPPilgrimGeofenceArea;

NS_ASSUME_NONNULL_BEGIN

NS_SWIFT_NAME(GeofenceRegion)
@interface FSQPGeofenceRegion : NSObject <NSSecureCoding>

- (instancetype)init NS_UNAVAILABLE;

- (BOOL)hasPassedThreshold:(CLLocation *)location;

@property (nonatomic, readonly) CLLocationCoordinate2D center;

@property (nonatomic, readonly) CLLocationDistance radius;

@property (nonatomic, readonly) CLLocationDistance threshold;

@end

@interface FSQPGeofenceRegion (Swift)

- (instancetype)initFromModel:(id<FSQPPilgrimGeofenceArea>)model;

@end

NS_ASSUME_NONNULL_END
