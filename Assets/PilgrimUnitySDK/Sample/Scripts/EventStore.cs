using Foursquare;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventStore 
{

	public static void AddGeofenceEvents(List<GeofenceEvent> geofenceEvents)
	{
		string geofenceEventsJSON = PlayerPrefs.GetString("geofenceEvents", "{\"Items\":[]}");
		try {
			List<GeofenceEvent> cachedGeofenceEvents = new List<GeofenceEvent>(JsonHelper.FromJson<GeofenceEvent>(geofenceEventsJSON));
			cachedGeofenceEvents.AddRange(geofenceEvents);
			geofenceEventsJSON = JsonHelper.ToJson<GeofenceEvent>(cachedGeofenceEvents.ToArray());
			PlayerPrefs.SetString("geofenceEvents", geofenceEventsJSON);
		} catch (Exception e) {
			Debug.Log("Error parsing geofence events json: " + e);
		}
	}

	public static List<GeofenceEvent> GetGeofenceEvents()
	{
		string geofenceEventsJSON = PlayerPrefs.GetString("geofenceEvents", "{\"Items\":[]}");
		try {
			List<GeofenceEvent> geofenceEvents = new List<GeofenceEvent>(JsonHelper.FromJson<GeofenceEvent>(geofenceEventsJSON));
			return geofenceEvents;
		} catch (Exception e) {
			Debug.Log("Error parsing geofence events json: " + e);
			return new List<GeofenceEvent>();
		}
	}
	
	public static void Clear()
	{
		PlayerPrefs.DeleteKey("geofenceEvents");
	}
}
