//
//  FSQPLastKnownUserState.h
//  PilgrimSDK
//
//  Copyright © 2020 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>

NS_ASSUME_NONNULL_BEGIN

@class FSQPUserState;

@protocol FSQPPilgrimLastKnownUserState;

NS_SWIFT_NAME(LastKnownUserState)
@interface FSQPLastKnownUserState : NSObject

@property (nonatomic, readonly) CLLocationCoordinate2D coordinate;

@property (nonatomic, readonly, copy) NSDate *timestamp;

@property (nonatomic, readonly, copy) FSQPUserState *state;

/**
 *  Unavailable.
 */
- (instancetype)init NS_UNAVAILABLE;

/**
 *  Unavailable.
 */
+ (instancetype)new NS_UNAVAILABLE;

@end

@interface FSQPLastKnownUserState (Swift)

- (instancetype)initFromModel:(id<FSQPPilgrimLastKnownUserState>)model;

@end

NS_ASSUME_NONNULL_END
