using UnityEngine;

namespace Foursquare
{

    public class DummyClient : IPilgrimClient
    {

        public event LocationPermissionsResult OnLocationPermissionsResult;

        public event GetCurrentLocationResult OnGetCurrentLocationResult;

        public void SetUserInfo(PilgrimUserInfo userInfo)
        {

        }

        public void RequestLocationPermissions()
        {
            if (OnLocationPermissionsResult != null) {
                OnLocationPermissionsResult(true);
            }
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }

        public void ClearAllData()
        {
            
        }

        public void GetCurrentLocation()
        {
            if (OnGetCurrentLocationResult != null) {
                string dummyJson = "{\"currentPlace\":{\"arrivalTime\":1542238989,\"location\":{\"longitude\":-87.628565102669072,\"latitude\":41.889316274046827},\"venue\":{\"name\":\"Foursquare Chicago\"}},\"matchedGeofences\":[{\"venue\":{\"name\":\"Foursquare Chicago\"}}]}";
                var dummyLocation = JsonUtility.FromJson<CurrentLocation>(dummyJson);
                OnGetCurrentLocationResult(true, dummyLocation);
            }
        }
        
    }

}