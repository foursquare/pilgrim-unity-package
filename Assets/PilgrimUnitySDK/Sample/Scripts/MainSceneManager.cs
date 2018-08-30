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

	// void OnGUI()
    // {
	// 	string text = "Events:\n";
	// 	List<GeofenceEvent> geofenceEvents = EventStore.GetGeofenceEvents();
	// 	foreach (GeofenceEvent geofenceEvent in geofenceEvents) {
	// 		text += "" + geofenceEvent.EventType + " @ " + geofenceEvent.Venue.Name + "\n";
	// 	}
    //     GUI.Label(new Rect(0, 0, 1080, 1920), text);
    // }

	void Start() 
	{
		List<EventStore.Item> items = EventStore.GetItems();
		if (items.Count > 0) {
			noEventsText.enabled = false;
			AddItems(items);
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

	public void AddItems(List<EventStore.Item> items)
	{
		foreach (EventStore.Item item in items) {
			AddItem(item);	
		}
	}

	public void AddItem(EventStore.Item item)
	{
		if (noEventsText != null) {
			noEventsText.enabled = false;
		}
		GameObject eventItemGO = Instantiate(eventItemPrefab, Vector3.zero, Quaternion.identity);
		eventItemGO.transform.SetParent(eventsScrollRect.content, false);
		eventItemGO.transform.SetSiblingIndex(0);
		EventItem eventItem = eventItemGO.GetComponent<EventItem>();
		eventItem.Item = item;
	}
	
}
