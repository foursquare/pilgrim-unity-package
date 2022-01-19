//  Copyright © 2019 Foursquare. All rights reserved.

#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>

@class FSQPVenue;

@protocol FSQPPilgrimGeofence;

NS_ASSUME_NONNULL_BEGIN

typedef NS_ENUM(NSUInteger, FSQPGeofenceType) {
    FSQPGeofenceTypeVenue,
    FSQPGeofenceTypeLatLon,
    FSQPGeofenceTypePolygon,
} NS_SWIFT_NAME(Geofence.BoundaryType);

NS_SWIFT_NAME(Geofence)
@interface FSQPGeofence : NSObject <NSSecureCoding>

@property (nonatomic, copy, readonly) NSString *geofenceID;

@property (nullable, nonatomic, copy, readonly) NSArray<NSString *> *categoryIDs;

@property (nullable, nonatomic, copy, readonly) NSArray<NSString *> *chainIDs;

@property (nullable, nonatomic, copy, readonly) NSString *partnerVenueID;

@property (nonatomic, readonly) NSTimeInterval dwellTime;

@property (nullable, nonatomic, readonly) FSQPVenue *venue;

@property (nonatomic, readonly) FSQPGeofenceType type;

@property (nonatomic, copy, readonly) NSString *name;

@property (nullable, nonatomic, copy, readonly) NSDictionary<NSString *, NSString *> *properties;

- (instancetype)init NS_UNAVAILABLE;

@end

@interface FSQPGeofence (Swift)

- (instancetype)initFromModel:(id<FSQPPilgrimGeofence>)model;

@end

NS_ASSUME_NONNULL_END
