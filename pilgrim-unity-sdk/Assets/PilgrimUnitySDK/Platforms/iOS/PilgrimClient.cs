#if UNITY_IOS

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Foursquare.iOS
{

    public class PilgrimClient : IPilgrimClient, IDisposable
    {

        public event LocationPermissionsResult OnLocationPermissionsResult;

        public event GetCurrentLocationResult OnGetCurrentLocationResult;

        internal delegate void PilgrimLocationPermissionsCallback(IntPtr clientHandlePtr, bool granted);

        internal delegate void PilgrimGetCurrentLocationCallback(IntPtr clientHandlePtr, bool success, string currentLocationJson);

        private GCHandle clientHandle;

        private IntPtr clientPtr;

        public PilgrimClient()
        {
            clientHandle = GCHandle.Alloc(this);
            clientPtr = Externs.CreateClient(GCHandle.ToIntPtr(clientHandle));
            Externs.SetCallbacks(clientPtr, OnLocationPermissionsCallback, OnGetCurrentLocationCallback);
        }

        public void SetUserInfo(PilgrimUserInfo userInfo)
        {
            var json = "{";
            foreach (var pair in userInfo.BackingStore) {
                if (json.Length > 1) {
                    json += ",";
                }
                if (pair.Value != null) {
                    json += "\"" + pair.Key + "\":\"" + pair.Value + "\"";
                }
            }
            json += "}";
            Externs.SetUserInfo(clientPtr, json);
        }

        public void RequestLocationPermissions()
        {
            Externs.RequestLocationPermissions(clientPtr);
        }

        public void Start()
        {
            Externs.Start(clientPtr);
        }

        public void Stop()
        {
            Externs.Stop(clientPtr);
        }

        public void ClearAllData()
        {
            Externs.ClearAllData(clientPtr);
        }

        public void GetCurrentLocation()
        {
            Externs.GetCurrentLocation(clientPtr);
        }

        public void Dispose()
        {
            clientHandle.Free();
            Externs.Destroy(clientPtr);
        }

        [MonoPInvokeCallback(typeof(PilgrimLocationPermissionsCallback))]
        private static void OnLocationPermissionsCallback(IntPtr clientHandlePtr, bool granted)
        {
            var client = GCHandle.FromIntPtr(clientHandlePtr).Target as PilgrimClient;
            if (client.OnLocationPermissionsResult != null) {
                client.OnLocationPermissionsResult(granted);
            }
        }

        [MonoPInvokeCallback(typeof(PilgrimGetCurrentLocationCallback))]
        private static void OnGetCurrentLocationCallback(IntPtr clientHandlePtr, bool success, string currentLocationJson)
        {
            var client = GCHandle.FromIntPtr(clientHandlePtr).Target as PilgrimClient;
            if (client.OnGetCurrentLocationResult != null) {
                if (success) {
                    var currentLocation = JsonUtility.FromJson<CurrentLocation>(currentLocationJson);
                    client.OnGetCurrentLocationResult(true, currentLocation);
                } else {
                    client.OnGetCurrentLocationResult(false, null);
                }
            }
        }

    }

}

#endif