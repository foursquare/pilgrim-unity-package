using System;

namespace Foursquare
{
    public interface IPilgrimClient
    {

        event Action<bool> OnLocationPermissionResult;

        event Action<CurrentLocation, Exception> OnGetCurrentLocationResult;

        void SetUserInfo(UserInfo userInfo, bool persisted);

        void RequestLocationPermissions();
        
        void Start();

        void Stop();

        void ClearAllData();

        void GetCurrentLocation();

    }

}