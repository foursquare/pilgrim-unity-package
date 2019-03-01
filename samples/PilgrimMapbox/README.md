# Pilgrim Mapbox

This demo project shows how to integrate the PilgrimUnitySDK into an application which also uses the Mapbox [package](https://www.mapbox.com/unity/).
To run the project open this directory in Unity, then open `Assets/PilgrimMapbox/Scenes/MapScene.unity`, then follow the directions in [Application Setup](https://github.com/foursquare/pilgrim-unity-sdk#application-setup). Additionally, you will need to register for a Mapbox [access token](https://account.mapbox.com/access-tokens/). You can setup the project to use the token in the menu item `Mapbox > Setup`. After setup you can build and run for iOS and Android.

![](../../images/mapbox.gif)

### Handling Duplicate Dependencies in Android

PilgrimUnitySDK and Mapbox have several duplicate Android dependencies such as okhttp, support-v4, etc. To prevent build errors you need to either delete or disable one of these duplicates. Alternatively, you can implement the `UnityEditor.Build.IPreprocessBuildWithReport` interface to handle this, for an example see [AndroidDuplicatePluginDisabler](https://github.com/foursquare/pilgrim-unity-sdk/blob/master/samples/PilgrimMapbox/Assets/PilgrimMapbox/Scripts/Editor/AndroidDuplicatePluginDisabler.cs). This class disables the duplicate plugin with the older version.
