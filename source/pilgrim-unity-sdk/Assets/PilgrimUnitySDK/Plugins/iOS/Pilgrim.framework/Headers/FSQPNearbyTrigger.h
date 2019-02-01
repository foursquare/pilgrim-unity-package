//
//  FSQPNearbyTrigger.h
//  PilgrimSDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>

/**
 * An object representing an nearby venue event being triggered
 */
NS_SWIFT_NAME(NearbyTrigger)
@interface FSQPNearbyTrigger : NSObject

/**
 *  Defines a set of nearby trigger types
 */
typedef NS_ENUM(NSInteger, FSQPNearbyTriggerType) {
    /** Foursquare ID for the visit's venue */
    FSQPNearbyTriggerVenue = 0,
    /** The visit's venue category (or a venue in its hierarchy's category); has a matching Foursquare ID */
    FSQPNearbyTriggerCategory = 1,
    /** Foursquare ID for the visit's venue chain matches */
    FSQPNearbyTriggerChain = 2
} NS_SWIFT_NAME(NearbyTrigger.Type);

/**
 * The radius size of the event trigger
 */
@property (nonatomic, readonly) NSInteger radius;
/**
 * The type of the NearbyTrigger.
 */
@property (nonatomic, readonly) FSQPNearbyTriggerType triggerType;
/**
 * The identifier string for the venue that was triggered
 */
@property (nonatomic, readonly) NSString *venueId;

/**
 * Initializes a new FSQPNearbyTrigger object
 *
 * @param triggerType A trigger type
 * @param venueId     A Foursquare venue identifier string
 *
 * @return a new FSQPNearbyTrigger object
 */
- (instancetype)initWithTriggerType:(FSQPNearbyTriggerType)triggerType
                            venueId:(NSString *)venueId;
/**
 * Initializes a new FSQPNearbyTrigger object
 *
 * @param triggerType       A trigger type
 * @param venueId           A Foursquare venue identifier string
 * @param triggerRadius     The radius size of the event trigger
 *
 * @return a new FSQPNearbyTrigger object
 */
- (instancetype)initWithTriggerType:(FSQPNearbyTriggerType)triggerType
                            venueId:(NSString *)venueId
                             radius:(NSInteger)triggerRadius;
/**
 *  Unavailable. Use `initWithTriggerType:` instead
 */
- (instancetype)init NS_UNAVAILABLE;

@end
