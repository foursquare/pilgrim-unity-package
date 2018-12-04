using Foursquare;
using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{

	public CurrentLocationUI currentLocationUIPrefab;

	public UserInfoUI userInfoUIPrefab;

	

	public GameObject loadingUIPrefab;

	public AlertUI alertUIPrefab;

	private GameObject loadingUI;

	private enum NextAction
	{
		START,
		GET_CURRENT_LOCATION
	}

	private NextAction nextAction;

	void OnEnable()
	{
		PilgrimUnitySDK.OnLocationPermissionsResult += OnLocationPermissionsResult;
		PilgrimUnitySDK.OnGetCurrentLocationResult += OnGetCurrentLocationResult;
	}

	void OnDisable()
	{
		PilgrimUnitySDK.OnLocationPermissionsResult -= OnLocationPermissionsResult;
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
		var userInfoUI = Instantiate<UserInfoUI>(userInfoUIPrefab, Vector3.zero, Quaternion.identity);
		userInfoUI.transform.SetParent(canvas.transform, false);
	}

	private void OnLocationPermissionsResult(bool granted)
	{
		if (nextAction == NextAction.START) {
			if (granted) {
				PilgrimUnitySDK.Start();
			}
		} else if (nextAction == NextAction.GET_CURRENT_LOCATION) {
			var canvas = GameObject.FindObjectOfType<Canvas>();
			loadingUI = Instantiate<GameObject>(loadingUIPrefab, Vector3.zero, Quaternion.identity);
			loadingUI.transform.SetParent(canvas.transform, false);
			
			PilgrimUnitySDK.GetCurrentLocation();
		}
	}

	private void OnGetCurrentLocationResult(CurrentLocation currentLocation, Exception exception)
	{
		Destroy(loadingUI);

		var canvas = GameObject.FindObjectOfType<Canvas>();
		if (currentLocation != null) {
			var currentLocationUI = Instantiate<CurrentLocationUI>(currentLocationUIPrefab, Vector3.zero, Quaternion.identity);
			currentLocationUI.transform.SetParent(canvas.transform, false);
			currentLocationUI.CurrentLocation = currentLocation;
		} else {
			var alertUI = Instantiate<AlertUI>(alertUIPrefab, Vector3.zero, Quaternion.identity);
			alertUI.transform.SetParent(canvas.transform, false);
			alertUI.Message = exception.Message;
		}
	}
	
}
