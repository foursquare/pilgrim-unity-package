//
//  FSQPDebugLog.h
//  PilgrimSDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

/**
 *  An individual log statement
 */
NS_SWIFT_NAME(DebugLog)
@interface FSQPDebugLog : NSObject <NSCoding>

/**
 *  Defines a set of standard logging levels that can be used to control logging output.
 */
typedef NS_ENUM(NSInteger, FSQPLogLevel) {
    /** Verbose development messages */
    FSQPLogLevelDebug = 0,
    /** Verbose informational messages */
    FSQPLogLevelInfo,
    /** Messages indicating a potential problem */
    FSQPLogLevelWarn,
    /** Messages indicating an internal sdk problem */
    FSQPLogLevelError
} NS_SWIFT_NAME(DebugLog.Level);

/**
 *  Defines a set of log types to determine the source of the log statement.
 */
typedef NS_ENUM(NSInteger, FSQPLogType) {
    /** Non-specific log messages */
    FSQPLogTypeGeneral = 0,
    /** Battery log messages */
    FSQPLogTypeBattery,
    /** Location log messages */
    FSQPLogTypeLocation,
    /** Networking log messages */
    FSQPLogTypeNetwork,
    /** Persistant data storage log messages */
    FSQPLogTypePersistence,
    /** Pilgrim engine stop detection log messages */
    FSQPLogTypeStopDetection,
    /** Internal assertion log messages */
    FSQPLogTypeAssertion,
    /** Geofence detection log messages */
    FSQPLogTypeGeofence
} NS_SWIFT_NAME(DebugLog.Type);

/**
 *  Time that the event was recorded.
 */
@property (nonatomic, copy) NSDate *timestamp;

/**
 *  Set logging level that can be used to control logging output.
 */
@property (nonatomic) FSQPLogLevel level;
/**
 *  The type of log message and source from the SDK.
 */
@property (nonatomic) FSQPLogType type;

/**
 *  Associated data for detailed debugging.
 */
@property (nonatomic, copy, nullable) NSObject<NSCoding> *data;

/**
 *  A short string describing the event.
 */
@property (nonatomic, copy) NSString *eventDescription;

/**
 *  Unavailable; log messages are generated internally.
 */
- (instancetype)init NS_UNAVAILABLE;

@end

NS_ASSUME_NONNULL_END
