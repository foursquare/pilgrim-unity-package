#if 0
#elif defined(__arm64__) && __arm64__
// Generated by Apple Swift version 5.4.2 (swiftlang-1205.0.28.2 clang-1205.0.19.57)
#ifndef PILGRIM_SWIFT_H
#define PILGRIM_SWIFT_H
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Wgcc-compat"

#if !defined(__has_include)
# define __has_include(x) 0
#endif
#if !defined(__has_attribute)
# define __has_attribute(x) 0
#endif
#if !defined(__has_feature)
# define __has_feature(x) 0
#endif
#if !defined(__has_warning)
# define __has_warning(x) 0
#endif

#if __has_include(<swift/objc-prologue.h>)
# include <swift/objc-prologue.h>
#endif

#pragma clang diagnostic ignored "-Wauto-import"
#include <Foundation/Foundation.h>
#include <stdint.h>
#include <stddef.h>
#include <stdbool.h>

#if !defined(SWIFT_TYPEDEFS)
# define SWIFT_TYPEDEFS 1
# if __has_include(<uchar.h>)
#  include <uchar.h>
# elif !defined(__cplusplus)
typedef uint_least16_t char16_t;
typedef uint_least32_t char32_t;
# endif
typedef float swift_float2  __attribute__((__ext_vector_type__(2)));
typedef float swift_float3  __attribute__((__ext_vector_type__(3)));
typedef float swift_float4  __attribute__((__ext_vector_type__(4)));
typedef double swift_double2  __attribute__((__ext_vector_type__(2)));
typedef double swift_double3  __attribute__((__ext_vector_type__(3)));
typedef double swift_double4  __attribute__((__ext_vector_type__(4)));
typedef int swift_int2  __attribute__((__ext_vector_type__(2)));
typedef int swift_int3  __attribute__((__ext_vector_type__(3)));
typedef int swift_int4  __attribute__((__ext_vector_type__(4)));
typedef unsigned int swift_uint2  __attribute__((__ext_vector_type__(2)));
typedef unsigned int swift_uint3  __attribute__((__ext_vector_type__(3)));
typedef unsigned int swift_uint4  __attribute__((__ext_vector_type__(4)));
#endif

#if !defined(SWIFT_PASTE)
# define SWIFT_PASTE_HELPER(x, y) x##y
# define SWIFT_PASTE(x, y) SWIFT_PASTE_HELPER(x, y)
#endif
#if !defined(SWIFT_METATYPE)
# define SWIFT_METATYPE(X) Class
#endif
#if !defined(SWIFT_CLASS_PROPERTY)
# if __has_feature(objc_class_property)
#  define SWIFT_CLASS_PROPERTY(...) __VA_ARGS__
# else
#  define SWIFT_CLASS_PROPERTY(...)
# endif
#endif

#if __has_attribute(objc_runtime_name)
# define SWIFT_RUNTIME_NAME(X) __attribute__((objc_runtime_name(X)))
#else
# define SWIFT_RUNTIME_NAME(X)
#endif
#if __has_attribute(swift_name)
# define SWIFT_COMPILE_NAME(X) __attribute__((swift_name(X)))
#else
# define SWIFT_COMPILE_NAME(X)
#endif
#if __has_attribute(objc_method_family)
# define SWIFT_METHOD_FAMILY(X) __attribute__((objc_method_family(X)))
#else
# define SWIFT_METHOD_FAMILY(X)
#endif
#if __has_attribute(noescape)
# define SWIFT_NOESCAPE __attribute__((noescape))
#else
# define SWIFT_NOESCAPE
#endif
#if __has_attribute(ns_consumed)
# define SWIFT_RELEASES_ARGUMENT __attribute__((ns_consumed))
#else
# define SWIFT_RELEASES_ARGUMENT
#endif
#if __has_attribute(warn_unused_result)
# define SWIFT_WARN_UNUSED_RESULT __attribute__((warn_unused_result))
#else
# define SWIFT_WARN_UNUSED_RESULT
#endif
#if __has_attribute(noreturn)
# define SWIFT_NORETURN __attribute__((noreturn))
#else
# define SWIFT_NORETURN
#endif
#if !defined(SWIFT_CLASS_EXTRA)
# define SWIFT_CLASS_EXTRA
#endif
#if !defined(SWIFT_PROTOCOL_EXTRA)
# define SWIFT_PROTOCOL_EXTRA
#endif
#if !defined(SWIFT_ENUM_EXTRA)
# define SWIFT_ENUM_EXTRA
#endif
#if !defined(SWIFT_CLASS)
# if __has_attribute(objc_subclassing_restricted)
#  define SWIFT_CLASS(SWIFT_NAME) SWIFT_RUNTIME_NAME(SWIFT_NAME) __attribute__((objc_subclassing_restricted)) SWIFT_CLASS_EXTRA
#  define SWIFT_CLASS_NAMED(SWIFT_NAME) __attribute__((objc_subclassing_restricted)) SWIFT_COMPILE_NAME(SWIFT_NAME) SWIFT_CLASS_EXTRA
# else
#  define SWIFT_CLASS(SWIFT_NAME) SWIFT_RUNTIME_NAME(SWIFT_NAME) SWIFT_CLASS_EXTRA
#  define SWIFT_CLASS_NAMED(SWIFT_NAME) SWIFT_COMPILE_NAME(SWIFT_NAME) SWIFT_CLASS_EXTRA
# endif
#endif
#if !defined(SWIFT_RESILIENT_CLASS)
# if __has_attribute(objc_class_stub)
#  define SWIFT_RESILIENT_CLASS(SWIFT_NAME) SWIFT_CLASS(SWIFT_NAME) __attribute__((objc_class_stub))
#  define SWIFT_RESILIENT_CLASS_NAMED(SWIFT_NAME) __attribute__((objc_class_stub)) SWIFT_CLASS_NAMED(SWIFT_NAME)
# else
#  define SWIFT_RESILIENT_CLASS(SWIFT_NAME) SWIFT_CLASS(SWIFT_NAME)
#  define SWIFT_RESILIENT_CLASS_NAMED(SWIFT_NAME) SWIFT_CLASS_NAMED(SWIFT_NAME)
# endif
#endif

