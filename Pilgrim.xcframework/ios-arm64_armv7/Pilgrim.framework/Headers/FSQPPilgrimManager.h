//
//  FSQPPilgrimManager.h
//  PilgrimSDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <UIKit/UIKit.h>

#import "FSQPVisit.h"
#import "FSQPUserState.h"

NS_ASSUME_NONNULL_BEGIN

@class FSQPCurrentLocation;
@class FSQPDebugLog;
@class FSQPFeedbackProvider;
@class FSQPGeofenceEvent;
@class FSQPLastKnownUserState;
@class FSQPPilgrimManager;
@class FSQPUserInfo;
@class FSQPVisitTester;

NS_SWIFT_NAME(PilgrimManagerErrorDomain)
extern NSString * const FSQPPilgrimManagerErrorDomain;

typedef NS_ENUM(NSInteger, FSQPPilgrimManagerErrorCode) {
    FSQPPilgrimManagerErrorCodeUnknown,
    FSQPPilgrimManagerErrorCodeUnsupportedOS,
    FSQPPilgrimManagerErrorCodeInvalidParameters
} NS_SWIFT_NAME(PilgrimManagerErrorCode);

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
 *  Called when a user interacts with one or more geofences causing new geofence events.
 *  This method returns an array to account for the fact that a user could be interacting with multiple geofences simultaniously, causing multiple events to occur simultaniously.
 *
 *  @note Geofences are controlled in the Foursquare Developer Console.
 *
 *  @param geofenceEvents An array of geofence interaction events which contain information about the event along with the location where the event occured.
 *  @see FSQPGeofenceEvent
 */
- (void)pilgrimManager:(FSQPPilgrimManager *)pilgrimManager handleGeofenceEvents:(NSArray<FSQPGeofenceEvent *> *)geofenceEvents NS_SWIFT_NAME(pilgrimManager(_:handle:));

/**
 *  Called when a user state component changes.
 *  Updates of the user state may be limited to avoid too many network calls and save battery.
 *
 *  @note While visit and geofence events are unavailable when a user disables "precise location," the user state object and delegate method will still function as expected.
 *
 *  @param updatedUserState An updated user state containing detailed infromation about where a user is and what they're doing.
 *  @param changedComponents A bitmask containing the user state components that have changed since the user state was last updated.
 */
- (void)pilgrimManager:(FSQPPilgrimManager *)pilgrimManager handleUserState:(FSQPUserState *)updatedUserState changedComponents:(FSQPUserStateComponent)changedComponents NS_SWIFT_NAME(pilgrimManager(_:handleUserState:changedComponents:));

/**
 *
 */
- (void)pilgrimManager:(FSQPPilgrimManager *)pilgrimManager handleErrror:(NSError *)error;

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
 *  Enable to see live debug events in the developer console while in development.
 */
@property (nonatomic, getter=isLiveDebugEnabled) BOOL liveDebugEnabled;

/**
 *  Disable sharing of advertising identity data.
 */
@property (nonatomic, getter=shouldDisableAdIdentitySharing) BOOL disableAdIdentitySharing;

/**
 *  Version string for Pilgrim SDK.
 */
@property (nonatomic, readonly) NSString *pilgrimSDKVersionString;

/**
 *  The most recently retrieved user state. The value of this property is nil if no user state has ever been retrieved.
 *
 *  @note It is always a good idea to check the timestamp and coordinates of this property. It's possible that the
 *        user has moved since the last time the user state was updated.
 */
@property (nonatomic, readonly, copy, nullable) FSQPLastKnownUserState *lastKnownUserState;

/**
 *  A global shared instance of the Pilgrim SDK that should be used for configuring your consumer key and delegate.
 *
 *  @return The shared instance of the Pilgrim SDK
 */
+ (instancetype)sharedManager;

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
 *  Update your configuration to use a new consumer key and secret.
 *
 *  @note This method should not be required for most cases.
 *
 *  @param key          Your app's updated consumer key
 *  @param secret       Your app's updated consumer secret
 */
- (void)updateConfigurationWithConsumerKey:(NSString *)key
                                    secret:(NSString *)secret;

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
*  Gets an array of recent Debug logs and delivers them to the completion handler.
*
*  @note Completion will be given an empty array if `debugLogsEnabled` is NO.
*  @see  FSQPDebugLog
*/
- (void)debugLogsWithCompletion:(void (^)(NSArray<FSQPDebugLog *> *))completion;

/**
 *  Deletes debug logs from disk.
 */
- (void)deleteDebugLogs;

/**
 * Initializes a debug mode view controller for viewing PilgrimSDK logs and presents it.
 *
 * @param parentViewController the parent view controller presenting the debug view controller.
 */
- (void)presentDebugViewController:(UIViewController *)parentViewController NS_SWIFT_NAME(presentDebugViewController(parentViewController:)) __IOS_AVAILABLE(9.0);

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
@property (nonatomic, null_resettable) FSQPUserInfo *userInfo;

/**
 * For applications utilizing the server-to-server method for visit notifications,
 * you can use this to pass through your own identifier to the notification endpoint call.
 *
 * @param persisted Set to true to persist the user info data.
 */
- (void)setUserInfo:(FSQPUserInfo * _Nullable)userInfo persisted:(BOOL)persisted;

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
- (instancetype)init NS_UNAVAILABLE;

/**
 *  Unavailable. Use `sharedManager` instead.
 */
+ (instancetype)new NS_UNAVAILABLE;

@end

NS_ASSUME_NONNULL_END
