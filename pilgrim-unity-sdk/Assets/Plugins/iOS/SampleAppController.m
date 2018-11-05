#import "UnityAppController.h"
#import "PilgrimUnitySDK.h"

@interface SampleAppController : UnityAppController

@end

IMPL_APP_CONTROLLER_SUBCLASS(SampleAppController)

@implementation SampleAppController

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)options {
    [PilgrimUnitySDK initWithConsumerKey:@"TMKHTRWRRYO4WIZPVJNHA1Q3JU0YBED5XIONMQTOC00YYCLY"
                          consumerSecret:@"01IYW3XKATTKF40RHUOTFPU0TTFJTJ5QC1IIIXX0NLJDV1FH"];

    return [super application:application didFinishLaunchingWithOptions:options];
}

@end
