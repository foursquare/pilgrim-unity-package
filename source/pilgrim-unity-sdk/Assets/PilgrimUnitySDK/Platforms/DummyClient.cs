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
                string dummyJson = "{\"currentPlace\":{\"arrivalTime\":1542391398.094413,\"location\":{\"longitude\":-87.62864398628912,\"latitude\":41.889305898874632},\"venue\":{\"id\":\"52af211911d2aa9d4a1f0e0a\",\"name\":\"Foursquare Chicago\",\"location\":{\"address\":\"20 W Kinzie St\",\"city\":\"Chicago\",\"country\":\"US\",\"postalCode\":\"60606\",\"crossStreet\":\"at N State St\",\"state\":\"IL\",\"coordinate\":{\"longitude\":-87.628911000000002,\"latitude\":41.889263999999997}}}},\"matchedGeofences\":[{\"venue\":{\"id\":\"52af211911d2aa9d4a1f0e0a\",\"name\":\"Foursquare Chicago\",\"location\":{\"address\":\"20 W Kinzie St\",\"city\":\"Chicago\",\"country\":\"US\",\"postalCode\":\"60606\",\"crossStreet\":\"at N State St\",\"state\":\"IL\",\"coordinate\":{\"longitude\":-87.628911000000002,\"latitude\":41.889263999999997}}},\"location\":{\"longitude\":-87.62864398628912,\"latitude\":41.889305898874632},\"timestamp\":1542391398.094413}]}";
                var dummyLocation = JsonUtility.FromJson<CurrentLocation>(dummyJson);
                OnGetCurrentLocationResult(dummyLocation, null);
            }
        }
        
    }

}