using Foursquare;
using System.Collections.Generic;
using UnityEngine;

public class PilgrimCallbacks : MonoBehaviour 
{

	private PilgrimBehavior pilgrimBehavior;

	void Awake()
	{
		pilgrimBehavior = Object.FindObjectOfType<PilgrimBehavior>();
	}

	void OnPermissionsGranted(string didGrant)
	{
		pilgrimBehavior.onPermissionsGranted.Invoke(didGrant == "true" ? true : false);
	}

	public void OnGeofenceEvents(string geofenceEventsJSON)
	{
		GeofenceEvent[] geofenceEvents = JsonHelper.FromJson<GeofenceEvent>(geofenceEventsJSON);
		pilgrimBehavior.onGeofenceEvents.Invoke(new List<GeofenceEvent>(geofenceEvents));
	}

}
