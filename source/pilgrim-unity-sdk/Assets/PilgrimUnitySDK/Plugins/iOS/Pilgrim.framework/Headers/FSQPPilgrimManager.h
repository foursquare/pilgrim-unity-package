//
//  FSQPPilgrimManager.h
//  PilgrimSDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <UIKit/UIKit.h>

#import "FSQPVisit.h"

NS_ASSUME_NONNULL_BEGIN

@class FSQPCurrentLocation;
@class FSQPDebugLog;
@class FSQPFeedbackProvider;
@class FSQPGeofenceEvent;
@class FSQPNearbyTrigger;
@class FSQPPilgrimManager;
@class FSQPUserInfo;
@class FSQPVisitTester;

/**
 *  Implement this protocol on an object that you will retain for as long as you want to receive location context
 *  updates.
 */
NS_SWIFT_NAME(PilgrimManagerDelegate)
@protocol FSQPPilgrimManagerDelegate <NSObject>

/**
 *  Called when the user has arrived at or departed from a location.
 *  This will only be called when all trigger requirements have been met. Those can be configured remotely.
 *
 *  @note Make sure you have correctly configured the PilgrimSDK and called startMonitoringVisits.
 *
 *  @param visit Detailed contextual information and raw data associated with the location.
 */
- (void)pilgrimManager:(FSQPPilgrimManager *)pilgrimManager handleVisit:(FSQPVisit *)visit;

@optional

/**
 *  Called when a visit has been backfilled with contextual information.
 *  This will only be called when all trigger requirements have been met. Those can be configured remotely.
 *
 *  @note Make sure you have correctly configured the PilgrimSDK and called startMonitoringVisits.
 *
 *  @param visit Detailed contextual information and raw data associated with the location.
 */
- (void)pilgrimManager:(FSQPPilgrimManager *)pilgrimManager handleBackfillVisit:(FSQPVisit *)visit NS_SWIFT_NAME(pilgrimManager(_:handleBackfill:));

/**
 *  Called when nearby venues have been detected.
 *  This will only be called on arrivals when nearby trigger requirements have been met. Those can be configured remotely.
 *
 *  @note Make sure you have correctly configured the PilgrimSDK and called startMonitoringVisits.
 *
 *  @param nearbyVenues Array of detailed contextual information for the nearby venues.
 */
- (void)pilgrimManager:(FSQPPilgrimManager *)pilgrimManager handleNearbyVenues:(NSArray<FSQPNearbyVenue *> *)nearbyVenues;

/**
 *
 */
- (void)pilgrimManager:(FSQPPilgrimManager *)pilgrimManager handleGeofenceEvents:(NSArray<FSQPGeofenceEvent *> *)geofenceEvents NS_SWIFT_NAME(pilgrimManager(_:handle:));

@end

/**
 *  Class for configuring the Pilgrim SDK in your app to get background location updates with context.
 */
NS_SWIFT_NAME(PilgrimManager)
@interface FSQPPilgrimManager : NSObject

/**
 *  A delegate object that you control that receives updates when new locations are visited. You may change this
 *  at any time. If you set this to nil, the location manager will continue running, but you will not receive updates
 *  until you set it again.
 */
@property (nonatomic, weak, nullable) id<FSQPPilgrimManagerDelegate> delegate;

/**
 *  Saves detailed logs of activity for debugging purposes.
 */
@property (nonatomic, getter=isDebugLogsEnabled) BOOL debugLogsEnabled;

/**
 *  A global shared instance of the Pilgrim SDK that should be used for configuring your consumer key and delegate.
 *
 *  @return The shared instance of the Pilgrim SDK
 */
+ (instancetype)sharedManager;

/**
 *  If the current device is supported (no iPads or iPod touches; cellular network required)
 */
- (BOOL)isSupportedDevice;

/**
 *  If the user is on a supported device and all the required settings ("always" location permission) are on
 */
- (BOOL)canEnable;

/**
 *  A convenience method for requesting iOS background authorization from the user.
 */
- (void)requestAlwaysAuthorizationWithCompletion:(nullable void (^)(BOOL didAuthorize))completion DEPRECATED_MSG_ATTRIBUTE("This method will be deprecated in future versions of Pilgrim. Please call CLLocationManager.requestAlwaysAuthorization over using this method");

