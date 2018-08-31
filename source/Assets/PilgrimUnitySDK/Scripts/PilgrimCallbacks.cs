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

	public void OnGeofenceEvents(string geofenceEventsJson)
	{
		try {
			GeofenceEvent[] geofenceEvents = JsonHelper.FromJson<GeofenceEvent>(geofenceEventsJson);
			pilgrimBehavior.onGeofenceEvents.Invoke(new List<GeofenceEvent>(geofenceEvents));
		} catch (Exception e) {
			Debug.Log("Error parsing geofence events json: " + e);
		}
	}

	public void OnVisit(string visitJson)
	{
		try {
			Visit visit = JsonUtility.FromJson<Visit>(visitJson);
			pilgrimBehavior.onVisit.Invoke(visit);
		} catch (Exception e) {
			Debug.Log("Error parsing visit json: " + e);
		}
	}

}
