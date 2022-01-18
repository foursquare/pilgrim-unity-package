#import "UnityAppController.h"
#import "PilgrimUnitySDK.h"

@interface AppController : UnityAppController

@end

IMPL_APP_CONTROLLER_SUBCLASS(AppController)

@implementation AppController

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)options {
    [PilgrimUnitySDK initWithConsumerKey:@"CONSUMER_KEY" consumerSecret:@"CONSUMER_SECRET"];
    return [super application:application didFinishLaunchingWithOptions:options];
}

@end