#if !defined(SWIFT_PROTOCOL)
# define SWIFT_PROTOCOL(SWIFT_NAME) SWIFT_RUNTIME_NAME(SWIFT_NAME) SWIFT_PROTOCOL_EXTRA
# define SWIFT_PROTOCOL_NAMED(SWIFT_NAME) SWIFT_COMPILE_NAME(SWIFT_NAME) SWIFT_PROTOCOL_EXTRA
#endif

#if !defined(SWIFT_EXTENSION)
# define SWIFT_EXTENSION(M) SWIFT_PASTE(M##_Swift_, __LINE__)
#endif

#if !defined(OBJC_DESIGNATED_INITIALIZER)
# if __has_attribute(objc_designated_initializer)
#  define OBJC_DESIGNATED_INITIALIZER __attribute__((objc_designated_initializer))
# else
#  define OBJC_DESIGNATED_INITIALIZER
# endif
#endif
#if !defined(SWIFT_ENUM_ATTR)
# if defined(__has_attribute) && __has_attribute(enum_extensibility)
#  define SWIFT_ENUM_ATTR(_extensibility) __attribute__((enum_extensibility(_extensibility)))
# else
#  define SWIFT_ENUM_ATTR(_extensibility)
# endif
#endif
#if !defined(SWIFT_ENUM)
# define SWIFT_ENUM(_type, _name, _extensibility) enum _name : _type _name; enum SWIFT_ENUM_ATTR(_extensibility) SWIFT_ENUM_EXTRA _name : _type
# if __has_feature(generalized_swift_name)
#  define SWIFT_ENUM_NAMED(_type, _name, SWIFT_NAME, _extensibility) enum _name : _type _name SWIFT_COMPILE_NAME(SWIFT_NAME); enum SWIFT_COMPILE_NAME(SWIFT_NAME) SWIFT_ENUM_ATTR(_extensibility) SWIFT_ENUM_EXTRA _name : _type
# else
#  define SWIFT_ENUM_NAMED(_type, _name, SWIFT_NAME, _extensibility) SWIFT_ENUM(_type, _name, _extensibility)
# endif
#endif
#if !defined(SWIFT_UNAVAILABLE)
# define SWIFT_UNAVAILABLE __attribute__((unavailable))
#endif
#if !defined(SWIFT_UNAVAILABLE_MSG)
# define SWIFT_UNAVAILABLE_MSG(msg) __attribute__((unavailable(msg)))
#endif
#if !defined(SWIFT_AVAILABILITY)
# define SWIFT_AVAILABILITY(plat, ...) __attribute__((availability(plat, __VA_ARGS__)))
#endif
#if !defined(SWIFT_WEAK_IMPORT)
# define SWIFT_WEAK_IMPORT __attribute__((weak_import))
#endif
#if !defined(SWIFT_DEPRECATED)
# define SWIFT_DEPRECATED __attribute__((deprecated))
#endif
#if !defined(SWIFT_DEPRECATED_MSG)
# define SWIFT_DEPRECATED_MSG(...) __attribute__((deprecated(__VA_ARGS__)))
#endif
#if __has_feature(attribute_diagnose_if_objc)
# define SWIFT_DEPRECATED_OBJC(Msg) __attribute__((diagnose_if(1, Msg, "warning")))
#else
# define SWIFT_DEPRECATED_OBJC(Msg) SWIFT_DEPRECATED_MSG(Msg)
#endif
#if !defined(IBSegueAction)
# define IBSegueAction
#endif
#if __has_feature(modules)
#if __has_warning("-Watimport-in-framework-header")
#pragma clang diagnostic ignored "-Watimport-in-framework-header"
#endif
@import CoreLocation;
@import Foundation;
@import ObjectiveC;
#endif

#import <Pilgrim/Pilgrim.h>

#pragma clang diagnostic ignored "-Wproperty-attribute-mismatch"
#pragma clang diagnostic ignored "-Wduplicate-method-arg"
#if __has_warning("-Wpragma-clang-attribute")
# pragma clang diagnostic ignored "-Wpragma-clang-attribute"
#endif
#pragma clang diagnostic ignored "-Wunknown-pragmas"
#pragma clang diagnostic ignored "-Wnullability"

#if __has_attribute(external_source_symbol)
# pragma push_macro("any")
# undef any
# pragma clang attribute push(__attribute__((external_source_symbol(language="Swift", defined_in="Pilgrim",generated_declaration))), apply_to=any(function,enum,objc_interface,objc_category,objc_protocol))
# pragma pop_macro("any")
#endif





@class NSString;
@protocol FSQPPilgrimCategoryIcon;
@class NSNumber;

SWIFT_PROTOCOL("_TtP7Pilgrim19FSQPPilgrimCategory_")
@protocol FSQPPilgrimCategory
@property (nonatomic, readonly, copy) NSString * _Nonnull id;
@property (nonatomic, readonly, copy) NSString * _Nonnull name;
@property (nonatomic, readonly, copy) NSString * _Nullable pluralName;
@property (nonatomic, readonly, copy) NSString * _Nullable shortName;
@property (nonatomic, readonly, strong) id <FSQPPilgrimCategoryIcon> _Nullable icon;
@property (nonatomic, readonly) BOOL primary;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim23FSQPPilgrimCategoryIcon_")
@protocol FSQPPilgrimCategoryIcon
@property (nonatomic, readonly, copy) NSString * _Nonnull prefix;
@property (nonatomic, readonly, copy) NSString * _Nonnull suffix;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim16FSQPPilgrimChain_")
@protocol FSQPPilgrimChain
@property (nonatomic, readonly, copy) NSString * _Nonnull id;
@property (nonatomic, readonly, copy) NSString * _Nonnull name;
@end

@protocol FSQPPilgrimVisit;
@protocol FSQPPilgrimGeofenceEvent;

SWIFT_PROTOCOL("_TtP7Pilgrim26FSQPPilgrimCurrentLocation_")
@protocol FSQPPilgrimCurrentLocation
@property (nonatomic, readonly, strong) id <FSQPPilgrimVisit> _Nullable currentPlace;
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimGeofenceEvent>> * _Nullable matchedGeofences;
@end

@class NSDate;

