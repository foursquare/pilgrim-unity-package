using Foursquare;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PilgrimCallbacks : MonoBehaviour 
{

	private PilgrimBehavior pilgrimBehavior;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		pilgrimBehavior = UnityEngine.Object.FindObjectOfType<PilgrimBehavior>();
	}

	void OnPermissionsGranted(string didGrant)
	{
		pilgrimBehavior.onLocationPermissionGranted.Invoke(didGrant == "true" ? true : false);
	}

	public void OnGeofenceEvents(string geofenceEventsJSON)
	{
		try {
			GeofenceEvent[] geofenceEvents = JsonHelper.FromJson<GeofenceEvent>(geofenceEventsJSON);
			pilgrimBehavior.onGeofenceEvents.Invoke(new List<GeofenceEvent>(geofenceEvents));
		} catch (Exception e) {
			Debug.Log("Error parsing geofence events json: " + e);
		}
	}

}
