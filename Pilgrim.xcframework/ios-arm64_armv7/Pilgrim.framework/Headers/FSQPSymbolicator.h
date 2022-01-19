//  Copyright © 2019 Foursquare. All rights reserved.

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

NS_SWIFT_NAME(Symbolicator)
@interface FSQPSymbolicator : NSObject

+ (NSArray<NSString *> *)stackTraceInfo;

@end

NS_ASSUME_NONNULL_END
