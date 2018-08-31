#!/bin/sh

PROJECT_ROOT=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )

UNITY_COMMAND="/Applications/Unity/Unity.app/Contents/MacOS/Unity"

PILGRIM_UNITY_SDK_VERSION="0.1.0"
PILGRIM_UNITY_SDK_PACKAGE="$PROJECT_ROOT/pilgrim-unity-sdk-$PILGRIM_UNITY_SDK_VERSION.unitypackage"

PLAY_SERVICES_RESOLVER_VERSION="1.2.88.0"
PLAY_SERVICES_RESOLVER_PACKAGE="$PROJECT_ROOT/play-services-resolver-$PLAY_SERVICES_RESOLVER_VERSION.unitypackage"

# TODO cleanup -exportPackage line somehow
# TODO remove pilgrimsdk-2.0.0-beta2.aar temp until Pilgrim Android is in public repo

$UNITY_COMMAND  -gvh_disable \
                -batchmode \
                -importPackage $PLAY_SERVICES_RESOLVER_PACKAGE \
                -projectPath "$PROJECT_ROOT/source" \
                -exportPackage  "Assets/PlayServicesResolver" \
                                "Assets/PilgrimUnitySDK/Editor" \
                                "Assets/PilgrimUnitySDK/Foursquare" \
                                "Assets/PilgrimUnitySDK/PilgrimUnitySDK.prefab" \
                                "Assets/PilgrimUnitySDK/Scripts" \
                                "Assets/Plugins/iOS/PilgrimUnitySDK.h" \
                                "Assets/Plugins/iOS/PilgrimUnitySDK.m" \
                                "Assets/Plugins/iOS/PilgrimUnitySDKFunctions.m" \
                                "Assets/Plugins/Android/pilgrimunitysdk-release.aar" \
                                "Assets/Plugins/Android/pilgrimsdk-2.0.0-beta2.aar" \
                                $PILGRIM_UNITY_SDK_PACKAGE \
                -quit