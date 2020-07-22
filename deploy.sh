#!/bin/bash
set -e

./gradlew

# Ensure HEAD is at a release tag when running
version=$(git describe --exact-match HEAD)

ios_version=$(sed -n '/pilgrimiOSVersion = "[0-9].[0-9].[0-9]"/p' build.gradle | sed 's/    pilgrimiOSVersion = "\([0-9].[0-9].[0-9]\)"/\1/')
android_version=$(sed -n '/pilgrimAndroidVersion = "[0-9].[0-9].[0-9]"/p' build.gradle | sed 's/    pilgrimAndroidVersion = "\([0-9].[0-9].[0-9]\)"/\1/')
response_json=$(curl -s -X POST -d "{\"tag_name\":\"$version\",\"name\":\"$version\",\"body\":\"iOS SDK version: ${ios_version}\nAndroid SDK version: ${android_version}\",\"draft\":false,\"prerelease\":false}" -H "Authorization: token $GITHUB_TOKEN" -H "Accept: application/vnd.github.v3+json" "https://api.github.com/repos/foursquare/pilgrim-unity-package/releases")

release_id=$(echo $response_json | jq '.id')
curl -s -X POST --data-binary "@pilgrim-unity-sdk.unitypackage" -H "Authorization: token $GITHUB_TOKEN" -H "Content-Type: application/octet-stream" -H "Accept: application/vnd.github.v3+json" "https://uploads.github.com/repos/foursquare/pilgrim-unity-package/releases/$release_id/assets?name=pilgrim-unity-sdk-$version.unitypackage"
