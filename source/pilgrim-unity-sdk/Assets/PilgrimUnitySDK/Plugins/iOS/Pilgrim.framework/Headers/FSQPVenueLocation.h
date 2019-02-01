//
//  FSQPVenueLocation.h
//  PilgrimSDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

/**
 *  Foursquare location object for a venue.
 */
NS_SWIFT_NAME(VenueLocation)
@interface FSQPVenueLocation : NSObject <NSSecureCoding>

/** The street address (e.g., 425 Bush Street) */
@property (nonatomic, copy, nullable, readonly) NSString *address;
/** The cross street (e.g., Kearney Street) */
@property (nonatomic, copy, nullable, readonly) NSString *crossStreet;
/** The city (e.g., San Francisco) */
@property (nonatomic, copy, nullable, readonly) NSString *city;
/** The state (e.g., California) */
@property (nonatomic, copy, nullable, readonly) NSString *state;
/** The postal code (e.g., 94108) */
@property (nonatomic, copy, nullable, readonly) NSString *postalCode;
/** The country (e.g., United States) */
@property (nonatomic, copy, nullable, readonly) NSString *country;

/**
 The lat/long coordinates in a `CLLocationCoordinate2D`
 @see CLLocationCoordinate2D
 */
@property (nonatomic, readonly) CLLocationCoordinate2D coordinate;

/** Initializes a new FSQPVenueLocation object
 *
 * @param dict A dictionary representation of the venue location
 *
 * @return a new FSQPVenueLocation object
 */
- (instancetype)initWithVenueLocationDictionary:(NSDictionary *)dict NS_DESIGNATED_INITIALIZER;
/**
 *  Unavailable. Use `initWithVenueLocationDictionary:` instead.
 */
- (instancetype)init NS_UNAVAILABLE;

@end

NS_ASSUME_NONNULL_END
