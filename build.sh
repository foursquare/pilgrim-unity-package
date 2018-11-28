#!/bin/sh

# TODO remove pilgrimsdk-[version].aar from -exportPackage, temp until Pilgrim Android is in public repo

PROJECT_ROOT=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )

UNITY_COMMAND="/Applications/Unity/Unity.app/Contents/MacOS/Unity"

PILGRIM_UNITY_SDK_VERSION="0.1.0" # TODO Get this value dynamically, maybe from git tag or something?!?
PILGRIM_UNITY_SDK_PACKAGE="$PROJECT_ROOT/pilgrim-unity-sdk-$PILGRIM_UNITY_SDK_VERSION.unitypackage"

PLAY_SERVICES_RESOLVER_PACKAGE="$PROJECT_ROOT/"$( find play-services-resolver-*.unitypackage )""

# -gvh_disable is needed for Play Services Resolver (https://github.com/googlesamples/unity-jar-resolver#getting-started)

$UNITY_COMMAND  -gvh_disable \
                -batchmode \
                -importPackage $PLAY_SERVICES_RESOLVER_PACKAGE \
                -projectPath "$PROJECT_ROOT/pilgrim-unity-sdk" \
                -exportPackage  "Assets/PlayServicesResolver" \
                                "Assets/PilgrimUnitySDK/Api" \
                                "Assets/PilgrimUnitySDK/Editor" \
                                "Assets/PilgrimUnitySDK/Platforms" \
                                "Assets/PilgrimUnitySDK/Types" \
                                "Assets/Plugins/iOS/PilgrimClient.h" \
                                "Assets/Plugins/iOS/PilgrimClient.m" \
                                "Assets/Plugins/iOS/PilgrimInterface.m" \
                                "Assets/Plugins/iOS/PilgrimTypes.h" \
                                "Assets/Plugins/iOS/PilgrimUnitySDK.h" \
                                "Assets/Plugins/iOS/PilgrimUnitySDK.m" \
                                "Assets/Plugins/Android/pilgrimunitysdk-release.aar" \
                                "Assets/Plugins/Android/pilgrimsdk-2.1.0.aar" \
                                $PILGRIM_UNITY_SDK_PACKAGE \
                -quit