#if UNITY_IOS

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Foursquare.iOS
{

    public class PilgrimClient : IPilgrimClient, IDisposable
    {

        public event Action<bool> OnLocationPermissionResult = delegate {};

        public event Action<CurrentLocation, Exception> OnGetCurrentLocationResult = delegate {};

        internal delegate void PilgrimLocationPermissionsCallback(IntPtr clientHandlePtr, bool granted);

        internal delegate void PilgrimGetCurrentLocationCallback(IntPtr clientHandlePtr, bool success, string currentLocationJson, string errorMessage);

        private GCHandle _clientHandle;

        private IntPtr _clientPtr;

        public PilgrimClient()
        {
            _clientHandle = GCHandle.Alloc(this);
            _clientPtr = Externs.CreateClient(GCHandle.ToIntPtr(_clientHandle));
            Externs.SetCallbacks(_clientPtr, OnLocationPermissionsCallback, OnGetCurrentLocationCallback);
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
            Externs.SetUserInfo(_clientPtr, json);
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
            if (success) {
                var currentLocation = JsonUtility.FromJson<CurrentLocation>(currentLocationJson);
                client.OnGetCurrentLocationResult(currentLocation, null);
            } else {
                client.OnGetCurrentLocationResult(null, new Exception(errorMessage));
            }
        }

    }

}

#endif