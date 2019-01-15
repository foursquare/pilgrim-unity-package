using Foursquare;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    private AbstractMap _map;

    [SerializeField]
    private TextMeshPro _venueNameText;

    void Start()
    {
        PilgrimUnitySDK.OnLocationPermissionResult += OnGetLocationPermission;
        PilgrimUnitySDK.RequestLocationPermissions();
    }

    public void OnGetLocationPermission(bool granted)
    {
        PilgrimUnitySDK.OnLocationPermissionResult -= OnGetLocationPermission;
        PilgrimUnitySDK.OnGetCurrentLocationResult += OnGetCurrentLocation;
        PilgrimUnitySDK.GetCurrentLocation();
    }

    public void OnGetCurrentLocation(CurrentLocation currentLocation, Exception exception)
    {
        PilgrimUnitySDK.OnGetCurrentLocationResult -= OnGetCurrentLocation;
        
        var currentPlace = currentLocation.CurrentPlace;
        if (currentPlace != null) {
            _venueNameText.text = currentLocation.CurrentPlace.Venue.Name;
            _map.Initialize(new Vector2d(currentPlace.Location.Latitude, currentPlace.Location.Longitude), 17);
        }
    }

}
