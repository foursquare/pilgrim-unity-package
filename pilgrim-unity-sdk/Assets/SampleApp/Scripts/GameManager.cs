using Foursquare;
using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{

	public GameObject currentLocationUIPrefab;
	
	public InputField userIDInputField;
	public InputField birthdayInputField;
	public Dropdown genderDropdown;


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
		PilgrimUserInfo userInfo = new PilgrimUserInfo();
		if (userIDInputField.text != null && userIDInputField.text.Length > 0) {
			userInfo.SetUserId(userIDInputField.text);
		}
		if (birthdayInputField.text != null && birthdayInputField.text.Length > 0) {
			DateTime birthday;
			if (DateTime.TryParseExact(birthdayInputField.text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday)) {
				userInfo.SetBirthday(birthday);
			}
		}
		if (genderDropdown.value == 1) {
			userInfo.SetGender(PilgrimUserInfo.Gender.Male);	
		} else if (genderDropdown.value == 2) {
			userInfo.SetGender(PilgrimUserInfo.Gender.Female);
		}
		PilgrimUnitySDK.SetUserInfo(userInfo);
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
		if (success) {
			var canvas = GameObject.FindObjectOfType<Canvas>();
			var gameObject = Instantiate<GameObject>(currentLocationUIPrefab, Vector3.zero, Quaternion.identity);
			gameObject.transform.SetParent(canvas.transform, false);
			
			var currentLocationUI = gameObject.GetComponent<CurrentLocationUI>();
			currentLocationUI.CurrentLocation = currentLocation;
		} else {
			
		}
	}
	
}
