using Foursquare;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using System;
using UnityEngine;

public class GetCurrentLocationProvider : AbstractLocationProvider
{

    void OnEnable()
    {
        PilgrimUnitySDK.OnGetCurrentLocationResult += OnGetCurrentLocationResult;
    }

    void OnDisable()
    {
        PilgrimUnitySDK.OnGetCurrentLocationResult -= OnGetCurrentLocationResult;
    }

    private void OnGetCurrentLocationResult(CurrentLocation currentLocation, Exception exception)
    {
        if (exception != null)
        {
            Debug.LogError(exception);
            return;
        }

        var currentPlace = currentLocation.CurrentPlace;
        SendLocation(new Mapbox.Unity.Location.Location()
        {
            LatitudeLongitude = new Vector2d(currentPlace.Location.Latitude, currentPlace.Location.Longitude),
            IsLocationUpdated = true
        });
    }

}