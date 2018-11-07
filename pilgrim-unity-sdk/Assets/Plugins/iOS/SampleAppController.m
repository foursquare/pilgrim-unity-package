#import "UnityAppController.h"
#import "PilgrimUnitySDK.h"

@interface SampleAppController : UnityAppController

@end

IMPL_APP_CONTROLLER_SUBCLASS(SampleAppController)

@implementation SampleAppController

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)options {
    [PilgrimUnitySDK init];

    return [super application:application didFinishLaunchingWithOptions:options];
}

@end
