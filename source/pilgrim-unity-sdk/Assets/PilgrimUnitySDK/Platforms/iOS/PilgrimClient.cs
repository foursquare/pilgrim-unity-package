#if UNITY_IOS

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Foursquare.iOS
{

    public class PilgrimClient : IPilgrimClient, IDisposable
    {

        public event Action<bool> OnLocationPermissionResult = delegate { };

        public event Action<CurrentLocation, Exception> OnGetCurrentLocationResult = delegate { };

        private delegate void PilgrimLocationPermissionsCallback(IntPtr clientHandlePtr, bool granted);

        private delegate void PilgrimGetCurrentLocationCallback(IntPtr clientHandlePtr, bool success, string currentLocationJson, string errorMessage);

        private GCHandle _clientHandle;

        private IntPtr _clientPtr;

        public PilgrimClient()
        {
            _clientHandle = GCHandle.Alloc(this);
            _clientPtr = Externs.CreateClient(GCHandle.ToIntPtr(_clientHandle));
            Externs.SetCallbacks(_clientPtr, OnLocationPermissionsCallback, OnGetCurrentLocationCallback);
        }

        public UserInfo GetUserInfo()
        {
            var userInfoJson = Externs.GetUserInfo(_clientPtr);
            return JsonUtility.FromJson<UserInfo>(userInfoJson);
        }

        public void SetUserInfo(UserInfo userInfo, bool persisted)
        {
            var userInfoJson = JsonUtility.ToJson(userInfo);
            Externs.SetUserInfo(_clientPtr, userInfoJson, persisted);
        }

        public void RequestLocationPermissions()
        {
            Externs.RequestLocationPermissions(_clientPtr);
        }

        public void Start()
        {
            Externs.Start(_clientPtr);
        }

        public void Stop()
        {
            Externs.Stop(_clientPtr);
        }

        public void ClearAllData()
        {
            Externs.ClearAllData(_clientPtr);
        }

        public void GetCurrentLocation()
        {
            Externs.GetCurrentLocation(_clientPtr);
        }

        public void ShowDebugScreen()
        {
            Externs.ShowDebugScreen(_clientPtr);
        }

        public void Dispose()
        {
            _clientHandle.Free();
            Externs.Destroy(_clientPtr);
        }

        [MonoPInvokeCallback(typeof(PilgrimLocationPermissionsCallback))]
        private static void OnLocationPermissionsCallback(IntPtr clientHandlePtr, bool granted)
        {
            var client = GCHandle.FromIntPtr(clientHandlePtr).Target as PilgrimClient;
            client.OnLocationPermissionResult(granted);
        }

        [MonoPInvokeCallback(typeof(PilgrimGetCurrentLocationCallback))]
        private static void OnGetCurrentLocationCallback(IntPtr clientHandlePtr, bool success, string currentLocationJson, string errorMessage)
        {
            var client = GCHandle.FromIntPtr(clientHandlePtr).Target as PilgrimClient;
            if (success)
            {
                var currentLocation = JsonUtility.FromJson<CurrentLocation>(currentLocationJson);
                client.OnGetCurrentLocationResult(currentLocation, null);
            }
            else
            {
                client.OnGetCurrentLocationResult(null, new Exception(errorMessage));
            }
        }

        private static class Externs
        {

            [DllImport("__Internal")]
            public static extern IntPtr CreateClient(IntPtr clientHandlePtr);

            [DllImport("__Internal")]
            public static extern void SetCallbacks(IntPtr clientPtr,
                                                     PilgrimClient.PilgrimLocationPermissionsCallback locationPermissionsCallback,
                                                     PilgrimClient.PilgrimGetCurrentLocationCallback getCurrentLocationCallback);

            [DllImport("__Internal")]
            public static extern string GetUserInfo(IntPtr clientPtr);

            [DllImport("__Internal")]
            public static extern void SetUserInfo(IntPtr clientPtr, string userInfoJson, bool persisted);

            [DllImport("__Internal")]
            public static extern void RequestLocationPermissions(IntPtr clientPtr);

            [DllImport("__Internal")]
            public static extern void Start(IntPtr clientPtr);

            [DllImport("__Internal")]
            public static extern void Stop(IntPtr clientPtr);

            [DllImport("__Internal")]
            public static extern void ClearAllData(IntPtr clientPtr);

            [DllImport("__Internal")]
            public static extern void Destroy(IntPtr clientPtr);

            [DllImport("__Internal")]
            public static extern void GetCurrentLocation(IntPtr clientPtr);

            [DllImport("__Internal")]
            public static extern void ShowDebugScreen(IntPtr clientPtr);

        }

    }

}

#endif