using System;

namespace Foursquare
{

    public static class PilgrimUnitySDK
    {

        public static event Action<bool> OnLocationPermissionResult
        {
            add
            {
                _client.OnLocationPermissionResult += value;
            }
            remove
            {
                _client.OnLocationPermissionResult -= value;
            }
        }

        public static event Action OnLocationPermissionShowRationale
        {
            add
            {
                _client.OnLocationPermissionShowRationale += value;
            }
            remove
            {
                _client.OnLocationPermissionShowRationale -= value;
            }
        }

        public static event Action<CurrentLocation, Exception> OnGetCurrentLocationResult
        {
            add
            {
                _client.OnGetCurrentLocationResult += value;
            }
            remove
            {
                _client.OnGetCurrentLocationResult -= value;
            }
        }

        private static IPilgrimClient _client = PilgrimClientFactory.PilgrimClient();

        public static UserInfo GetUserInfo()
        {
            return _client.GetUserInfo();
        }

        public static void SetUserInfo(UserInfo userInfo, bool persisted = true)
        {
            _client.SetUserInfo(userInfo, persisted);
        }

        public static void RequestLocationPermissions()
        {
            _client.RequestLocationPermissions();
        }

        public static void Start()
        {
            _client.Start();
        }

        public static void Stop()
        {
            _client.Stop();
        }

        public static void ClearAllData()
        {
            _client.ClearAllData();
        }

        public static void GetCurrentLocation()
        {
            _client.GetCurrentLocation();
        }

        public static void ShowDebugScreen()
        {
            _client.ShowDebugScreen();
        }

        public static void FireTestVisit(Location location)
        {
            _client.FireTestVisit(location);
        }

    }

}