SWIFT_PROTOCOL("_TtP7Pilgrim19FSQPPilgrimDebugLog_")
@protocol FSQPPilgrimDebugLog
@property (nonatomic, readonly, copy) NSDate * _Nonnull timestamp;
@property (nonatomic, readonly) enum FSQPLogLevel level;
@property (nonatomic, readonly) enum FSQPLogType type;
@property (nonatomic, readonly) id _Nullable data;
@property (nonatomic, readonly, copy) NSString * _Nonnull eventDescription;
@property (nonatomic, readonly) BOOL isPublic;
@end

@protocol FSQPPilgrimVenue;
@protocol FSQPPilgrimGeofenceBoundary;

SWIFT_PROTOCOL("_TtP7Pilgrim19FSQPPilgrimGeofence_")
@protocol FSQPPilgrimGeofence
@property (nonatomic, readonly, copy) NSString * _Nonnull id;
@property (nonatomic, readonly) NSTimeInterval dwellTime;
@property (nonatomic, readonly, strong) id <FSQPPilgrimVenue> _Nullable venue;
@property (nonatomic, readonly, copy) NSArray<NSString *> * _Nullable categoryIds;
@property (nonatomic, readonly, copy) NSArray<NSString *> * _Nullable chainIds;
@property (nonatomic, readonly, copy) NSString * _Nullable partnerVenueId;
@property (nonatomic, readonly) enum FSQPGeofenceType type;
@property (nonatomic, readonly, copy) NSString * _Nonnull name;
@property (nonatomic, readonly, strong) id <FSQPPilgrimGeofenceBoundary> _Nonnull boundary;
@property (nonatomic, readonly, copy) NSDictionary<NSString *, NSString *> * _Nullable properties;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim23FSQPPilgrimGeofenceArea_")
@protocol FSQPPilgrimGeofenceArea
@property (nonatomic, readonly) CLLocationDegrees lat;
@property (nonatomic, readonly) CLLocationDegrees lng;
@property (nonatomic, readonly) CLLocationDistance radius;
@property (nonatomic, readonly) CLLocationDistance threshold;
@end

@protocol FSQPPilgrimLocation;
@class CLLocation;

SWIFT_PROTOCOL("_TtP7Pilgrim27FSQPPilgrimGeofenceBoundary_")
@protocol FSQPPilgrimGeofenceBoundary
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimLocation>> * _Nullable points;
@property (nonatomic, readonly, strong) id <FSQPPilgrimLocation> _Nullable center;
@property (nonatomic, readonly, strong) NSNumber * _Nullable radius;
- (BOOL)didEnterWith:(CLLocation * _Nonnull)location SWIFT_WARN_UNUSED_RESULT;
- (BOOL)didExitWith:(CLLocation * _Nonnull)location SWIFT_WARN_UNUSED_RESULT;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim24FSQPPilgrimGeofenceEvent_")
@protocol FSQPPilgrimGeofenceEvent
@property (nonatomic, readonly) enum FSQPGeofenceEventType eventType;
@property (nonatomic, readonly, strong) id <FSQPPilgrimGeofence> _Nonnull geofence;
@property (nonatomic, readonly, strong) id <FSQPPilgrimLocation> _Nonnull location;
@end

@protocol FSQPPilgrimUserState;

SWIFT_PROTOCOL("_TtP7Pilgrim29FSQPPilgrimLastKnownUserState_")
@protocol FSQPPilgrimLastKnownUserState
@property (nonatomic, readonly, strong) id <FSQPPilgrimUserState> _Nonnull state;
@property (nonatomic, readonly) CLLocationCoordinate2D coordinate;
@property (nonatomic, readonly, copy) NSDate * _Nonnull timestamp;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim19FSQPPilgrimLocation_")
@protocol FSQPPilgrimLocation
@property (nonatomic, readonly) CLLocationDegrees lat;
@property (nonatomic, readonly) CLLocationDegrees lng;
@property (nonatomic, readonly) CLLocationAccuracy hacc;
@property (nonatomic, readonly, copy) NSDate * _Nonnull timestamp;
@property (nonatomic, readonly) CLLocationSpeed speed;
@property (nonatomic, readonly) CLLocationDirection heading;
@property (nonatomic, readonly) CLLocationDistance altitude;
@property (nonatomic, readonly) CLLocationAccuracy vacc;
@property (nonatomic, readonly, strong) NSNumber * _Nullable floor;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim24FSQPPilgrimLocationVisit_")
@protocol FSQPPilgrimLocationVisit
@property (nonatomic, readonly, strong) id <FSQPPilgrimLocation> _Nullable arrival;
@property (nonatomic, readonly, strong) id <FSQPPilgrimLocation> _Nullable departure;
@property (nonatomic, readonly, copy) NSString * _Nonnull stopDetectionAlgorithm;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim22FSQPPilgrimUserSegment_")
@protocol FSQPPilgrimUserSegment
@property (nonatomic, readonly) NSInteger segmentId;
@property (nonatomic, readonly, copy) NSString * _Nonnull name;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim20FSQPPilgrimUserState_")
@protocol FSQPPilgrimUserState
@property (nonatomic, readonly) BOOL traveling;
@property (nonatomic, readonly) BOOL commuting;
@property (nonatomic, readonly, copy) NSString * _Nullable state;
@property (nonatomic, readonly, copy) NSString * _Nullable city;
@property (nonatomic, readonly, copy) NSString * _Nullable postalCode;
@property (nonatomic, readonly, copy) NSString * _Nullable country;
@property (nonatomic, readonly, copy) NSString * _Nullable county;
@property (nonatomic, readonly, copy) NSString * _Nullable dma;
@property (nonatomic, readonly) FSQPUserStateComponent changedComponents;
@end

@protocol FSQPPilgrimVenueLocation;

