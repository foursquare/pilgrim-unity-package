using Foursquare;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public AbstractMap _map;

    public LocationElements _locationElements;

    public Button _centerButton;

    public Button _backButton;

    private float _elevation;

    private bool _isMapInitialized;

    void Start()
    {
        _centerButton.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);
        _locationElements.HideArrow();

        _map.OnInitialized += OnMapUpdate;
        _map.OnUpdated += OnMapUpdate;

        PilgrimUnitySDK.OnGetCurrentLocationResult += (currentLocation, exception) =>
        {
            if (exception != null)
            {
                _locationElements._placeBubble.Exception = exception;
            }
            else
            {
                _locationElements._placeBubble.Venue = currentLocation.CurrentPlace.Venue;
            }

        };
        PilgrimUnitySDK.OnLocationPermissionResult += (granted) =>
        {
            if (granted)
            {
                StartCoroutine(DelayAndRun(() => { PilgrimUnitySDK.GetCurrentLocation(); }));
            }
        };
        PilgrimUnitySDK.RequestLocationPermissions();
    }

    public void FadeOutDidFinish()
    {
        var duration = _locationElements.MoveToMap(_elevation);
        StartCoroutine(AnimationFinished(duration));
    }

    public void OnPressBack()
    {
        _centerButton.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);
        _locationElements.HideArrow();

        var duration = _locationElements.MoveFromMap();
        StartCoroutine(FadeInAndGetCurrentLocation(duration));
    }

    private IEnumerator AnimationFinished(float delay)
    {
        yield return new WaitForSeconds(delay);
        _centerButton.gameObject.SetActive(true);
        _backButton.gameObject.SetActive(true);
        _locationElements.ShowArrow();
        Camera.main.GetComponent<CameraDrag>().DragEnabled = true;
    }

    private IEnumerator FadeInAndGetCurrentLocation(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject.Find("FadeImage").GetComponent<FadeImage>().FadeIn();
        _locationElements._placeBubble.Venue = null;
        Camera.main.GetComponent<CameraDrag>().DragEnabled = false;
        StartCoroutine(DelayAndRun(() => { PilgrimUnitySDK.GetCurrentLocation(); }));
    }

    private void OnMapUpdate()
    {
        GameObject.Find("FadeImage").GetComponent<FadeImage>().FadeOut();

        _map.Terrain.SetElevationType(ElevationLayerType.TerrainWithElevation);
        _elevation = _map.QueryElevationInUnityUnitsAt(_map.CenterLatitudeLongitude);

        if (Mathf.Approximately(_elevation, 0.0f))
        {
            _map.Terrain.SetElevationType(ElevationLayerType.FlatTerrain);
        }
        else
        {
            _map.Terrain.SetElevationType(ElevationLayerType.TerrainWithElevation);
        }
    }

    private IEnumerator DelayAndRun(Action action)
    {
        yield return new WaitForSeconds(0.5f);
        action();
    }

}
