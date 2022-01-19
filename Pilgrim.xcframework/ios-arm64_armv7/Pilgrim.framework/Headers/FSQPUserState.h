//
//  FSQPUserState.h
//  Pilgrim
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>

@protocol FSQPPilgrimUserState;

NS_ASSUME_NONNULL_BEGIN

typedef NS_OPTIONS(NSUInteger, FSQPUserStateComponent) {
    FSQPUserStateComponentTraveling = 1 << 0,
    FSQPUserStateComponentCommuting = 1 << 1,
    FSQPUserStateComponentState = 1 << 2,
    FSQPUserStateComponentCity = 1 << 3,
    FSQPUserStateComponentPostalCode = 1 << 4,
    FSQPUserStateComponentCountry = 1 << 5,
    FSQPUserStateComponentCounty = 1 << 6,
    FSQPUserStateComponentDma = 1 << 7
} NS_SWIFT_NAME(UserStateComponent);

/**
 * User States will be used to keep track of different states of locations that people can be in i.e. travel, sick
 */
NS_SWIFT_NAME(UserState)
@interface FSQPUserState : NSObject <NSSecureCoding>

/**
 * Whether or not the user is currently traveling
 */
@property (nonatomic, readonly) BOOL traveling;

/**
 * Whether or not the user is currently commuting
 */
@property (nonatomic, readonly) BOOL commuting;

/**
 *  The state the user is currently in.
 */
@property (nonatomic, nullable, copy, readonly) NSString *state;

/**
 *  The city the user is currently in.
 */
@property (nonatomic, nullable, copy, readonly) NSString *city;

/**
 *  The postal code the user is currently in.
 */
@property (nonatomic, nullable, copy, readonly) NSString *postalCode;

/**
 *  The country the user is currently in.
 */
@property (nonatomic, nullable, copy, readonly) NSString *country;

/**
 *  The country the user is currently in.
 */
@property (nonatomic, nullable, copy, readonly) NSString *county;

/**
 *  The dma the user is currently in.
 */
@property (nonatomic, nullable, copy, readonly) NSString *dma;

/**
 *  Unavailable.
 */
- (instancetype)init NS_UNAVAILABLE;

/**
 *  Unavailable.
 */
+ (instancetype)new NS_UNAVAILABLE;


@end

@interface FSQPUserState (Swift)

- (instancetype)initFromModel:(id<FSQPPilgrimUserState>)model;

@end

NS_ASSUME_NONNULL_END