SWIFT_PROTOCOL("_TtP7Pilgrim16FSQPPilgrimVenue_")
@protocol FSQPPilgrimVenue
@property (nonatomic, readonly, copy) NSString * _Nonnull id;
@property (nonatomic, readonly, copy) NSString * _Nonnull name;
@property (nonatomic, readonly, strong) NSNumber * _Nullable probability;
@property (nonatomic, readonly, copy) NSString * _Nullable partnerVenueId;
@property (nonatomic, readonly, strong) id <FSQPPilgrimVenueLocation> _Nullable location;
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimChain>> * _Nonnull venueChains;
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimCategory>> * _Nonnull categories;
@property (nonatomic, readonly, strong) id <FSQPPilgrimCategory> _Nullable primaryCategory;
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimVenue>> * _Nonnull hierarchy;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim24FSQPPilgrimVenueLocation_")
@protocol FSQPPilgrimVenueLocation
@property (nonatomic, readonly, copy) NSString * _Nullable address;
@property (nonatomic, readonly, copy) NSString * _Nullable crossStreet;
@property (nonatomic, readonly, copy) NSString * _Nullable city;
@property (nonatomic, readonly, copy) NSString * _Nullable state;
@property (nonatomic, readonly, copy) NSString * _Nullable postalCode;
@property (nonatomic, readonly, copy) NSString * _Nullable country;
@property (nonatomic, readonly) CLLocationCoordinate2D coordinate;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim16FSQPPilgrimVisit_")
@protocol FSQPPilgrimVisit
@property (nonatomic, readonly) FSQPLocationType locationType;
@property (nonatomic, readonly, copy) NSString * _Nonnull locationTypeString;
@property (nonatomic, readonly) FSQPConfidence confidence;
@property (nonatomic, readonly, copy) NSString * _Nonnull confidenceString;
@property (nonatomic, readonly, strong) id <FSQPPilgrimVenue> _Nullable venue;
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimVenue>> * _Nullable otherPossibleVenues;
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimUserSegment>> * _Nullable segments;
@property (nonatomic, readonly, copy) NSString * _Nonnull displayName;
@property (nonatomic, readonly, copy) NSString * _Nullable pilgrimVisitId;
@property (nonatomic, readonly, strong) NSNumber * _Nullable matchedTrigger;
@property (nonatomic, readonly, strong) id <FSQPPilgrimLocationVisit> _Nonnull locationVisit;
@property (nonatomic, readonly, strong) NSNumber * _Nullable regionLat;
@property (nonatomic, readonly, strong) NSNumber * _Nullable regionLng;
@end


@class NSValue;
@class FSQPDebugLog;
@class FSQPGeofenceRegion;
@class FSQPGeofence;
@class FSQPLastKnownUserState;
@class NSURL;
@protocol FSQPPilgrimManagerDelegate;
@class FSQPCurrentLocation;
@class FSQPVisit;
@class UIViewController;

SWIFT_CLASS("_TtC7Pilgrim11PilgrimCore")
@interface PilgrimCore : NSObject
@property (nonatomic, readonly, copy) NSArray<NSValue *> * _Nonnull homeRegions;
@property (nonatomic, readonly, copy) NSArray<NSValue *> * _Nonnull workRegions;
@property (nonatomic, readonly, copy) NSArray<FSQPDebugLog *> * _Nonnull debugLogs;
- (void)debugLogsWith:(void (^ _Nonnull)(NSArray<FSQPDebugLog *> * _Nonnull))completion;
@property (nonatomic, readonly, strong) FSQPGeofenceRegion * _Nullable geofenceArea;
@property (nonatomic, readonly, copy) NSArray<FSQPGeofence *> * _Nonnull geofences;
@property (nonatomic, readonly, copy) NSDictionary<NSString *, NSString *> * _Nonnull userInfo;
@property (nonatomic) BOOL liveDebugEnabled;
@property (nonatomic) BOOL disableAdIdentitySharing;
@property (nonatomic, readonly, strong) FSQPLastKnownUserState * _Nullable lastKnownUserState;
@property (nonatomic) BOOL isDebugLogsEnabled;
@property (nonatomic) BOOL shouldShowNonPublicLogs;
@property (nonatomic, copy) NSString * _Nullable oauthToken;
@property (nonatomic, readonly, copy) NSString * _Nonnull pilgrimSDKVersionString;
@property (nonatomic, readonly, copy) NSString * _Nullable installId;
@property (nonatomic, readonly, copy) NSURL * _Nullable databaseURL;
- (nonnull instancetype)init;
- (void)configureWithConsumerKey:(NSString * _Nonnull)consumerKey secret:(NSString * _Nonnull)secret oauthToken:(NSString * _Nullable)oauthToken delegate:(id <FSQPPilgrimManagerDelegate> _Nullable)delegate completion:(void (^ _Nullable)(BOOL, NSError * _Nullable))completion;
- (void)updateCredentialsWithConsumerKey:(NSString * _Nonnull)consumerKey secret:(NSString * _Nonnull)secret oauthToken:(NSString * _Nullable)oauthToken;
- (void)install;
- (void)start;
- (void)stopWithReportDisable:(BOOL)reportDisable;
- (void)getCurrentLocationWithCompletion:(void (^ _Nonnull)(FSQPCurrentLocation * _Nullable, NSError * _Nullable))completion;
- (void)clearAllDataWithCompletion:(void (^ _Nullable)(void))completion;
- (void)deleteDebugLogs;
- (void)fireTestDepartureWithVisit:(FSQPVisit * _Nonnull)visit dwellTime:(NSTimeInterval)dwellTime;
- (void)fireTestVisitWithLocation:(CLLocation * _Nonnull)location;
- (void)fireTestVisitWithConfidence:(FSQPConfidence)confidence locationType:(FSQPLocationType)locationType isDeparture:(BOOL)isDeparture;
- (void)setUserInfo:(NSDictionary<NSString *, NSString *> * _Nullable)userInfo persisted:(BOOL)persisted;
- (void)leaveWithFeedback:(enum FSQPVisitFeedback)feedback visitId:(NSString * _Nonnull)visitId actualVenueId:(NSString * _Nullable)actualVenueId completion:(void (^ _Nullable)(NSError * _Nullable))completion;
- (void)checkinWithVenueId:(NSString * _Nonnull)venueId;
- (void)checkinWithPartnerVenueId:(NSString * _Nonnull)partnerVenueId;
- (void)sendTestSentryMessage;
- (void)upsertInterceptors:(NSArray * _Nonnull)externalIntercetors;
- (void)refreshGeofencesWithLocation:(CLLocation * _Nonnull)location;
- (void)presentDebugViewControllerFrom:(UIViewController * _Nonnull)parentViewController;
@end





#if __has_attribute(external_source_symbol)
# pragma clang attribute pop
#endif
#pragma clang diagnostic pop
#endif

