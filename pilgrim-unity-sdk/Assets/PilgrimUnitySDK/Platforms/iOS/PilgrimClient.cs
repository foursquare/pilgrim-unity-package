#if UNITY_IOS

using System;
using System.Runtime.InteropServices;

namespace Foursquare.iOS
{

    public class PilgrimClient : IPilgrimClient, IDisposable
    {

        public event OnLocationPermissionsGranted OnLocationPermissionsGranted;

        public delegate void PilgrimLocationPermissionsCallback(IntPtr clientPtr, bool granted);

        private GCHandle clientHandle;

        private IntPtr clientPtr;

        public PilgrimClient()
        {
            clientHandle = GCHandle.Alloc(this);

            clientPtr = Externs.CreateClient(GCHandle.ToIntPtr(clientHandle));
            Externs.SetCallbacks(clientPtr, OnLocationPermissionsCallback);
        }

        public void SetUserInfo(PilgrimUserInfo userInfo)
        {
            var json = "{";
            foreach (var pair in userInfo.BackingStore) {
                if (pair.Value != null) {
                    json += "\"" + pair.Key + "\":\"" + pair.Value + "\",";
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

        public void Dispose()
        {
            clientHandle.Free();
            Externs.Destroy(clientPtr);
        }

        [MonoPInvokeCallback(typeof(PilgrimLocationPermissionsCallback))]
        private static void OnLocationPermissionsCallback(IntPtr clientHandlePtr, bool granted)
        {
            var client = GCHandle.FromIntPtr(clientHandlePtr).Target as PilgrimClient;
            if (client.OnLocationPermissionsGranted != null) {
                client.OnLocationPermissionsGranted(granted);
            }
        }

    }

}

#endif