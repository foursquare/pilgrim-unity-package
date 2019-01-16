using Foursquare;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private AbstractMap _map;

    [SerializeField]
    private LocationElements _locationElements;

    [SerializeField]
    private Button _centerButton;

    private float _elevation;

    void OnEnable()
    {
        PilgrimUnitySDK.OnLocationPermissionResult += OnGetLocationPermission;
        PilgrimUnitySDK.OnGetCurrentLocationResult += OnGetCurrentLocation;
    }

    void OnDisable()
    {
        PilgrimUnitySDK.OnLocationPermissionResult -= OnGetLocationPermission;
        PilgrimUnitySDK.OnGetCurrentLocationResult -= OnGetCurrentLocation;
    }

    void Start()
    {
        _centerButton.gameObject.SetActive(false);
        PilgrimUnitySDK.RequestLocationPermissions();
    }

    public void OnGetLocationPermission(bool granted)
    {
        PilgrimUnitySDK.GetCurrentLocation();
    }

    public void OnGetCurrentLocation(CurrentLocation currentLocation, Exception exception)
    {
        var currentPlace = currentLocation.CurrentPlace;
        var latLng = new Vector2d(currentPlace.Location.Latitude, currentPlace.Location.Longitude);
        if (currentPlace != null)
        {
            _locationElements.PlaceBubble.Venue = currentPlace.Venue;

            _map.OnInitialized += () =>
            {
                _map.UpdateMap(latLng); // QueryElevationInUnityUnitsAt doesn't seem to work in OnInitialized
            };
            _map.OnUpdated += () =>
            {
                _elevation = _map.QueryElevationInUnityUnitsAt(_map.CenterLatitudeLongitude);
                if (_elevation == 0)
                {
                    _map.Terrain.ElevationType = ElevationLayerType.FlatTerrain;
                }
                GameObject.Find("FadeImage").GetComponent<FadeImage>().FadeOut();
            };
            _map.Initialize(latLng, 17);
        }
    }

    public void FadeOutDidFinish()
    {
        var duration = _locationElements.Move(_elevation);
        StartCoroutine(SwitchToCameraExtents(duration));
    }

    private IEnumerator SwitchToCameraExtents(float delay)
    {
        yield return new WaitForSeconds(delay);
        _map.SetExtent(MapExtentType.CameraBounds);
        _centerButton.gameObject.SetActive(true);
    }

}
