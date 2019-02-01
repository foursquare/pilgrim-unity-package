//
//  FSQPCurrentLocation.h
//  Pilgrim
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>

@class FSQPGeofenceEvent;
@class FSQPVisit;

NS_ASSUME_NONNULL_BEGIN

/**
 * An object representing the current location of the user.
 */
NS_SWIFT_NAME(CurrentLocation)
@interface FSQPCurrentLocation : NSObject

/**
 * The visit object that is the current place the user is in.
 */
@property (nonatomic, readonly) FSQPVisit *currentPlace;

/**
 * An array of geofence objects the user is currently in, will be non-nil and empty if no matched geofences.
 */
@property (nonatomic, readonly) NSArray<FSQPGeofenceEvent *> *matchedGeofences;

/**
 * The geographic location of the user.
 */
@property (nonatomic, readonly) CLLocation *location;

/**
 * Unavailable.
 */
- (instancetype)init NS_UNAVAILABLE;

@end

NS_ASSUME_NONNULL_END
