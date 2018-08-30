#import <Foundation/Foundation.h>

@interface PilgrimUnitySDK : NSObject

+ (instancetype)shared;

// Used to check if Pilgrim was running before and should be restarted right away, e.g. bg launch from SLC, CLVisit
// TODO(rojas): not a great name
@property (nonatomic, readonly, getter=isRunning) BOOL running;

- (void)requestLocationPermission:(nullable void (^)(BOOL didAuthorize))completion;

- (void)startWithConsumerKey:(NSString *)key secret:(NSString *)secret;

- (void)stop;

- (void)didBecomeActive;

@property (nonatomic, copy, nullable) NSString *oauthToken;

@end
