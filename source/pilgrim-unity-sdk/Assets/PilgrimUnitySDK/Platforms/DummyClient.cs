using System;
using UnityEngine;

namespace Foursquare
{

    public class DummyClient : IPilgrimClient
    {

        public event Action<bool> OnLocationPermissionResult = delegate { };

        public event Action<CurrentLocation, Exception> OnGetCurrentLocationResult = delegate { };

        public void SetUserInfo(UserInfo userInfo)
        {

        }

        public void RequestLocationPermissions()
        {
            OnLocationPermissionResult(true);
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
            string dummyJson = "{\"currentPlace\":{\"venue\":{\"chains\":[{\"id\":\"556e5779bd6a82902e28bcea\",\"name\":\"Foursquare\"}],\"locationInformation\":{\"location\":{\"longitude\":-87.628911000000002,\"latitude\":41.889263999999997},\"city\":\"Chicago\",\"country\":\"US\",\"postalCode\":\"60606\",\"crossStreet\":\"at N State St\",\"address\":\"20 W Kinzie St\",\"state\":\"IL\"},\"id\":\"52af211911d2aa9d4a1f0e0a\",\"probability\":0.47794239984996967,\"categories\":[{\"id\":\"4bf58dd8d48988d125941735\",\"pluralName\":\"Tech Startups\",\"icon\":{\"prefix\":\"https://ss3.4sqi.net/img/categories_v2/shops/technology_\",\"suffix\":\".png\"},\"name\":\"Tech Startup\",\"shortName\":\"Tech Startup\",\"isPrimary\":true}],\"name\":\"Foursquare Chicago\",\"hierarchy\":[]},\"locationType\":2,\"confidence\":3,\"arrivalTime\":1547497356.99088,\"location\":{\"longitude\":-87.628673265377884,\"latitude\":41.889312744140625}},\"matchedGeofences\":[{\"venueId\":\"4a9037fef964a5209b1620e3\",\"venue\":{\"locationInformation\":{\"location\":{\"longitude\":-87.629789599999995,\"latitude\":41.889389700000002},\"city\":\"Chicago\",\"country\":\"US\",\"postalCode\":\"60654\",\"crossStreet\":\"\",\"address\":\"400 N Dearborn St\",\"state\":\"IL\"},\"id\":\"4a9037fef964a5209b1620e3\",\"hierarchy\":[],\"categories\":[{\"id\":\"4bf58dd8d48988d179941735\",\"pluralName\":\"Bagel Shops\",\"icon\":{\"prefix\":\"https://ss3.4sqi.net/img/categories_v2/food/bagels_\",\"suffix\":\".png\"},\"name\":\"Bagel Shop\",\"shortName\":\"Bagels\",\"isPrimary\":true},{\"id\":\"4bf58dd8d48988d1e0931735\",\"pluralName\":\"Coffee Shops\",\"icon\":{\"prefix\":\"https://ss3.4sqi.net/img/categories_v2/food/coffeeshop_\",\"suffix\":\".png\"},\"name\":\"Coffee Shop\",\"shortName\":\"Coffee Shop\",\"isPrimary\":false}],\"name\":\"Einstein Bros Bagels\",\"chains\":[]},\"timestamp\":1547497356.99088,\"categoryIds\":[\"4bf58dd8d48988d1e0931735\"],\"location\":{\"longitude\":-87.628673265377884,\"latitude\":41.889312744140625},\"chainIds\":[]},{\"venueId\":\"52af211911d2aa9d4a1f0e0a\",\"venue\":{\"locationInformation\":{\"location\":{\"longitude\":-87.628911000000002,\"latitude\":41.889263999999997},\"city\":\"Chicago\",\"country\":\"US\",\"postalCode\":\"60606\",\"crossStreet\":\"at N State St\",\"address\":\"20 W Kinzie St\",\"state\":\"IL\"},\"id\":\"52af211911d2aa9d4a1f0e0a\",\"hierarchy\":[],\"categories\":[{\"id\":\"4bf58dd8d48988d125941735\",\"pluralName\":\"Tech Startups\",\"icon\":{\"prefix\":\"https://ss3.4sqi.net/img/categories_v2/shops/technology_\",\"suffix\":\".png\"},\"name\":\"Tech Startup\",\"shortName\":\"Tech Startup\",\"isPrimary\":true}],\"name\":\"Foursquare Chicago\",\"chains\":[]},\"timestamp\":1547497356.99088,\"categoryIds\":[],\"location\":{\"longitude\":-87.628673265377884,\"latitude\":41.889312744140625},\"chainIds\":[]}]}";
            var dummyLocation = JsonUtility.FromJson<CurrentLocation>(dummyJson);
            OnGetCurrentLocationResult(dummyLocation, null);
        }

    }

}