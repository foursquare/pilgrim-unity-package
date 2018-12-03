namespace Foursquare
{
    public interface IPilgrimClient
    {

        event LocationPermissionsResult OnLocationPermissionsResult;

        event GetCurrentLocationResult OnGetCurrentLocationResult;

        void SetUserInfo(PilgrimUserInfo userInfo);

        void RequestLocationPermissions();
        
        void Start();

        void Stop();

        void ClearAllData();

        void GetCurrentLocation();

    }

}