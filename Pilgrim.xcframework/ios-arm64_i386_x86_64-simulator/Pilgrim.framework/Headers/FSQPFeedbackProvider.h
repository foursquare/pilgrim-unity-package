//
//  FSQPFeedbackProvider.h
//  PilgrimSDK
//
//  Created by Mitchell Livingston on 3/2/16.
//  Copyright © 2016 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

/**
 *  FSQPFeedbackProvider class contains methods for providing feedback on pilgrim visits and venues.
 *
 *  ## Notes
 *
 *  You should not initialize your own instance of this class but instead access the shared
 *  feedback provider on FSQPPilgrimManager.
 *
 *  @see FSQPPilgrimManager
 */
NS_SWIFT_NAME(FeedbackProvider)
@interface FSQPFeedbackProvider : NSObject

/**
 * Defines a list of feedback types for a given pilgrim visit to help improve accuracy.
 */
typedef NS_ENUM(NSInteger, FSQPVisitFeedback) {
    /** Confirm the visit was correct */
    FSQPVisitFeedbackConfirm,
    /** Mark the visit as a false stop */
    FSQPVisitFeedbackFalseStop,
    /** Confirm the stop was correct but the venue was incorrect */
    FSQPVisitFeedbackWrongVenue,
    /** Deny and remove the visit */
    FSQPVisitFeedbackDeny,
} NS_SWIFT_NAME(FeedbackProvider.VisitFeedback);

- (instancetype)init NS_UNAVAILABLE;

/**
 *  Provide feedback on a previous visit.
 *
 *  @param feedback         See FSQPVisitFeedback for options.
 *  @param visitId          The Foursquare venue ID to provide feedback for.
 *  @param actualVenueId    If the correct venue ID is known, this will inform pilgrim.
 *  @param completion       A completion handler called when the transaction finishes.
 */
- (void)leaveVisitFeedback:(FSQPVisitFeedback)feedback
                   visitId:(NSString *)visitId
             actualVenueId:(nullable NSString *)actualVenueId
                completion:(nullable void (^)(NSError * _Nullable error))completion NS_SWIFT_NAME(leaveVisitFeedback(_:visitId:actualVenueId:completion:));

/**
 *  Inform Pilgrim that the device is known to be at a specific venue. Used to improve our
 *  ability to detect visits at the Foursquare venue ID provided.
 *
 *  @param foursquareVenueId    The foursquare venue ID where you believe the device is currently at.
 */
- (void)checkInWithVenueId:(NSString *)foursquareVenueId NS_SWIFT_NAME(checkIn(venueId:));

/**
 *  Inform Pilgrim that the device is known to be at a specific venue. Used to improve our
 *  ability to detect visits at the partner venue ID provided. In order to use this, make sure
 *  your venue IDs are harmonized with Foursquare.
 *
 *  @param partnerVenueId   A partner venue ID where you believe the device is currently at.
 */
- (void)checkInWithPartnerVenueId:(NSString *)partnerVenueId NS_SWIFT_NAME(checkIn(partnerVenueId:));

@end

NS_ASSUME_NONNULL_END
