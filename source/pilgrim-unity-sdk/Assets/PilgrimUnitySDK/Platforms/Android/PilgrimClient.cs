#if UNITY_ANDROID

using System;
using UnityEngine;

namespace Foursquare.Android
{

    public class PilgrimClient : AndroidJavaProxy, IPilgrimClient, IDisposable
    {

        public event Action<bool> OnLocationPermissionResult = delegate { };

        public event Action<CurrentLocation, Exception> OnGetCurrentLocationResult = delegate { };

        private AndroidJavaObject _androidPilgrimClient;

        public PilgrimClient() : base("com.foursquare.pilgrimunitysdk.PilgrimClientListener")
        {
            using (var playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                _androidPilgrimClient = new AndroidJavaObject("com.foursquare.pilgrimunitysdk.PilgrimClient", activity, this);
            }
        }

        public void SetUserInfo(UserInfo userInfo)
        {
            using (var userInfoMap = new AndroidJavaObject("java.util.HashMap"))
            {
                var putMethod = AndroidJNIHelper.GetMethodID(userInfoMap.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
                object[] args = new object[2];
                foreach (var pair in userInfo.BackingStore)
                {
                    if (pair.Value == null)
                    {
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

                _androidPilgrimClient.Call("setUserInfo", userInfoMap);
            }
        }

        public void RequestLocationPermissions()
        {
            _androidPilgrimClient.Call("requestLocationPermissions");
        }

        public void Start()
        {
            _androidPilgrimClient.Call("start");
        }

        public void Stop()
        {
            _androidPilgrimClient.Call("stop");
        }

        public void ClearAllData()
        {
            _androidPilgrimClient.Call("clearAllData");
        }

        public void GetCurrentLocation()
        {
            _androidPilgrimClient.Call("getCurrentLocation");
        }

        public void Dispose()
        {
            _androidPilgrimClient.Dispose();
        }

        public void onLocationPermissionResult(bool granted)
        {
            OnLocationPermissionResult(granted);
        }

        public void onGetCurrentLocationResult(bool success, string currentLocationJson, string errorMessage)
        {
            if (success)
            {
                var currentLocation = JsonUtility.FromJson<CurrentLocation>(currentLocationJson);
                OnGetCurrentLocationResult(currentLocation, null);
            }
            else
            {
                OnGetCurrentLocationResult(null, new Exception(errorMessage));
            }
        }

    }

}

#endif