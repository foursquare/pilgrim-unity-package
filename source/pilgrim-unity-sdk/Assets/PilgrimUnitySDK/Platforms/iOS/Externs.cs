#if UNITY_IOS

using System;
using System.Runtime.InteropServices;

namespace Foursquare.iOS
{

    public class Externs
    {

        [DllImport("__Internal")]
        internal static extern IntPtr CreateClient(IntPtr clientHandlePtr);

        [DllImport("__Internal")]
        internal static extern void SetCallbacks(IntPtr clientPtr,
                                                 PilgrimClient.PilgrimLocationPermissionsCallback locationPermissionsCallback,
                                                 PilgrimClient.PilgrimGetCurrentLocationCallback getCurrentLocationCallback);

        [DllImport("__Internal")]
        internal static extern void SetUserInfo(IntPtr clientPtr, string userInfoJson, bool persisted);

        [DllImport("__Internal")]
        internal static extern void RequestLocationPermissions(IntPtr clientPtr);

        [DllImport("__Internal")]
        internal static extern void Start(IntPtr clientPtr);

        [DllImport("__Internal")]
        internal static extern void Stop(IntPtr clientPtr);

        [DllImport("__Internal")]
        internal static extern void ClearAllData(IntPtr clientPtr);

        [DllImport("__Internal")]
        internal static extern void Destroy(IntPtr clientPtr);

        [DllImport("__Internal")]
        internal static extern void GetCurrentLocation(IntPtr clientPtr);

    }

}

#endif