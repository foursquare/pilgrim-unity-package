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
	
	public void OnGeofenceEvents(List<Foursquare.GeofenceEvent> geofenceEvents)
	{
		EventStore.AddGeofenceEvents(geofenceEvents);
		
		MainSceneManager mainSceneManager = (MainSceneManager)GameObject.FindObjectOfType(typeof(MainSceneManager));
		if (mainSceneManager) {
			mainSceneManager.AddEvents(geofenceEvents);
		}
	}

}
