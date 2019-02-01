//
//  FSQPVisitTester.h
//  PilgrimSDK
//
//  Created by Kyle Fowler on 09/18/18.
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "FSQPVisit.h"

NS_ASSUME_NONNULL_BEGIN

/**
 *  Class for sending test visits through to the app delegate.
 */
NS_SWIFT_NAME(VisitTester)
@interface FSQPVisitTester : NSObject

- (instancetype)init NS_UNAVAILABLE;

/**
 *  Simulates a visit. For testing purposes.
 *  Asserts if no delegate is set.
 */
- (void)fireTestVisit:(FSQPConfidence)confidence locationType:(FSQPLocationType)type isDeparture:(BOOL)isDeparture
NS_SWIFT_NAME(fireTestVisit(confidence:type:departure:));

/**
 * Generates a visit and optional nearby venues at the given location.
 */
- (void)fireTestVisit:(CLLocation *)location
NS_SWIFT_NAME(fireTestVisit(location:));

/**
 * Simulates a departure delegate call for a given visit and dwell time
 */
- (void)fireTestDeparture:(FSQPVisit *)visit dwellTime:(NSTimeInterval)dwellTime
NS_SWIFT_NAME(fireTestDeparture(visit:dwellTime:));

@end

NS_ASSUME_NONNULL_END
