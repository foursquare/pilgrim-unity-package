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

	#if UNITY_EDITOR
	public static List<GeofenceEvent> GetGeofenceEvents()
	{
		string geofenceEventsJSON  = "{\"Items\":[{\"eventType\":\"dwell\",\"venueID\":\"547b8903498ef62123c41ecb\",\"categoryIDs\":[],\"chainIDs\":[],\"partnerVenueID\":\"\",\"venue\":{\"name\":\"Casey's General Store\",\"location\":{\"lat\":41.891381,\"lng\":-87.648111,\"hacc\":65.000000}},\"timestamp\":1535219480.414939}]}";
		GeofenceEvent[] geofenceEvents = JsonHelper.FromJson<GeofenceEvent>(geofenceEventsJSON);
		return new List<GeofenceEvent>(geofenceEvents);
	}
	#else
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
	#endif
	
	public static void Clear()
	{
		PlayerPrefs.DeleteKey("geofenceEvents");
	}
}
