using Foursquare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilgrimEventsManager : MonoBehaviour 
{

	private static PilgrimEventsManager instance = null;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);  
        }  
        DontDestroyOnLoad(gameObject);
    }
	
	public void OnGeofenceEvents(List<GeofenceEvent> geofenceEvents)
	{
		EventStore.AddGeofenceEvents(geofenceEvents);
		
		MainSceneManager mainSceneManager = (MainSceneManager)GameObject.FindObjectOfType(typeof(MainSceneManager));
		if (mainSceneManager) {
            foreach (GeofenceEvent geofenceEvent in geofenceEvents) {
			    mainSceneManager.AddEvent(geofenceEvent);
            }
		}
	}

    public void OnVisit(Visit visit)
    {
        EventStore.AddVisit(visit);

        MainSceneManager mainSceneManager = (MainSceneManager)GameObject.FindObjectOfType(typeof(MainSceneManager));
		if (mainSceneManager) {
            mainSceneManager.AddEvent(visit);
		}
    }

}
