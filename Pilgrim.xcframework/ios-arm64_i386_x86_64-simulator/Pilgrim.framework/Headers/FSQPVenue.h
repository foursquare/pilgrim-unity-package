//
//  FSQPVenue.h
//  PilgrimSDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>

@class FSQPVenueLocation;
@class FSQPCategoryIcon;

@protocol FSQPPilgrimVenue;
@protocol FSQPPilgrimCategoryIcon;
@protocol FSQPPilgrimChain;
@protocol FSQPPilgrimCategory;

NS_ASSUME_NONNULL_BEGIN

/**
 *  Foursquare category for a venue.
 */
NS_SWIFT_NAME(Category)
@interface FSQPCategory : NSObject <NSSecureCoding>

/**
 *  Unique identifier of the category in the Foursquare API.
 */
@property (nonatomic, copy, readonly) NSString *foursquareID;

/**
 *  Displayable name of the category.
 */
@property (nonatomic, copy, readonly) NSString *name;

/**
 *  Displayable plural name of the category.
 */
@property (nonatomic, copy, nullable, readonly) NSString *pluralName;

/**
 *  Displayable short name of the category.
 */
@property (nonatomic, copy, nullable, readonly) NSString *shortName;

/**
 *  Icon information for representing the category.
 */
@property (nonatomic, nullable, readonly) FSQPCategoryIcon *icon;

/**
 *  YES if this is the primary category for the venue.
 */
@property (nonatomic, readonly) BOOL isPrimary;

/**
 *  Unavailable.
 */
+ (instancetype)init NS_UNAVAILABLE;

@end

/**
 *  The icon image information for a category.
 */
NS_SWIFT_NAME(CategoryIcon)
@interface FSQPCategoryIcon : NSObject <NSSecureCoding>

/**
 *  URL prefix for generating the icon.
 */
@property (nonatomic, copy, readonly) NSString *prefix;

/**
 *  URL suffix for generating the icon.
 */
@property (nonatomic, copy, readonly) NSString *suffix;

/**
 *  Unavailable.
 */
- (instancetype)init NS_UNAVAILABLE;

@end


/**
 *  Foursquare representation of a chain of venues, i.e. Starbucks.
 */
NS_SWIFT_NAME(Chain)
@interface FSQPChain : NSObject <NSSecureCoding>

/**
 *  Unique identifier of the chain in the Foursquare API.
 */
@property (nonatomic, copy, readonly) NSString *foursquareID;

/**
 *  Name of the chain from the Foursquare API.
 */
@property (nonatomic, copy, readonly) NSString *name;

/**
 *  Unavailable.
 */
- (instancetype)init NS_UNAVAILABLE;

@end

/**
 *  Representation of a venue in the Foursquare Places database.
 */
NS_SWIFT_NAME(Venue)
@interface FSQPVenue : NSObject <NSSecureCoding>

/**
 *  Unique identifier of the venue in the Foursquare API.
 */
@property (nonatomic, copy, readonly) NSString *foursquareID;

/**
 *  Displayable name of the venue.
 */
@property (nonatomic, copy, readonly) NSString *name;

/**
 * Probability that this venue is associated with the visit.
 * Values range from 0.0 - 1.0
 */
@property (nullable, nonatomic, copy, readonly) NSNumber *probability;

/**
 *  If you are a partner who uses venue harmonization this is venue id corresponding to your harmonized place.
 */
@property (nonatomic, copy, nullable, readonly) NSString *partnerVenueId;

/**
 *  Location information of the venue.
 */
@property (nonatomic, nullable, readonly) FSQPVenueLocation *locationInformation;

/**
 *  Any chain objects associated with the venue. Will be empty if no chain data exists for the venue.
 */
@property (nonatomic, copy, readonly) NSArray<FSQPChain *> *chains;

/**
 *  Foursquare categories associated with the venue. Every venue has exactly one primary category, and up to two
 *  secondary categories.
 */
@property (nonatomic, copy, readonly) NSArray<FSQPCategory *> *categories;

/**
 *  Returns the primary category of the venue.
 */
@property (nullable, nonatomic, readonly) FSQPCategory *primaryCategory;

/**
 *  Venue parents.
 */
@property (nonatomic, copy, readonly) NSArray<FSQPVenue *> *hierarchy;

/**
 *  Unavailable.
 */
- (instancetype)init NS_UNAVAILABLE;

@end

@interface FSQPVenue (Swift)

- (nullable instancetype)initFromModel:(nullable id<FSQPPilgrimVenue>)model;

@end

@interface FSQPCategoryIcon (Swift)

- (nullable instancetype)initFromModel:(nullable id<FSQPPilgrimCategoryIcon>)model;

@end

@interface FSQPChain (Swift)

- (instancetype)initFromModel:(id<FSQPPilgrimChain>)model;

@end

@interface FSQPCategory (Swift)

- (instancetype)initFromModel:(id<FSQPPilgrimCategory>)model;

@end

NS_ASSUME_NONNULL_END
