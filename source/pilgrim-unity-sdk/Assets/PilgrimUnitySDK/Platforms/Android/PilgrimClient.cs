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

        public UserInfo GetUserInfo()
        {
            var userInfoJson = _androidPilgrimClient.Call<string>("getUserInfo");
            return JsonUtility.FromJson<UserInfo>(userInfoJson);
        }

        public void SetUserInfo(UserInfo userInfo, bool persisted)
        {
            var userInfoJson = JsonUtility.ToJson(userInfo);
            _androidPilgrimClient.Call("setUserInfo", userInfoJson, persisted);
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

        public void ShowDebugScreen()
        {
            _androidPilgrimClient.Call("showDebugScreen");
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