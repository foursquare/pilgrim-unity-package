//
//  FSQPDebugModeViewController.h
//  PilgrimSDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <UIKit/UIKit.h>

NS_ASSUME_NONNULL_BEGIN

@class FSQPPilgrimManager;
/**
 *  The wrapper view controller object for the debug mode view controller.
 */
__IOS_AVAILABLE(9.0)
NS_SWIFT_NAME(DebugModeViewController)
@interface FSQPDebugModeViewController : UITabBarController

/**
 *  Unavailable. Use `initWithPilgrimManager:` instead.
 */
- (instancetype)initWithNibName:(nullable NSString *)nibNameOrNil bundle:(nullable NSBundle *)nibBundleOrNil NS_UNAVAILABLE;
/**
 *  Unavailable. Use `initWithPilgrimManager:` instead.
 */
- (instancetype)initWithCoder:(NSCoder *)aDecoder NS_UNAVAILABLE;

@end

NS_ASSUME_NONNULL_END