#elif defined(__ARM_ARCH_7A__) && __ARM_ARCH_7A__
// Generated by Apple Swift version 5.4.2 (swiftlang-1205.0.28.2 clang-1205.0.19.57)
#ifndef PILGRIM_SWIFT_H
#define PILGRIM_SWIFT_H
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Wgcc-compat"

#if !defined(__has_include)
# define __has_include(x) 0
#endif
#if !defined(__has_attribute)
# define __has_attribute(x) 0
#endif
#if !defined(__has_feature)
# define __has_feature(x) 0
#endif
#if !defined(__has_warning)
# define __has_warning(x) 0
#endif

#if __has_include(<swift/objc-prologue.h>)
# include <swift/objc-prologue.h>
#endif

#pragma clang diagnostic ignored "-Wauto-import"
#include <Foundation/Foundation.h>
#include <stdint.h>
#include <stddef.h>
#include <stdbool.h>

#if !defined(SWIFT_TYPEDEFS)
# define SWIFT_TYPEDEFS 1
# if __has_include(<uchar.h>)
#  include <uchar.h>
# elif !defined(__cplusplus)
typedef uint_least16_t char16_t;
typedef uint_least32_t char32_t;
# endif
typedef float swift_float2  __attribute__((__ext_vector_type__(2)));
typedef float swift_float3  __attribute__((__ext_vector_type__(3)));
typedef float swift_float4  __attribute__((__ext_vector_type__(4)));
typedef double swift_double2  __attribute__((__ext_vector_type__(2)));
typedef double swift_double3  __attribute__((__ext_vector_type__(3)));
typedef double swift_double4  __attribute__((__ext_vector_type__(4)));
typedef int swift_int2  __attribute__((__ext_vector_type__(2)));
typedef int swift_int3  __attribute__((__ext_vector_type__(3)));
typedef int swift_int4  __attribute__((__ext_vector_type__(4)));
typedef unsigned int swift_uint2  __attribute__((__ext_vector_type__(2)));
typedef unsigned int swift_uint3  __attribute__((__ext_vector_type__(3)));
typedef unsigned int swift_uint4  __attribute__((__ext_vector_type__(4)));
#endif

#if !defined(SWIFT_PASTE)
# define SWIFT_PASTE_HELPER(x, y) x##y
# define SWIFT_PASTE(x, y) SWIFT_PASTE_HELPER(x, y)
#endif
#if !defined(SWIFT_METATYPE)
# define SWIFT_METATYPE(X) Class
#endif
#if !defined(SWIFT_CLASS_PROPERTY)
# if __has_feature(objc_class_property)
#  define SWIFT_CLASS_PROPERTY(...) __VA_ARGS__
# else
#  define SWIFT_CLASS_PROPERTY(...)
# endif
#endif

#if __has_attribute(objc_runtime_name)
# define SWIFT_RUNTIME_NAME(X) __attribute__((objc_runtime_name(X)))
#else
# define SWIFT_RUNTIME_NAME(X)
#endif
#if __has_attribute(swift_name)
# define SWIFT_COMPILE_NAME(X) __attribute__((swift_name(X)))
#else
# define SWIFT_COMPILE_NAME(X)
#endif
#if __has_attribute(objc_method_family)
# define SWIFT_METHOD_FAMILY(X) __attribute__((objc_method_family(X)))
#else
# define SWIFT_METHOD_FAMILY(X)
#endif
#if __has_attribute(noescape)
# define SWIFT_NOESCAPE __attribute__((noescape))
#else
# define SWIFT_NOESCAPE
#endif
#if __has_attribute(ns_consumed)
# define SWIFT_RELEASES_ARGUMENT __attribute__((ns_consumed))
#else
# define SWIFT_RELEASES_ARGUMENT
#endif
#if __has_attribute(warn_unused_result)
# define SWIFT_WARN_UNUSED_RESULT __attribute__((warn_unused_result))
#else
# define SWIFT_WARN_UNUSED_RESULT
#endif
#if __has_attribute(noreturn)
# define SWIFT_NORETURN __attribute__((noreturn))
#else
# define SWIFT_NORETURN
#endif
#if !defined(SWIFT_CLASS_EXTRA)
# define SWIFT_CLASS_EXTRA
#endif
#if !defined(SWIFT_PROTOCOL_EXTRA)
# define SWIFT_PROTOCOL_EXTRA
#endif
#if !defined(SWIFT_ENUM_EXTRA)
# define SWIFT_ENUM_EXTRA
#endif
#if !defined(SWIFT_CLASS)
# if __has_attribute(objc_subclassing_restricted)
#  define SWIFT_CLASS(SWIFT_NAME) SWIFT_RUNTIME_NAME(SWIFT_NAME) __attribute__((objc_subclassing_restricted)) SWIFT_CLASS_EXTRA
#  define SWIFT_CLASS_NAMED(SWIFT_NAME) __attribute__((objc_subclassing_restricted)) SWIFT_COMPILE_NAME(SWIFT_NAME) SWIFT_CLASS_EXTRA
# else
#  define SWIFT_CLASS(SWIFT_NAME) SWIFT_RUNTIME_NAME(SWIFT_NAME) SWIFT_CLASS_EXTRA
#  define SWIFT_CLASS_NAMED(SWIFT_NAME) SWIFT_COMPILE_NAME(SWIFT_NAME) SWIFT_CLASS_EXTRA
# endif
#endif
#if !defined(SWIFT_RESILIENT_CLASS)
# if __has_attribute(objc_class_stub)
#  define SWIFT_RESILIENT_CLASS(SWIFT_NAME) SWIFT_CLASS(SWIFT_NAME) __attribute__((objc_class_stub))
#  define SWIFT_RESILIENT_CLASS_NAMED(SWIFT_NAME) __attribute__((objc_class_stub)) SWIFT_CLASS_NAMED(SWIFT_NAME)
# else
#  define SWIFT_RESILIENT_CLASS(SWIFT_NAME) SWIFT_CLASS(SWIFT_NAME)
#  define SWIFT_RESILIENT_CLASS_NAMED(SWIFT_NAME) SWIFT_CLASS_NAMED(SWIFT_NAME)
# endif
#endif

