//
//  FSQPNearbyVenue.h
//  PilgrimSDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>

@class FSQPVenue;

NS_ASSUME_NONNULL_BEGIN

/**
 * An object representing a nearby venue used as a result of a venue visit event or a venue search action.
 *
 * @see FSQPVenue
 * @see FSQPVisit
 */
NS_SWIFT_NAME(NearbyVenue)
@interface FSQPNearbyVenue : NSObject <NSSecureCoding>

/**
 * The nearby FSQPVenue object.
 *
 * @see FSQPVenue
 */
@property (nonatomic, readonly) FSQPVenue *venue;

/**
 * An array of match types for the nearby venue.
 */
@property (nonatomic, readonly) NSArray<NSString *> *matchTypes;

/**
 * Initializes a new FSQPNearbyVenue object.
 *
 * @param venueDict A dictionary representation of the nearby venue.
 *
 * @return a new FSQPNearbyVenue object
 */
- (nullable instancetype)initWithNearbyVenueDict:(NSDictionary *)venueDict NS_DESIGNATED_INITIALIZER;
- (nullable instancetype)initWithCoder:(NSCoder *)aDecoder NS_DESIGNATED_INITIALIZER;

/**
 *  Unavailable. Use `initWithNearbyVenueDict:` instead.
 */
- (instancetype)init NS_UNAVAILABLE;

@end

NS_ASSUME_NONNULL_END
