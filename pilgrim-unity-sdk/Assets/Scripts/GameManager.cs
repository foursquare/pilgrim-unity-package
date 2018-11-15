using Foursquare;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

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
	private void OnLocationPermissionsResult(bool granted)
	{
		if (granted) {
			if (nextAction == NextAction.START) {
				PilgrimUnitySDK.Start();
			} else if (nextAction == NextAction.GET_CURRENT_LOCATION) {
				PilgrimUnitySDK.GetCurrentLocation();
			}
		}
	}

	private void OnGetCurrentLocationResult(bool success, CurrentLocation currentLocation)
	{
		
	}
	
}
