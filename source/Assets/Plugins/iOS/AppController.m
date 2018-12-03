#import "UnityAppController.h"
#import "PilgrimUnitySDK.h"

@interface AppController : UnityAppController

@end

IMPL_APP_CONTROLLER_SUBCLASS(AppController)

@implementation AppController

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)options {
    [PilgrimUnitySDK initWithConsumerKey:@"SF45TRX2FDJBCZ3W2HT5J3UBU2WBLQKBCCVRBIX01AOB2GFW" consumerSecret:@"0BGOFDZK42TDSZN305VI5P241WIOMJNGWR0RXGLOZH4SPTK4"];
    return [super application:application didFinishLaunchingWithOptions:options];
}

@end
