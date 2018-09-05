using Foursquare;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EventStore 
{

	public class Event {

		public string Title { 
			get { 
				if (geofenceEvent != null) {
					return "Geofence Event";
				} else {
					return "Visit";
				}
			}
		}

		public string Description {
			get {
				if (geofenceEvent != null) {
					return geofenceEvent.EventType.ToString() + " @ " + geofenceEvent.Venue.Name;
				} else {
					return (visit.IsArrival ? "Arrival" : "Departure") + " @ " + visit.Venue.Name;
				}
			}
		}

		public DateTime Timestamp {
			get {
				if (geofenceEvent != null) {
					return geofenceEvent.Timestamp;
				} else {
					return visit.Timestamp;
				}
			}
		}

		public Category Category {
			get {
				if (geofenceEvent != null) {
					return geofenceEvent.Venue.Category;
				} else {
					return visit.Venue.Category;
				}
			}
		}

		private GeofenceEvent geofenceEvent;

		private Visit visit;

		private Event(GeofenceEvent geofenceEvent)
		{
			this.geofenceEvent = geofenceEvent;
		}

		private Event(Visit visit)
		{
			this.visit = visit;
		}

		public static implicit operator Event(GeofenceEvent geofenceEvent) 
		{
			return new Event(geofenceEvent);
		}

		public static implicit operator Event(Visit visit)
		{
			return new Event(visit);
		}

	}

	public static List<Event> GetEvents() {
		List<Event> events = new List<Event>();
		
		List<GeofenceEvent> geofenceEvents = GetGeofenceEvents();
		foreach (GeofenceEvent geofenceEvent in geofenceEvents) {
			events.Add(geofenceEvent);
		}
		
		List<Visit> visits = GetVisits();
		foreach (Visit visit in visits) {
			events.Add(visit);
		}

		return events.OrderByDescending(o => o.Timestamp).ToList();
	}

	public static void AddGeofenceEvents(List<GeofenceEvent> geofenceEvents)
	{
		List<GeofenceEvent> cachedGeofenceEvents = GetGeofenceEvents();
		try {
			cachedGeofenceEvents.AddRange(geofenceEvents);
			string geofenceEventsJson = JsonHelper.ToJson<GeofenceEvent>(cachedGeofenceEvents.ToArray());
			PlayerPrefs.SetString("geofenceEvents", geofenceEventsJson);
		} catch (Exception e) {
			Debug.Log("Error parsing geofence events json: " + e);
		}
	}

	public static List<GeofenceEvent> GetGeofenceEvents()
	{
		#if UNITY_EDITOR

		string geofenceEventsJson  = "{\"Items\":[{\"eventType\":\"entrance\",\"venueID\":\"547b8903498ef62123c41ecb\",\"categoryIDs\":[],\"chainIDs\":[],\"partnerVenueID\":\"\",\"venue\":{\"name\":\"Casey's General Store\",\"category\":{\"name\":\"Convenience Store\",\"icon\":\"https://ss3.4sqi.net/img/categories_v2/shops/conveniencestore_bg_88.png\"}},\"location\":{\"lat\":41.891381,\"lng\":-87.648111,\"hacc\":65.000000},\"timestamp\":1535219480.414939}, {\"eventType\":\"dwell\",\"venueID\":\"547b8903498ef62123c41ecb\",\"categoryIDs\":[],\"chainIDs\":[],\"partnerVenueID\":\"\",\"venue\":{\"name\":\"Casey's General Store\",\"category\":{\"name\":\"Convenience Store\",\"icon\":\"https://ss3.4sqi.net/img/categories_v2/shops/conveniencestore_bg_88.png\"}},\"location\":{\"lat\":41.891381,\"lng\":-87.648111,\"hacc\":65.000000},\"timestamp\":1535659715.414939}]}";
		GeofenceEvent[] geofenceEvents = JsonHelper.FromJson<GeofenceEvent>(geofenceEventsJson);
		return new List<GeofenceEvent>(geofenceEvents);

		#else

		string geofenceEventsJson = PlayerPrefs.GetString("geofenceEvents", "{\"Items\":[]}");
		try {
			List<GeofenceEvent> geofenceEvents = new List<GeofenceEvent>(JsonHelper.FromJson<GeofenceEvent>(geofenceEventsJson));
			return geofenceEvents;
		} catch (Exception e) {
			Debug.Log("Error parsing geofence events json: " + e);
			return new List<GeofenceEvent>();
		}

		#endif
	}

	public static void AddVisit(Visit visit)
	{
		List<Visit> cachedVisits = GetVisits();
		try {
			cachedVisits.Add(visit);
			string visitsJson = JsonHelper.ToJson<Visit>(cachedVisits.ToArray());
			PlayerPrefs.SetString("visits", visitsJson);
		} catch (Exception e) {
			Debug.Log("Error parsing visits] json: " + e);
		}
	}

	public static List<Visit> GetVisits()
	{
		#if UNITY_EDITOR

		string visitsJson = "{\"Items\":[{\"isArrival\":true,\"venue\":{\"name\":\"20 W. Kinzie Building\",\"category\":{\"name\":\"Tech Startup\",\"icon\":\"https://ss3.4sqi.net/img/categories_v2/shops/technology_88.png\"}},\"timestamp\":1535647171.074142}]}";
		Visit[] visits = JsonHelper.FromJson<Visit>(visitsJson);
		return new List<Visit>(visits);

		#else

		string visitsJson = PlayerPrefs.GetString("visits", "{\"Items\":[]}");
		try {
			List<Visit> visits = new List<Visit>(JsonHelper.FromJson<Visit>(visitsJson));
			return visits;
		} catch (Exception e) {
			Debug.Log("Error parsing visits json: " + e);
			return new List<Visit>();
		}

		#endif
	}
	
	public static void Clear()
	{
		PlayerPrefs.DeleteKey("geofenceEvents");
		PlayerPrefs.DeleteKey("visits");
	}
}
