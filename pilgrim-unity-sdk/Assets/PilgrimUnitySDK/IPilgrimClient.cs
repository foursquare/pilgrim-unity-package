
namespace Foursquare
{
    public interface IPilgrimClient
    {

        event OnLocationPermissionsGranted OnLocationPermissionsGranted;

        void SetUserInfo(PilgrimUserInfo userInfo);

        void RequestLocationPermissions();
        
        void Start();

        void Stop();

        void ClearAllData();

    }

}