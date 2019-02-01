//
//  FSQPUserSegment.h
//  PilgrimSDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

/**
 * For applications utilizing User Segments, returns a list of segment categories
 * that categorize the user based on locations they've visited.
 */
NS_SWIFT_NAME(UserSegment)
@interface FSQPUserSegment : NSObject <NSSecureCoding>

/**
 * The identifier for the segment (represented as a integer).
 */
@property (nonatomic, readonly) NSInteger segmentId;
/**
 * The name for the segment.
 */
@property (nonatomic, readonly, copy) NSString *name;

/**
 *  Unavailable.
 */
- (instancetype)init NS_UNAVAILABLE;
/**
 *  Unavailable.
 */
+ (instancetype)new NS_UNAVAILABLE;

@end

NS_ASSUME_NONNULL_END
