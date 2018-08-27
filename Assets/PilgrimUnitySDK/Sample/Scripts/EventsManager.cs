using Foursquare;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventsManager : MonoBehaviour 
{

	public static bool isFirstRun = true;

	public GameObject eventItemPrefab;

	public ScrollRect eventsScrollRect;

	public Text noEventsText;

	void Start() 
	{
		#if UNITY_EDITOR
		EventStore.Clear();
		#endif

		List<GeofenceEvent> events = EventStore.GetGeofenceEvents();
		if (events.Count > 0) {
			Destroy(noEventsText.gameObject);
			AddEvents(events);
		}

		#if UNITY_EDITOR
		string geofenceEventsJSON  = "{\"Items\":[{\"eventType\":\"dwell\",\"venueID\":\"547b8903498ef62123c41ecb\",\"categoryIDs\":[],\"chainIDs\":[],\"partnerVenueID\":\"\",\"venue\":{\"name\":\"Casey's General Store\",\"location\":{\"lat\":41.891381,\"lng\":-87.648111,\"hacc\":65.000000}},\"timestamp\":1535219480.414939}]}";
		GeofenceEvent[] geofenceEvents = JsonHelper.FromJson<GeofenceEvent>(geofenceEventsJSON);
		OnGeofenceEvents(new List<GeofenceEvent>(geofenceEvents));
		#endif
	}

	public void OnGeofenceEvents(List<Foursquare.GeofenceEvent> geofenceEvents)
	{
		EventStore.AddGeofenceEvents(geofenceEvents);
		AddEvents(geofenceEvents);
	}

	public void OnClickLogsButton()
	{
		SceneManager.LoadScene("LogsScene");
	}

	private void AddEvents(List<GeofenceEvent> geofenceEvents)
	{
		if (geofenceEvents.Count > 0 && noEventsText != null) {
			Destroy(noEventsText.gameObject);
		}
		foreach (GeofenceEvent geofenceEvent in geofenceEvents) {
			GameObject eventItemGO = Instantiate(eventItemPrefab, Vector3.zero, Quaternion.identity);
			eventItemGO.transform.SetParent(eventsScrollRect.content, false);
			EventItem eventItem = eventItemGO.GetComponent<EventItem>();
			eventItem.GeofenceEvent = geofenceEvent;
		}
	}
	
}
