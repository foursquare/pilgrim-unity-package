using Foursquare;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    private AbstractMap _map;

    void Start()
    {
        PilgrimUnitySDK.OnLocationPermissionResult += OnGetLocationPermission;
        PilgrimUnitySDK.RequestLocationPermissions();
    }

    void OnGetLocationPermission(bool granted)
    {
        PilgrimUnitySDK.OnLocationPermissionResult -= OnGetLocationPermission;
        PilgrimUnitySDK.OnGetCurrentLocationResult += OnGetCurrentLocation;
        PilgrimUnitySDK.GetCurrentLocation();
    }

    void OnGetCurrentLocation(CurrentLocation currentLocation, Exception exception)
    {
        Debug.Log(currentLocation.CurrentPlace == null);
        Debug.Log(exception);
        if (currentLocation.CurrentPlace != null) {
            Debug.Log(currentLocation.CurrentPlace.Venue.Name);
        }

        PilgrimUnitySDK.OnGetCurrentLocationResult -= OnGetCurrentLocation;
        
        var currentPlace = currentLocation.CurrentPlace;
        if (currentPlace != null) {
            _map.Initialize(new Vector2d(currentPlace.Location.Latitude, currentPlace.Location.Longitude), 17);
        }
    }

}
