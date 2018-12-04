using Foursquare;
using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{

	public GameObject currentLocationUIPrefab;

	public GameObject userInfoUIPrefab;

	public GameObject loadingUIPrefab;

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
		var gameObject = Instantiate<GameObject>(userInfoUIPrefab, Vector3.zero, Quaternion.identity);
		gameObject.transform.SetParent(canvas.transform, false);
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

		if (currentLocation != null) {
			var canvas = GameObject.FindObjectOfType<Canvas>();
			var gameObject = Instantiate<GameObject>(currentLocationUIPrefab, Vector3.zero, Quaternion.identity);
			gameObject.transform.SetParent(canvas.transform, false);
			
			var currentLocationUI = gameObject.GetComponent<CurrentLocationUI>();
			currentLocationUI.CurrentLocation = currentLocation;
		} else {
			Debug.LogError(exception.Message);
		}
	}
	
}