#if !defined(SWIFT_PROTOCOL)
# define SWIFT_PROTOCOL(SWIFT_NAME) SWIFT_RUNTIME_NAME(SWIFT_NAME) SWIFT_PROTOCOL_EXTRA
# define SWIFT_PROTOCOL_NAMED(SWIFT_NAME) SWIFT_COMPILE_NAME(SWIFT_NAME) SWIFT_PROTOCOL_EXTRA
#endif

#if !defined(SWIFT_EXTENSION)
# define SWIFT_EXTENSION(M) SWIFT_PASTE(M##_Swift_, __LINE__)
#endif

#if !defined(OBJC_DESIGNATED_INITIALIZER)
# if __has_attribute(objc_designated_initializer)
#  define OBJC_DESIGNATED_INITIALIZER __attribute__((objc_designated_initializer))
# else
#  define OBJC_DESIGNATED_INITIALIZER
# endif
#endif
#if !defined(SWIFT_ENUM_ATTR)
# if defined(__has_attribute) && __has_attribute(enum_extensibility)
#  define SWIFT_ENUM_ATTR(_extensibility) __attribute__((enum_extensibility(_extensibility)))
# else
#  define SWIFT_ENUM_ATTR(_extensibility)
# endif
#endif
#if !defined(SWIFT_ENUM)
# define SWIFT_ENUM(_type, _name, _extensibility) enum _name : _type _name; enum SWIFT_ENUM_ATTR(_extensibility) SWIFT_ENUM_EXTRA _name : _type
# if __has_feature(generalized_swift_name)
#  define SWIFT_ENUM_NAMED(_type, _name, SWIFT_NAME, _extensibility) enum _name : _type _name SWIFT_COMPILE_NAME(SWIFT_NAME); enum SWIFT_COMPILE_NAME(SWIFT_NAME) SWIFT_ENUM_ATTR(_extensibility) SWIFT_ENUM_EXTRA _name : _type
# else
#  define SWIFT_ENUM_NAMED(_type, _name, SWIFT_NAME, _extensibility) SWIFT_ENUM(_type, _name, _extensibility)
# endif
#endif
#if !defined(SWIFT_UNAVAILABLE)
# define SWIFT_UNAVAILABLE __attribute__((unavailable))
#endif
#if !defined(SWIFT_UNAVAILABLE_MSG)
# define SWIFT_UNAVAILABLE_MSG(msg) __attribute__((unavailable(msg)))
#endif
#if !defined(SWIFT_AVAILABILITY)
# define SWIFT_AVAILABILITY(plat, ...) __attribute__((availability(plat, __VA_ARGS__)))
#endif
#if !defined(SWIFT_WEAK_IMPORT)
# define SWIFT_WEAK_IMPORT __attribute__((weak_import))
#endif
#if !defined(SWIFT_DEPRECATED)
# define SWIFT_DEPRECATED __attribute__((deprecated))
#endif
#if !defined(SWIFT_DEPRECATED_MSG)
# define SWIFT_DEPRECATED_MSG(...) __attribute__((deprecated(__VA_ARGS__)))
#endif
#if __has_feature(attribute_diagnose_if_objc)
# define SWIFT_DEPRECATED_OBJC(Msg) __attribute__((diagnose_if(1, Msg, "warning")))
#else
# define SWIFT_DEPRECATED_OBJC(Msg) SWIFT_DEPRECATED_MSG(Msg)
#endif
#if !defined(IBSegueAction)
# define IBSegueAction
#endif
#if __has_feature(modules)
#if __has_warning("-Watimport-in-framework-header")
#pragma clang diagnostic ignored "-Watimport-in-framework-header"
#endif
@import CoreLocation;
@import Foundation;
@import ObjectiveC;
#endif

#import <Pilgrim/Pilgrim.h>

#pragma clang diagnostic ignored "-Wproperty-attribute-mismatch"
#pragma clang diagnostic ignored "-Wduplicate-method-arg"
#if __has_warning("-Wpragma-clang-attribute")
# pragma clang diagnostic ignored "-Wpragma-clang-attribute"
#endif
#pragma clang diagnostic ignored "-Wunknown-pragmas"
#pragma clang diagnostic ignored "-Wnullability"

#if __has_attribute(external_source_symbol)
# pragma push_macro("any")
# undef any
# pragma clang attribute push(__attribute__((external_source_symbol(language="Swift", defined_in="Pilgrim",generated_declaration))), apply_to=any(function,enum,objc_interface,objc_category,objc_protocol))
# pragma pop_macro("any")
#endif





@class NSString;
@protocol FSQPPilgrimCategoryIcon;
@class NSNumber;

SWIFT_PROTOCOL("_TtP7Pilgrim19FSQPPilgrimCategory_")
@protocol FSQPPilgrimCategory
@property (nonatomic, readonly, copy) NSString * _Nonnull id;
@property (nonatomic, readonly, copy) NSString * _Nonnull name;
@property (nonatomic, readonly, copy) NSString * _Nullable pluralName;
@property (nonatomic, readonly, copy) NSString * _Nullable shortName;
@property (nonatomic, readonly, strong) id <FSQPPilgrimCategoryIcon> _Nullable icon;
@property (nonatomic, readonly) BOOL primary;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim23FSQPPilgrimCategoryIcon_")
@protocol FSQPPilgrimCategoryIcon
@property (nonatomic, readonly, copy) NSString * _Nonnull prefix;
@property (nonatomic, readonly, copy) NSString * _Nonnull suffix;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim16FSQPPilgrimChain_")
@protocol FSQPPilgrimChain
@property (nonatomic, readonly, copy) NSString * _Nonnull id;
@property (nonatomic, readonly, copy) NSString * _Nonnull name;
@end

@protocol FSQPPilgrimVisit;
@protocol FSQPPilgrimGeofenceEvent;

SWIFT_PROTOCOL("_TtP7Pilgrim26FSQPPilgrimCurrentLocation_")
@protocol FSQPPilgrimCurrentLocation
@property (nonatomic, readonly, strong) id <FSQPPilgrimVisit> _Nullable currentPlace;
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimGeofenceEvent>> * _Nullable matchedGeofences;
@end

@class NSDate;

