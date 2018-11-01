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

		public GeofenceEvent GeofenceEvent {
			get {
				return geofenceEvent;
			}
		}

		private Visit visit;

		public Visit Visit {
			get {
				return visit;
			}
		}

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

	private static List<GeofenceEvent> cachedGeofenceEvents;

	private static List<Visit> cachedVisits;

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

		return events.OrderBy(o => o.Timestamp).ToList();
	}

	public static void AddGeofenceEvents(List<GeofenceEvent> geofenceEvents)
	{
		if (cachedGeofenceEvents == null) {
			cachedGeofenceEvents = GetGeofenceEvents();
		}
		try {
			cachedGeofenceEvents.AddRange(geofenceEvents);
			string geofenceEventsJson = JsonHelper.ToJson<GeofenceEvent>(cachedGeofenceEvents.ToArray());
			PlayerPrefs.SetString("geofenceEvents", geofenceEventsJson);
		} catch (Exception e) {
			Debug.Log("Error parsing geofence events json: " + e);
		}
	}

	private static List<GeofenceEvent> GetGeofenceEvents()
	{
		if (cachedGeofenceEvents == null) {
			string geofenceEventsJson = PlayerPrefs.GetString("geofenceEvents", "{\"Items\":[]}");
			try {
				cachedGeofenceEvents = new List<GeofenceEvent>(JsonHelper.FromJson<GeofenceEvent>(geofenceEventsJson));
			} catch (Exception e) {
				Debug.Log("Error parsing geofence events json: " + e);
			}
		}
		return cachedGeofenceEvents;
	}

	public static void AddVisit(Visit visit)
	{
		if (cachedVisits == null) {
			cachedVisits = GetVisits();
		}
		try {
			cachedVisits.Add(visit);
			string visitsJson = JsonHelper.ToJson<Visit>(cachedVisits.ToArray());
			PlayerPrefs.SetString("visits", visitsJson);
		} catch (Exception e) {
			Debug.Log("Error parsing visits json: " + e);
		}
	}

	private static List<Visit> GetVisits()
	{
		if (cachedVisits == null) {
			string visitsJson = PlayerPrefs.GetString("visits", "{\"Items\":[]}");
			try {
				cachedVisits = new List<Visit>(JsonHelper.FromJson<Visit>(visitsJson));
			} catch (Exception e) {
				Debug.Log("Error parsing visits json: " + e);
			}
		}
		return cachedVisits;
	}
	
	public static void Clear()
	{
		PlayerPrefs.DeleteKey("geofenceEvents");
		cachedGeofenceEvents = null;
		PlayerPrefs.DeleteKey("visits");
		cachedVisits = null;
	}

	#if UNITY_EDITOR

	public static void AddSampleEvents()
	{
		string visitsJson = "{\"Items\":[{\"isArrival\":true,\"venue\":{\"name\":\"20 W. Kinzie Building\",\"category\":{\"name\":\"Tech Startup\",\"icon\":\"https://ss3.4sqi.net/img/categories_v2/shops/technology_88.png\"}},\"timestamp\":1535647171.074142}]}";	
		Visit[] visits = JsonHelper.FromJson<Visit>(visitsJson);
		foreach (Visit visit in visits) {
			AddVisit(visit);
		}

		string geofenceEventsJson  = "{\"Items\":[{\"eventType\":\"entrance\",\"venueID\":\"547b8903498ef62123c41ecb\",\"categoryIDs\":[],\"chainIDs\":[],\"partnerVenueID\":\"\",\"venue\":{\"name\":\"Casey's General Store\",\"category\":{\"name\":\"Convenience Store\",\"icon\":\"https://ss3.4sqi.net/img/categories_v2/shops/conveniencestore_bg_88.png\"}},\"location\":{\"lat\":41.891381,\"lng\":-87.648111,\"hacc\":65.000000},\"timestamp\":1535219480.414939}, {\"eventType\":\"dwell\",\"venueID\":\"547b8903498ef62123c41ecb\",\"categoryIDs\":[],\"chainIDs\":[],\"partnerVenueID\":\"\",\"venue\":{\"name\":\"Casey's General Store\",\"category\":{\"name\":\"Convenience Store\",\"icon\":\"https://ss3.4sqi.net/img/categories_v2/shops/conveniencestore_bg_88.png\"}},\"location\":{\"lat\":41.891381,\"lng\":-87.648111,\"hacc\":65.000000},\"timestamp\":1535659715.414939}]}";
		GeofenceEvent[] geofenceEvents = JsonHelper.FromJson<GeofenceEvent>(geofenceEventsJson);
		AddGeofenceEvents(new List<GeofenceEvent>(geofenceEvents));
	}

	#endif

	public static void DeleteEvent(EventStore.Event evt)
	{
		if (evt.Visit != null) {
			List<Visit> cachedVisits = GetVisits();
			try {
				cachedVisits.Remove(evt.Visit);
				string visitsJson = JsonHelper.ToJson<Visit>(cachedVisits.ToArray());
				PlayerPrefs.SetString("visits", visitsJson);
			} catch (Exception e) {
				Debug.Log("Error parsing visits json: " + e);
			}
		} else {
			List<GeofenceEvent> cachedGeofenceEvents = GetGeofenceEvents();
			try {
				cachedGeofenceEvents.Remove(evt.GeofenceEvent);
				string geofenceEventsJson = JsonHelper.ToJson<GeofenceEvent>(cachedGeofenceEvents.ToArray());
				PlayerPrefs.SetString("geofenceEvents", geofenceEventsJson);
			} catch (Exception e) {
				Debug.Log("Error parsing geofence events json: " + e);
			}
		}
	}

}
