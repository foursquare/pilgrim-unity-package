namespace Foursquare
{
    public interface IPilgrimClient
    {

        event LocationPermissionsGranted OnLocationPermissionsGranted;

        void SetUserInfo(PilgrimUserInfo userInfo);

        void RequestLocationPermissions();
        
        void Start();

        void Stop();

        void ClearAllData();

    }

}