SWIFT_PROTOCOL("_TtP7Pilgrim19FSQPPilgrimDebugLog_")
@protocol FSQPPilgrimDebugLog
@property (nonatomic, readonly, copy) NSDate * _Nonnull timestamp;
@property (nonatomic, readonly) enum FSQPLogLevel level;
@property (nonatomic, readonly) enum FSQPLogType type;
@property (nonatomic, readonly) id _Nullable data;
@property (nonatomic, readonly, copy) NSString * _Nonnull eventDescription;
@property (nonatomic, readonly) BOOL isPublic;
@end

@protocol FSQPPilgrimVenue;
@protocol FSQPPilgrimGeofenceBoundary;

SWIFT_PROTOCOL("_TtP7Pilgrim19FSQPPilgrimGeofence_")
@protocol FSQPPilgrimGeofence
@property (nonatomic, readonly, copy) NSString * _Nonnull id;
@property (nonatomic, readonly) NSTimeInterval dwellTime;
@property (nonatomic, readonly, strong) id <FSQPPilgrimVenue> _Nullable venue;
@property (nonatomic, readonly, copy) NSArray<NSString *> * _Nullable categoryIds;
@property (nonatomic, readonly, copy) NSArray<NSString *> * _Nullable chainIds;
@property (nonatomic, readonly, copy) NSString * _Nullable partnerVenueId;
@property (nonatomic, readonly) enum FSQPGeofenceType type;
@property (nonatomic, readonly, copy) NSString * _Nonnull name;
@property (nonatomic, readonly, strong) id <FSQPPilgrimGeofenceBoundary> _Nonnull boundary;
@property (nonatomic, readonly, copy) NSDictionary<NSString *, NSString *> * _Nullable properties;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim23FSQPPilgrimGeofenceArea_")
@protocol FSQPPilgrimGeofenceArea
@property (nonatomic, readonly) CLLocationDegrees lat;
@property (nonatomic, readonly) CLLocationDegrees lng;
@property (nonatomic, readonly) CLLocationDistance radius;
@property (nonatomic, readonly) CLLocationDistance threshold;
@end

@protocol FSQPPilgrimLocation;
@class CLLocation;

SWIFT_PROTOCOL("_TtP7Pilgrim27FSQPPilgrimGeofenceBoundary_")
@protocol FSQPPilgrimGeofenceBoundary
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimLocation>> * _Nullable points;
@property (nonatomic, readonly, strong) id <FSQPPilgrimLocation> _Nullable center;
@property (nonatomic, readonly, strong) NSNumber * _Nullable radius;
- (BOOL)didEnterWith:(CLLocation * _Nonnull)location SWIFT_WARN_UNUSED_RESULT;
- (BOOL)didExitWith:(CLLocation * _Nonnull)location SWIFT_WARN_UNUSED_RESULT;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim24FSQPPilgrimGeofenceEvent_")
@protocol FSQPPilgrimGeofenceEvent
@property (nonatomic, readonly) enum FSQPGeofenceEventType eventType;
@property (nonatomic, readonly, strong) id <FSQPPilgrimGeofence> _Nonnull geofence;
@property (nonatomic, readonly, strong) id <FSQPPilgrimLocation> _Nonnull location;
@end

@protocol FSQPPilgrimUserState;

SWIFT_PROTOCOL("_TtP7Pilgrim29FSQPPilgrimLastKnownUserState_")
@protocol FSQPPilgrimLastKnownUserState
@property (nonatomic, readonly, strong) id <FSQPPilgrimUserState> _Nonnull state;
@property (nonatomic, readonly) CLLocationCoordinate2D coordinate;
@property (nonatomic, readonly, copy) NSDate * _Nonnull timestamp;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim19FSQPPilgrimLocation_")
@protocol FSQPPilgrimLocation
@property (nonatomic, readonly) CLLocationDegrees lat;
@property (nonatomic, readonly) CLLocationDegrees lng;
@property (nonatomic, readonly) CLLocationAccuracy hacc;
@property (nonatomic, readonly, copy) NSDate * _Nonnull timestamp;
@property (nonatomic, readonly) CLLocationSpeed speed;
@property (nonatomic, readonly) CLLocationDirection heading;
@property (nonatomic, readonly) CLLocationDistance altitude;
@property (nonatomic, readonly) CLLocationAccuracy vacc;
@property (nonatomic, readonly, strong) NSNumber * _Nullable floor;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim24FSQPPilgrimLocationVisit_")
@protocol FSQPPilgrimLocationVisit
@property (nonatomic, readonly, strong) id <FSQPPilgrimLocation> _Nullable arrival;
@property (nonatomic, readonly, strong) id <FSQPPilgrimLocation> _Nullable departure;
@property (nonatomic, readonly, copy) NSString * _Nonnull stopDetectionAlgorithm;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim22FSQPPilgrimUserSegment_")
@protocol FSQPPilgrimUserSegment
@property (nonatomic, readonly) NSInteger segmentId;
@property (nonatomic, readonly, copy) NSString * _Nonnull name;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim20FSQPPilgrimUserState_")
@protocol FSQPPilgrimUserState
@property (nonatomic, readonly) BOOL traveling;
@property (nonatomic, readonly) BOOL commuting;
@property (nonatomic, readonly, copy) NSString * _Nullable state;
@property (nonatomic, readonly, copy) NSString * _Nullable city;
@property (nonatomic, readonly, copy) NSString * _Nullable postalCode;
@property (nonatomic, readonly, copy) NSString * _Nullable country;
@property (nonatomic, readonly, copy) NSString * _Nullable county;
@property (nonatomic, readonly, copy) NSString * _Nullable dma;
@property (nonatomic, readonly) FSQPUserStateComponent changedComponents;
@end

@protocol FSQPPilgrimVenueLocation;

SWIFT_PROTOCOL("_TtP7Pilgrim16FSQPPilgrimVenue_")
@protocol FSQPPilgrimVenue
@property (nonatomic, readonly, copy) NSString * _Nonnull id;
@property (nonatomic, readonly, copy) NSString * _Nonnull name;
@property (nonatomic, readonly, strong) NSNumber * _Nullable probability;
@property (nonatomic, readonly, copy) NSString * _Nullable partnerVenueId;
@property (nonatomic, readonly, strong) id <FSQPPilgrimVenueLocation> _Nullable location;
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimChain>> * _Nonnull venueChains;
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimCategory>> * _Nonnull categories;
@property (nonatomic, readonly, strong) id <FSQPPilgrimCategory> _Nullable primaryCategory;
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimVenue>> * _Nonnull hierarchy;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim24FSQPPilgrimVenueLocation_")
@protocol FSQPPilgrimVenueLocation
@property (nonatomic, readonly, copy) NSString * _Nullable address;
@property (nonatomic, readonly, copy) NSString * _Nullable crossStreet;
@property (nonatomic, readonly, copy) NSString * _Nullable city;
@property (nonatomic, readonly, copy) NSString * _Nullable state;
@property (nonatomic, readonly, copy) NSString * _Nullable postalCode;
@property (nonatomic, readonly, copy) NSString * _Nullable country;
@property (nonatomic, readonly) CLLocationCoordinate2D coordinate;
@end


