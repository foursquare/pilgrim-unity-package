using Foursquare;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour 
{

	public static bool isFirstRun = true;

	public GameObject eventItemPrefab;

	public ScrollRect eventsScrollRect;

	public Text noEventsText;

	public GameObject avatarPrefab;

	void OnGUI()
    {
		string text = "Events:\n";
		List<GeofenceEvent> geofenceEvents = EventStore.GetGeofenceEvents();
		foreach (GeofenceEvent geofenceEvent in geofenceEvents) {
			text += "" + geofenceEvent.EventType + " @ " + geofenceEvent.Venue.Name + "\n";
		}
        GUI.Label(new Rect(0, 0, 1080, 1920), text);
    }

	void Start() 
	{
		List<GeofenceEvent> geofenceEvents = EventStore.GetGeofenceEvents();
		if (geofenceEvents.Count > 0) {
			noEventsText.enabled = false;
			AddEvents(geofenceEvents);
		}
		
		if (MainSceneManager.isFirstRun) {
			Instantiate(avatarPrefab, Vector3.up * 8.0f, Quaternion.Euler(0.0f, 180.0f, 0.0f));
		} else {
			Instantiate(avatarPrefab, Vector3.zero, Quaternion.Euler(0.0f, 180.0f, 0.0f));
		}
	}

	public void OnClickLogsButton()
	{
		SceneManager.LoadScene("LogsScene");
		MainSceneManager.isFirstRun = false;
	}

	public void AddEvents(List<GeofenceEvent> geofenceEvents)
	{
		if (geofenceEvents.Count > 0 && noEventsText != null) {
			noEventsText.enabled = false;
		}
		foreach (GeofenceEvent geofenceEvent in geofenceEvents) {
			GameObject eventItemGO = Instantiate(eventItemPrefab, Vector3.zero, Quaternion.identity);
			eventItemGO.transform.SetParent(eventsScrollRect.content, false);
			EventItem eventItem = eventItemGO.GetComponent<EventItem>();
			eventItem.GeofenceEvent = geofenceEvent;
		}
	}
	
}
