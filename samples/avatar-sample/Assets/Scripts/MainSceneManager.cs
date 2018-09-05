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

	void Start() 
	{
		List<EventStore.Event> events = EventStore.GetEvents();
		if (events.Count > 0) {
			noEventsText.enabled = false;
			AddEvents(events);
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

	public void AddEvents(List<EventStore.Event> items)
	{
		foreach (EventStore.Event item in items) {
			AddEvent(item);	
		}
	}

	public void AddEvent(EventStore.Event evt)
	{
		if (noEventsText != null) {
			noEventsText.enabled = false;
		}
		GameObject eventItemGO = Instantiate(eventItemPrefab, Vector3.zero, Quaternion.identity);
		eventItemGO.transform.SetParent(eventsScrollRect.content, false);
		EventItem eventItem = eventItemGO.GetComponent<EventItem>();
		eventItem.Event = evt;
	}
	
}
