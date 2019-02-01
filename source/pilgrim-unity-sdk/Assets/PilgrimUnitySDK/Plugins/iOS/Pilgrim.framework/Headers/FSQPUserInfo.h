//
//  FSQPUserInfo.h
//  PilgrimSDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

/**
 *  An object representing user information to be added by the Pilgrim Partner app.
 */
NS_SWIFT_NAME(UserInfo)
@interface FSQPUserInfo : NSObject

/**
 *  A dictionary representation of the FSQPUserInfo object.
 *
 *  @return A string mapping of all FSQPUserInfo data in an NSDictionary
 */
@property (nonatomic, copy, readonly) NSDictionary<NSString *, NSString*> *source;

/**
 *  Manually set custom string user data into the user info object. Note that this will
 *  silently fail and log an error if used to set one of the predefined keys that we use
 *  (ie. userId, gender, birthday, etc.), the provided setter methods should be used for
 *  those cases.
 *
 *  @warning           Do not use for predefined keys
 *
 *  @param userInfo    The value to store
 *  @param key         The key to use for the backing NSDictionary object
 */
- (void)setUserInfo:(NSString *)userInfo forKey:(NSString *)key;

/**
 *  Removes the key from the backing dictionary.
 *
 *  @warning       Do not use for predefined keys, set object to nil instead.
 *
 *  @param key     The key to use for the backing NSDictionary object.
 */
- (void)removeKey:(NSString *)key;

/**
 *  Sets a user id for the given user. Setting to nil removes the value.
 *
 *  @param userId     A NSString representing the userId to set.
 */
- (void)setUserId:(nullable NSString *)userId;

/**
 *  Sets a gender for the given user. Setting to nil removes the value.
 *
 *  @param gender     A NSString representing the gender to set.
 */
- (void)setGender:(nullable NSString *)gender;

/**
 *  Sets a birthday for the given user. This gets converted to a NSString as a NSTimeIntervalSince1970.
 *  Setting to nil removes the value.
 *
 *  @param birthday     A NSDate representing the birthday to set
 */
- (void)setBirthday:(nullable NSDate *)birthday;

/**
 *  Unavailable; use userInfo property within FSQPPilgrimManager instead
 */
- (instancetype)init NS_UNAVAILABLE;

@end

NS_ASSUME_NONNULL_END
