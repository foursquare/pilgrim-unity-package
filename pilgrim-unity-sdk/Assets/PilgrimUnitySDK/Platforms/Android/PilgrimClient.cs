#if UNITY_ANDROID

using System;
using UnityEngine;

namespace Foursquare.Android
{

    public class PilgrimClient : AndroidJavaProxy, IPilgrimClient, IDisposable
    {

        public event LocationPermissionsResult OnLocationPermissionsResult;

        public event GetCurrentLocationResult OnGetCurrentLocationResult;

        private AndroidJavaObject pilgrimClient;

        public PilgrimClient() : base("com.foursquare.pilgrimunitysdk.PilgrimClientListener")
        {
            using (var playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                pilgrimClient = new AndroidJavaObject("com.foursquare.pilgrimunitysdk.PilgrimClient", activity, this);
            }
        }

        public void SetUserInfo(PilgrimUserInfo userInfo)
        {
            using (var userInfoMap = new AndroidJavaObject("java.util.HashMap"))
            {
                var putMethod = AndroidJNIHelper.GetMethodID(userInfoMap.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
                object[] args = new object[2];
                foreach (var pair in userInfo.BackingStore) {
                    if (pair.Value == null) {
                        continue;
                    }
                    using (AndroidJavaObject k = new AndroidJavaObject("java.lang.String", pair.Key))
                    using (AndroidJavaObject v = new AndroidJavaObject("java.lang.String", pair.Value)) 
                    {
                        args[0] = k;
                        args[1] = v;
                        AndroidJNI.CallObjectMethod(userInfoMap.GetRawObject(), putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
                    }
                }

                pilgrimClient.Call("setUserInfo", userInfoMap);
            }
        }

        public void RequestLocationPermissions()
        {
            pilgrimClient.Call("requestLocationPermissions");
        }

        public void Start()
        {
            pilgrimClient.Call("start");
        }

        public void Stop()
        {
            pilgrimClient.Call("stop");
        }

        public void ClearAllData()
        {
            pilgrimClient.Call("clearAllData");
        }

        public void GetCurrentLocation()
        {
            pilgrimClient.Call("getCurrentLocation");
        }

        public void Dispose()
        {
            pilgrimClient.Dispose();
        }

        public void onLocationPermissionResult(bool granted) {
            if (OnLocationPermissionsResult != null) {
                OnLocationPermissionsResult(granted);
            }
        }

        public void onGetCurrentLocationResult(bool success, String currentLocationJson) {
            if (OnGetCurrentLocationResult != null) {
                if (success) {
                    var currentLocation = JsonUtility.FromJson<CurrentLocation>(currentLocationJson);
                    OnGetCurrentLocationResult(true, currentLocation);
                } else {
                    OnGetCurrentLocationResult(false, null);
                }
            }
        }

    }

}

#endif