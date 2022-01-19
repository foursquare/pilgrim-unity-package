//
//  PilgrimTypes.h
//  PilgrimUnitySDK
//
//  Copyright © 2019 Foursquare. All rights reserved.
//

typedef const void *PilgrimClientRef;

typedef const void *PilgrimClientHandleRef;

typedef void (*PilgrimLocationPermissionsCallback)(PilgrimClientHandleRef clientHandle, BOOL granted);

typedef void (*PilgrimGetCurrentLocationCallback)(PilgrimClientHandleRef clientHandle, BOOL success, const char *currentLocationJson, const char *errorMessage);