SWIFT_PROTOCOL("_TtP7Pilgrim16FSQPPilgrimVisit_")
@protocol FSQPPilgrimVisit
@property (nonatomic, readonly) FSQPLocationType locationType;
@property (nonatomic, readonly, copy) NSString * _Nonnull locationTypeString;
@property (nonatomic, readonly) FSQPConfidence confidence;
@property (nonatomic, readonly, copy) NSString * _Nonnull confidenceString;
@property (nonatomic, readonly, strong) id <FSQPPilgrimVenue> _Nullable venue;
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimVenue>> * _Nullable otherPossibleVenues;
@property (nonatomic, readonly, copy) NSArray<id <FSQPPilgrimUserSegment>> * _Nullable segments;
@property (nonatomic, readonly, copy) NSString * _Nonnull displayName;
@property (nonatomic, readonly, copy) NSString * _Nullable pilgrimVisitId;
@property (nonatomic, readonly, strong) NSNumber * _Nullable matchedTrigger;
@property (nonatomic, readonly, strong) id <FSQPPilgrimLocationVisit> _Nonnull locationVisit;
@property (nonatomic, readonly, strong) NSNumber * _Nullable regionLat;
@property (nonatomic, readonly, strong) NSNumber * _Nullable regionLng;
@end


@class NSValue;
@class FSQPDebugLog;
@class FSQPGeofenceRegion;
@class FSQPGeofence;
@class FSQPLastKnownUserState;
@class NSURL;
@protocol FSQPPilgrimManagerDelegate;
@class FSQPCurrentLocation;
@class FSQPVisit;
@class UIViewController;

SWIFT_CLASS("_TtC7Pilgrim11PilgrimCore")
@interface PilgrimCore : NSObject
@property (nonatomic, readonly, copy) NSArray<NSValue *> * _Nonnull homeRegions;
@property (nonatomic, readonly, copy) NSArray<NSValue *> * _Nonnull workRegions;
@property (nonatomic, readonly, copy) NSArray<FSQPDebugLog *> * _Nonnull debugLogs;
- (void)debugLogsWith:(void (^ _Nonnull)(NSArray<FSQPDebugLog *> * _Nonnull))completion;
@property (nonatomic, readonly, strong) FSQPGeofenceRegion * _Nullable geofenceArea;
@property (nonatomic, readonly, copy) NSArray<FSQPGeofence *> * _Nonnull geofences;
@property (nonatomic, readonly, copy) NSDictionary<NSString *, NSString *> * _Nonnull userInfo;
@property (nonatomic) BOOL liveDebugEnabled;
@property (nonatomic) BOOL disableAdIdentitySharing;
@property (nonatomic, readonly, strong) FSQPLastKnownUserState * _Nullable lastKnownUserState;
@property (nonatomic) BOOL isDebugLogsEnabled;
@property (nonatomic) BOOL shouldShowNonPublicLogs;
@property (nonatomic, copy) NSString * _Nullable oauthToken;
@property (nonatomic, readonly, copy) NSString * _Nonnull pilgrimSDKVersionString;
@property (nonatomic, readonly, copy) NSString * _Nullable installId;
@property (nonatomic, readonly, copy) NSURL * _Nullable databaseURL;
- (nonnull instancetype)init;
- (void)configureWithConsumerKey:(NSString * _Nonnull)consumerKey secret:(NSString * _Nonnull)secret oauthToken:(NSString * _Nullable)oauthToken delegate:(id <FSQPPilgrimManagerDelegate> _Nullable)delegate completion:(void (^ _Nullable)(BOOL, NSError * _Nullable))completion;
- (void)updateCredentialsWithConsumerKey:(NSString * _Nonnull)consumerKey secret:(NSString * _Nonnull)secret oauthToken:(NSString * _Nullable)oauthToken;
- (void)install;
- (void)start;
- (void)stopWithReportDisable:(BOOL)reportDisable;
- (void)getCurrentLocationWithCompletion:(void (^ _Nonnull)(FSQPCurrentLocation * _Nullable, NSError * _Nullable))completion;
- (void)clearAllDataWithCompletion:(void (^ _Nullable)(void))completion;
- (void)deleteDebugLogs;
- (void)fireTestDepartureWithVisit:(FSQPVisit * _Nonnull)visit dwellTime:(NSTimeInterval)dwellTime;
- (void)fireTestVisitWithLocation:(CLLocation * _Nonnull)location;
- (void)fireTestVisitWithConfidence:(FSQPConfidence)confidence locationType:(FSQPLocationType)locationType isDeparture:(BOOL)isDeparture;
- (void)setUserInfo:(NSDictionary<NSString *, NSString *> * _Nullable)userInfo persisted:(BOOL)persisted;
- (void)leaveWithFeedback:(enum FSQPVisitFeedback)feedback visitId:(NSString * _Nonnull)visitId actualVenueId:(NSString * _Nullable)actualVenueId completion:(void (^ _Nullable)(NSError * _Nullable))completion;
- (void)checkinWithVenueId:(NSString * _Nonnull)venueId;
- (void)checkinWithPartnerVenueId:(NSString * _Nonnull)partnerVenueId;
- (void)sendTestSentryMessage;
- (void)upsertInterceptors:(NSArray * _Nonnull)externalIntercetors;
- (void)refreshGeofencesWithLocation:(CLLocation * _Nonnull)location;
- (void)presentDebugViewControllerFrom:(UIViewController * _Nonnull)parentViewController;
@end





#if __has_attribute(external_source_symbol)
# pragma clang attribute pop
#endif
#pragma clang diagnostic pop
#endif

#endif
