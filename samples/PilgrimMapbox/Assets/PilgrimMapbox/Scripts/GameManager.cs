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

    [SerializeField]
    private Button _backButton;

    private float _elevation;

    private bool _isMapInitialized;

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
        _backButton.gameObject.SetActive(false);
        _locationElements.HideArrow();

        PilgrimUnitySDK.RequestLocationPermissions();
    }

    public void OnGetLocationPermission(bool granted)
    {
        StartCoroutine(FakeLatency(() => { PilgrimUnitySDK.GetCurrentLocation(); }));
    }

    public void OnGetCurrentLocation(CurrentLocation currentLocation, Exception exception)
    {
        if (exception != null)
        {
            _locationElements.PlaceBubble.Exception = exception;
            return;
        }

        var currentPlace = currentLocation.CurrentPlace;
        var latLng = new Vector2d(currentPlace.Location.Latitude, currentPlace.Location.Longitude);
        if (currentPlace != null)
        {
            _locationElements.PlaceBubble.Venue = currentPlace.Venue;

            _map.OnInitialized += () =>
            {
                _isMapInitialized = true;
                _map.UpdateMap(latLng); // QueryElevationInUnityUnitsAt doesn't seem to work in OnInitialized
            };
            _map.OnUpdated += () =>
            {
                _elevation = _map.QueryElevationInUnityUnitsAt(_map.CenterLatitudeLongitude);
                if (Mathf.Approximately(_elevation, 0.0f))
                {
                    _map.Terrain.ElevationType = ElevationLayerType.FlatTerrain;
                }
                else
                {
                    _map.Terrain.ElevationType = ElevationLayerType.TerrainWithElevation;
                }
                GameObject.Find("FadeImage").GetComponent<FadeImage>().FadeOut();
            };

            if (!_isMapInitialized)
            {
                _map.Initialize(latLng, 17);
            }
            else
            {
                _map.UpdateMap(latLng);
            }
        }
    }

    public void FadeOutDidFinish()
    {
        var duration = _locationElements.MoveToMap(_elevation);
        StartCoroutine(SwitchToCameraExtents(duration));
    }

    public void OnPressBack()
    {
        _centerButton.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);
        _locationElements.HideArrow();

        var duration = _locationElements.MoveFromMap();
        StartCoroutine(FadeInAndGetCurrentLocation(duration));
    }

    private IEnumerator SwitchToCameraExtents(float delay)
    {
        yield return new WaitForSeconds(delay);
        _map.SetExtent(MapExtentType.CameraBounds);
        _centerButton.gameObject.SetActive(true);
        _backButton.gameObject.SetActive(true);
        _locationElements.ShowArrow();
    }

    private IEnumerator FadeInAndGetCurrentLocation(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject.Find("FadeImage").GetComponent<FadeImage>().FadeIn();
        _locationElements.PlaceBubble.Venue = null;
        StartCoroutine(FakeLatency(() => { PilgrimUnitySDK.GetCurrentLocation(); }));
    }

    private IEnumerator FakeLatency(Action action)
    {
#if UNITY_EDITOR
        yield return new WaitForSeconds(1.0f);
#else
        yield return null;
#endif
        action();
    }

}
