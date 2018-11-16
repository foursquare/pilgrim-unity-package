using System.IO;
using UnityEngine;
using UnityEditor;

namespace Foursquare
{

    public class AndroidGenerator
    {
        
        private const string appText = 
@"package com.foursquare.unitysample;

import android.app.Application;

import com.foursquare.pilgrimunitysdk.PilgrimUnitySDK;

public final class App extends Application {{

    @Override
    public void onCreate() {{
        super.onCreate();
        PilgrimUnitySDK.init(this, ""{0}"", ""{1}"");
    }}

}}
";

        private const string manifestText = 
@"<?xml version=""1.0"" encoding=""utf-8""?>
<manifest xmlns:android=""http://schemas.android.com/apk/res/android"" package=""com.foursquare.unitysample"" xmlns:tools=""http://schemas.android.com/tools"" android:installLocation=""preferExternal"">
  <supports-screens android:smallScreens=""true"" android:normalScreens=""true"" android:largeScreens=""true"" android:xlargeScreens=""true"" android:anyDensity=""true"" />
  <application android:name=""com.foursquare.unitysample.App"" android:theme=""@style/UnityThemeSelector"" android:icon=""@mipmap/app_icon"" android:label=""@string/app_name"">
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

        public static void GenerateAppSubclass()
        {
            var consumerKey = PilgrimConfigSettings.Get("ConsumerKey");
            var consumerSecret = PilgrimConfigSettings.Get("ConsumerSecret");
            var fileContents = string.Format(appText, consumerKey, consumerSecret);
            File.WriteAllText(Application.dataPath + "/Plugins/Android/App.java", fileContents);
            AssetDatabase.Refresh();
        }

        public static void GenerateManifest()
        {
            var consumerKey = PilgrimConfigSettings.Get("ConsumerKey");
            var consumerSecret = PilgrimConfigSettings.Get("ConsumerSecret");
            var fileContents = string.Format(manifestText, consumerKey, consumerSecret);
            File.WriteAllText(Application.dataPath + "/Plugins/Android/AndroidManifest.xml", fileContents);
            AssetDatabase.Refresh();
        }   

    }

}