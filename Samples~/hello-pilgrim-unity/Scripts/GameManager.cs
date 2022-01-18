using Foursquare;
using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public CurrentLocationUI _currentLocationUIPrefab;

    public UserInfoUI _userInfoUIPrefab;

    public GameObject _loadingUIPrefab;

    public AlertUI _alertUIPrefab;

    public TestVisitUI _testVisitUIPrefab;

    private GameObject loadingUI;

    private enum NextAction
    {
        START,
        GET_CURRENT_LOCATION
    }

    private NextAction nextAction;

    void OnEnable()
    {
        PilgrimUnitySDK.OnLocationPermissionResult += OnLocationPermissionResult;
        PilgrimUnitySDK.OnGetCurrentLocationResult += OnGetCurrentLocationResult;
    }

    void OnDisable()
    {
        PilgrimUnitySDK.OnLocationPermissionResult -= OnLocationPermissionResult;
        PilgrimUnitySDK.OnGetCurrentLocationResult -= OnGetCurrentLocationResult;
    }

    public void OnPressStart()
    {
        nextAction = NextAction.START;
        PilgrimUnitySDK.RequestLocationPermissions();
    }

    public void OnPressStop()
    {
        PilgrimUnitySDK.Stop();
    }

    public void OnPressClearData()
    {
        PilgrimUnitySDK.ClearAllData();
    }

    public void OnPressGetCurrentLocation()
    {
        nextAction = NextAction.GET_CURRENT_LOCATION;
        PilgrimUnitySDK.RequestLocationPermissions();
    }

    public void OnPressSetUserInfo()
    {
        var canvas = GameObject.FindObjectOfType<Canvas>();
        var userInfoUI = Instantiate<UserInfoUI>(_userInfoUIPrefab, Vector3.zero, Quaternion.identity);
        userInfoUI.transform.SetParent(canvas.transform, false);
    }

    public void OnPressShowDebugScreen()
    {
        PilgrimUnitySDK.ShowDebugScreen();
    }

    public void OnPressFireTestVisit()
    {
        var canvas = GameObject.FindObjectOfType<Canvas>();
        var testVisitUI = Instantiate<TestVisitUI>(_testVisitUIPrefab, Vector3.zero, Quaternion.identity);
        testVisitUI.transform.SetParent(canvas.transform, false);
    }

    private void OnLocationPermissionResult(bool granted)
    {
        if (!granted) {
            return;
        }

        if (nextAction == NextAction.START)
        {
            PilgrimUnitySDK.Start();
        }
        else if (nextAction == NextAction.GET_CURRENT_LOCATION)
        {
            var canvas = GameObject.FindObjectOfType<Canvas>();
            loadingUI = Instantiate<GameObject>(_loadingUIPrefab, Vector3.zero, Quaternion.identity);
            loadingUI.transform.SetParent(canvas.transform, false);

            PilgrimUnitySDK.GetCurrentLocation();
        }
    }

    private void OnGetCurrentLocationResult(CurrentLocation currentLocation, Exception exception)
    {
        Destroy(loadingUI);

        var canvas = GameObject.FindObjectOfType<Canvas>();
        if (currentLocation != null)
        {
            var currentLocationUI = Instantiate<CurrentLocationUI>(_currentLocationUIPrefab, Vector3.zero, Quaternion.identity);
            currentLocationUI.transform.SetParent(canvas.transform, false);
            currentLocationUI.CurrentLocation = currentLocation;
        }
        else
        {
            var alertUI = Instantiate<AlertUI>(_alertUIPrefab, Vector3.zero, Quaternion.identity);
            alertUI.transform.SetParent(canvas.transform, false);
            alertUI.Message = exception.Message;
        }
    }

}
