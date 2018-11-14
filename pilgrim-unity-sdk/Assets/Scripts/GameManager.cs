using Foursquare;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

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

	void Start() 
	{
		// PilgrimUserInfo userInfo = new PilgrimUserInfo();
		// userInfo.Set("a", "b");
		// userInfo.Set("c", "d");
		// userInfo.SetBirthday(new System.DateTime(1984, 11, 26));
		// userInfo.SetGender(PilgrimUserInfo.Gender.Male);
		// userInfo.SetUserId("marchinram");
		// PilgrimUnitySDK.SetUserInfo(userInfo);

		// yield return new WaitForSeconds(10.0f);

		// userInfo = new PilgrimUserInfo();
		// userInfo.Set("a", null);
		// userInfo.Set("c", "d");
		// userInfo.SetBirthday(new System.DateTime(1984, 11, 26));
		// userInfo.SetGender(PilgrimUserInfo.Gender.Male);
		// userInfo.SetUserId(null);
		// PilgrimUnitySDK.SetUserInfo(userInfo);

		
		PilgrimUnitySDK.RequestLocationPermissions();
	}

	private void OnLocationPermissionsResult(bool granted)
	{
		if (granted) {
			// PilgrimUnitySDK.Start();
			PilgrimUnitySDK.GetCurrentLocation();
		}
	}

	private void OnGetCurrentLocationResult(bool success, CurrentLocation currentLocation)
	{
		if (success) {
			Debug.Log("CURRENT LOCATION");
			Debug.Log(currentLocation.CurrentPlace.ArrivalTime);
			Debug.Log(currentLocation.CurrentPlace.Venue.Name);
		} else {
			Debug.Log("CURRENT LOCATION FAILED");
		}
	}
	
}
