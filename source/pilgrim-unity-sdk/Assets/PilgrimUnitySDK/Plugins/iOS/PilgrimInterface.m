//
//  PilgrimInterface.m
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

#import "PilgrimClient.h"
#import "PilgrimTypes.h"

PilgrimClientRef CreateClient(PilgrimClientHandleRef clientHandlePtr) {
    PilgrimClient *client = [[PilgrimClient alloc] initWithClientHandle:clientHandlePtr];
    return CFBridgingRetain(client);
}

void SetCallbacks(PilgrimClientRef clientPtr,
                  PilgrimLocationPermissionsCallback locationPermissionsCallback,
                  PilgrimGetCurrentLocationCallback getCurrentLocationCallback) {
    PilgrimClient *client = (__bridge PilgrimClient *)(clientPtr);
    client.locationPermissionsCallback = locationPermissionsCallback;
    client.getCurrentLocationCallback = getCurrentLocationCallback;
}

const char * GetUserInfo(PilgrimClientRef clientPtr) {
    PilgrimClient *client = (__bridge PilgrimClient *)(clientPtr);
    return [client getUserInfo];
}

void SetUserInfo(PilgrimClientRef clientPtr, const char *userInfoJson, BOOL persisted) {
    PilgrimClient *client = (__bridge PilgrimClient *)(clientPtr);
    [client setUserInfo:userInfoJson persisted:persisted];
}

void RequestLocationPermissions(PilgrimClientRef clientPtr) {
    PilgrimClient *client = (__bridge PilgrimClient *)(clientPtr);
    [client requestLocationPermissions];
}

void Start(PilgrimClientRef clientPtr) {
    PilgrimClient *client = (__bridge PilgrimClient *)(clientPtr);
    [client start];
}

void Stop(PilgrimClientRef clientPtr) {
    PilgrimClient *client = (__bridge PilgrimClient *)(clientPtr);
    [client stop];
}

void ClearAllData(PilgrimClientRef clientPtr) {
    PilgrimClient *client = (__bridge PilgrimClient *)(clientPtr);
    [client clearAllData];
}

void GetCurrentLocation(PilgrimClientRef clientPtr) {
    PilgrimClient *client = (__bridge PilgrimClient *)(clientPtr);
    [client getCurrentLocation];
}

void ShowDebugScreen(PilgrimClientRef clientPtr) {
    PilgrimClient *client = (__bridge PilgrimClient *)(clientPtr);
    [client showDebugScreen];
}

void FireTestVisit(PilgrimClientRef clientPtr, double latitude, double longitude) {
    PilgrimClient *client = (__bridge PilgrimClient *)(clientPtr);
    [client fireTestVisitWithLatitude:latitude longitude:longitude];
}

void Destroy(PilgrimClientRef clientPtr) {
    PilgrimClient *client = (__bridge PilgrimClient *)(clientPtr);
    CFRelease((__bridge CFTypeRef)(client));
}
