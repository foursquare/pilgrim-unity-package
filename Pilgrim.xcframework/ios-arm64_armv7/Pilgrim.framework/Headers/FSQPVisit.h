//
//  FSQPVisit.h
//  PilgrimSDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>

NS_ASSUME_NONNULL_BEGIN

@class FSQPNearbyVenue;
@class FSQPUserSegment;
@class FSQPVenue;
@class FSQPVisit;

@protocol FSQPPilgrimVisit;

/**
 *  Everything Pilgrim knows about a user's location, including raw data and a probable venue.
 */
NS_SWIFT_NAME(Visit)
@interface FSQPVisit : NSObject <NSSecureCoding>

/**
 *  Defines a set of location types that Pilgrim can return.
 */
typedef NS_ENUM(NSInteger, FSQPLocationType) {
    /** Pilgrim was unable to determine the location type */
    FSQPLocationTypeUnknown = 0,
    /** The location is the user's home */
    FSQPLocationTypeHome,
    /** The location is the user's work */
    FSQPLocationTypeWork,
    /** The location is a venue visit */
    FSQPLocationTypeVenue,
} NS_SWIFT_NAME(Visit.LocationType);

/**
 *  Defines the confidence level of how accurate Pilgrim thinks the user is stopped at the given location.
 */
typedef NS_ENUM(NSInteger, FSQPConfidence) {
    /** Pilgrim is not confident at all */
    FSQPConfidenceNone = 0,
    /** Pilgrim is slightly confident */
    FSQPConfidenceLow,
    /** Pilgrim is somewhat confident */
    FSQPConfidenceMedium,
    /** Pilgrim is very confident */
    FSQPConfidenceHigh,
} NS_SWIFT_NAME(Visit.Confidence);

/**
 *  The date and time when the user arrived at this location. This will be nil if the arrival date isn't available.
 */
@property (nonatomic, copy, nullable, readonly) NSDate *arrivalDate;

/*
 *  The location of the arrival. This will be nil if arrival information isn't available.
 */
@property (nonatomic, nullable, readonly) CLLocation *arrivalLocation;

/*
 *  The date and time when the user departed this location. This will be nil if the user has not yet left.
 */
@property (nonatomic, copy, nullable, readonly) NSDate *departureDate;

/*
 *  The location of the departure (that is, the location where the user was no longer considered to be at the place). This will be nil if the user has not yet left.
 */
@property (nonatomic, nullable, readonly) CLLocation *departureLocation;

/**
 *  YES if the visit was triggered by a departure, NO if the visit is an arrival.
 */
@property (nonatomic, readonly) BOOL hasDeparted;

/**
 *  What type of location the user is at, i.e. home, work, or a Foursquare Venue.
 */
@property (nonatomic, readonly) FSQPLocationType locationType;

/**
 *  How certain we are of the user's location context on a relative scale.
 */
@property (nonatomic, readonly) FSQPConfidence confidence;

/**
 *  String representation of confidence.
 *  @note For debugging purposes only.
 */
@property (nonatomic, readonly) NSString *confidenceString;

+ (NSString *)confidenceStringForValue:(FSQPConfidence)confidence;

/**
 *  The most likely Foursquare venue associated with the location. Will be nil if the user is at home or work.
 */
@property (nonatomic, nullable, readonly) FSQPVenue *venue;

/**
 *  An array of other possible venues the user can be stopped at.
 */
@property (nonatomic, copy, nullable, readonly) NSArray<FSQPVenue *> *otherPossibleVenues;

/**
 *  For applications utilizing User Segments, returns a list of segment categories
 *  that categorize the user based on locations they've visited.
 */
@property (nonatomic, copy, nullable, readonly) NSArray<FSQPUserSegment *> *segments;

/**
 *  The display name for the visit.
 */
@property (nonatomic, readonly) NSString *displayName;

/**
 *  The pilgrim identifier for this visit.
 */
@property (nonatomic, copy, nullable, readonly) NSString *pilgrimVisitId;

- (instancetype)init NS_UNAVAILABLE;

@end

@interface FSQPVisit (Model)

- (instancetype)initFromModel:(id<FSQPPilgrimVisit>)model;

@end

NS_ASSUME_NONNULL_END
