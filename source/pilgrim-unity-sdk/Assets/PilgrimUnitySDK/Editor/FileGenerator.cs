using System.IO;
using UnityEditor;
using UnityEngine;

namespace Foursquare
{

    public static class FileGenerator
    {
        
                private const string _appControllerText = 
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

        private const string _appSubclassText = 
@"package {0};

import android.app.Application;

import com.foursquare.pilgrimunitysdk.PilgrimUnitySDK;

public final class App extends Application {{

    @Override
    public void onCreate() {{
        super.onCreate();
        PilgrimUnitySDK.init(this, ""{1}"", ""{2}"");
    }}

}}
";

        private const string _manifestText = 
@"<?xml version=""1.0"" encoding=""utf-8""?>
<manifest xmlns:android=""http://schemas.android.com/apk/res/android"" package=""{0}"" xmlns:tools=""http://schemas.android.com/tools"" android:installLocation=""preferExternal"">
  <supports-screens android:smallScreens=""true"" android:normalScreens=""true"" android:largeScreens=""true"" android:xlargeScreens=""true"" android:anyDensity=""true"" />
  <application android:name=""{1}.App"" android:theme=""@style/UnityThemeSelector"" android:icon=""@mipmap/app_icon"" android:label=""@string/app_name"">
    <activity android:name=""com.unity3d.player.UnityPlayerActivity"" android:label=""@string/app_name"">
      <intent-filter>
        <action android:name=""android.intent.action.MAIN"" />
        <category android:name=""android.intent.category.LAUNCHER"" />
      </intent-filter>
      <meta-data android:name=""unityplayer.UnityActivity"" android:value=""true"" />
      <!-- <meta-data android:name=""unityplayer.SkipPermissionsDialog"" android:value=""true"" /> -->
    </activity>
  </application>
</manifest>";

        public static void GenerateiOSAppController()
        {
            if (HasSetConsumerKeyAndSecret()) {
                var consumerKey = PilgrimConfigSettings.GetString(PilgrimConfigSettings.ConsumerKeyKey);
                var consumerSecret = PilgrimConfigSettings.GetString(PilgrimConfigSettings.ConsumerSecretKey);
                var fileContents = string.Format(_appControllerText, consumerKey, consumerSecret);
                var filePath = Application.dataPath + "/Plugins/iOS/AppController.m";
                if (File.Exists(filePath)) {
                    Debug.LogError(filePath + " already exists!");
                } else {
                    File.WriteAllText(filePath, fileContents);
                    Debug.Log(filePath + " created successfully!");
                }
                AssetDatabase.Refresh();
            }
        }

        public static void GenerateAndroidAppSubclass()
        {
            if (HasSetConsumerKeyAndSecret() && HasSetPackageName()) {
                var consumerKey = PilgrimConfigSettings.GetString(PilgrimConfigSettings.ConsumerKeyKey);
                var consumerSecret = PilgrimConfigSettings.GetString(PilgrimConfigSettings.ConsumerSecretKey);
                var fileContents = string.Format(_appSubclassText, PlayerSettings.applicationIdentifier, consumerKey, consumerSecret);
                var filePath = Application.dataPath + "/Plugins/Android/App.java";
                if (File.Exists(filePath)) {
                    Debug.LogError(filePath + " already exists!");
                } else {
                    File.WriteAllText(filePath, fileContents);
                    Debug.Log(filePath + " created successfully!");
                }
                AssetDatabase.Refresh();
            }
        }

        public static void GenerateAndroidManifest()
        {
            if (HasSetConsumerKeyAndSecret() && HasSetPackageName()) {
                var fileContents = string.Format(_manifestText, PlayerSettings.applicationIdentifier, PlayerSettings.applicationIdentifier);
                var filePath = Application.dataPath + "/Plugins/Android/AndroidManifest.xml";
                if (File.Exists(filePath)) {
                    Debug.LogError(filePath + " already exists!");
                } else {
                    File.WriteAllText(filePath, fileContents);
                    Debug.Log(filePath + " created successfully!");
                }
                AssetDatabase.Refresh();
            }
        }

        private static bool HasSetConsumerKeyAndSecret() 
        {
            var consumerKey = PilgrimConfigSettings.GetString(PilgrimConfigSettings.ConsumerKeyKey);
            var consumerSecret = PilgrimConfigSettings.GetString(PilgrimConfigSettings.ConsumerSecretKey);
            if (consumerKey == null || consumerKey.Length == 0 || consumerSecret == null || consumerSecret.Length == 0) {
                Debug.LogError("Consumer Key and Consumer Secret are required.");
                EditorWindow.GetWindow(typeof(PilgrimConfigWindow), true, "Pilgrim").Show();
                return false;
            }
            return true;
        }

        private static bool HasSetPackageName()
        {
            if (PlayerSettings.applicationIdentifier == null || PlayerSettings.applicationIdentifier.Length == 0) {
                Debug.LogError("You must set the package name!");
                return false;
            }
            return true;
        }

    }

}