/**
 *  Configure the Pilgrim SDK with your consumer key and secret, and add a delegate for subscribing to location updates.
 *  This method is safe to call more than once, but consumer key and secret must remain the same.
 *
 *  @note You must maintain a strong reference to your delegate object. If at any time your delegate becomes nil,
 *        you will stop receiving location updates.
 *
 *  @param key          Your app's consumer key
 *  @param secret       Your app's consumer secret
 *  @param delegate     (optional) A delegate for receiving location updates
 *  @param completion   (optional) A completion handler to be called after configuration is done, for example
 *                      to call startMonitoringVisits.
 */
- (void)configureWithConsumerKey:(NSString *)key
                          secret:(NSString *)secret
                        delegate:(nullable id<FSQPPilgrimManagerDelegate>)delegate
                      completion:(nullable void (^)(BOOL didSucceed, NSError * _Nullable error))completion;

/**
 *  Call this after configuring the SDK to start the SDK and begin receiving location updates.
 */
- (void)start;

/**
 *  Stop receiving location updates, until you call `start` again.
 */
- (void)stop;

/**
 *  Remove all visit history, including resetting the home/work model.
 *
 *  @note You cannot call this when monitoring visits. Call stopMonitoringVisits first.
 *
 *  @param completion (optional) A completion handler to be called when finished.
 */
- (void)clearAllData:(nullable void (^)(void))completion NS_SWIFT_NAME(clearAllData(completion:));

/**
 *  @return An array of recent Debug logs, maximum 250.
 *
 *  @note Returns an empty array if `debugLogsEnabled` is NO.
 *  @see  FSQPDebugLog
 */
- (NSArray<FSQPDebugLog *> *)debugLogs;

/**
 *  Deletes debug logs from disk.
 */
- (void)deleteDebugLogs;

/**
 * Initializes a debug mode view controller for viewing PilgrimSDK logs and presents it.
 *
 * @param parentViewController the parent view controller presenting the debug view controller.
 */
- (void)presentDebugViewController:(UIViewController *)parentViewController NS_SWIFT_NAME(presentDebugViewController(parentViewController:));

/**
 *  Gets the current location of the user.
 *  This includes possibly a visit and and an array of geofences.
 *
 *  @param completion A completion handler to be called when finished.
 */
- (void)getCurrentLocationWithCompletion:(void (^)(FSQPCurrentLocation * _Nullable currentLocation, NSError * _Nullable error))completion;

/**
 *  Helper for testing visit.
 *  Asserts if no delegate is set.
 */
@property (nonatomic, nullable, readonly) FSQPVisitTester *visitTester;

/**
 * Use to provide feedback on visits for Foursquare to use to improve.
 * This will be non-null once the location manager is configured.
 */
@property (nonatomic, nullable, readonly) FSQPFeedbackProvider *feedbackProvider;

/**
 * For applications utilizing the server-to-server method for visit notifications,
 * you can use this to pass through your own identifier to the notification endpoint call.
 */
@property (nonatomic, readonly) FSQPUserInfo *userInfo;

/**
 * The descriptions of any global triggers that have defined in your app's console.
 * Helpful for determing why a visit would/would not trigger.
 */
@property (nonatomic, readonly) NSArray<NSString *> *triggerDescriptions;

/**
 * For applications utilizing Nearby notifications, you can use this property to add user specific Nearby
 * notifications on top of any global triggers you have defined in your console.
 */
@property (nonatomic, copy, nullable) NSArray<FSQPNearbyTrigger *> *nearbyTriggers;

/**
 * For applications with logged in Foursquare users pass the users oauth token to pilgrim.
 */
@property (nonatomic, copy, nullable) NSString *oauthToken;

/**
 * Returns a unique identifier that gets generated the first time this sdk runs on a specific device.
 */
@property (nonatomic, readonly, nullable) NSString *installId;

/**
 *  It can take several days to make a high confidence estimate of the user's home and work locations.
 *
 *  @return YES if we have identified either a high confidence home or work venue.
 */
- (BOOL)hasHomeOrWork;

/**
 *  Returns home locations.
 *  This returns an array of NSValue objects wrapping CLLocationCoordinate2D values (use property MKCoordinateValue).
 *
 *  @return Any home locations that exist.
 */
@property (nonatomic, copy, readonly) NSArray<NSValue *> *homeLocations;

/**
 *  Returns work locations.
 *  This returns an array of NSValue objects wrapping CLLocationCoordinate2D values (use property MKCoordinateValue).
 *
 *  @return Any work locations that exist.
 */
@property (nonatomic, copy, readonly) NSArray<NSValue *> *workLocations;

/**
 *  Unavailable. Use `sharedManager` instead.
 */
+ (instancetype)init NS_UNAVAILABLE;

@end

NS_ASSUME_NONNULL_END
