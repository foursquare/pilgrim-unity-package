using System.IO;
using UnityEditor;
using UnityEngine;

namespace Foursquare
{

    public static class iOSGenerator
    {

        private const string appControllerText = 
@"#import ""UnityAppController.h""
#import ""PilgrimUnitySDK.h""

@interface AppController : UnityAppController

@end

IMPL_APP_CONTROLLER_SUBCLASS(AppController)

@implementation AppController

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)options {{
    [PilgrimUnitySDK initWithConsumerKey:@""{0}"" consumerSecret:@""{1}""];
    return [super application:application didFinishLaunchingWithOptions:options];
}}

@end
";
        public static void GenerateAppController()
        {
            var consumerKey = PilgrimConfigSettings.Get("ConsumerKey");
            var consumerSecret = PilgrimConfigSettings.Get("ConsumerSecret");
            var fileContents = string.Format(appControllerText, consumerKey, consumerSecret);
            File.WriteAllText(Application.dataPath + "/Plugins/iOS/AppController.m", fileContents);
            AssetDatabase.Refresh();
        }

    }

}