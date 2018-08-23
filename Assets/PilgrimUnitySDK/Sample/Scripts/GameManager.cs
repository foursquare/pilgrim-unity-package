using Foursquare;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

	private const string CONSUMER_KEY 		= "TMKHTRWRRYO4WIZPVJNHA1Q3JU0YBED5XIONMQTOC00YYCLY";
	private const string CONSUMER_SECRET 	= "01IYW3XKATTKF40RHUOTFPU0TTFJTJ5QC1IIIXX0NLJDV1FH";

	private static GameManager instance = null;

	void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);  
        }  
        DontDestroyOnLoad(gameObject);
    }

	void Start() 
	{
		PilgrimUnitySDK.RequestPermissions();
	}

	public void OnPermissionsGranted(bool didGrant)
	{
		PilgrimUnitySDK.Start(CONSUMER_KEY, CONSUMER_SECRET);
	}

	public void OnGeofenceEvents(List<Foursquare.GeofenceEvent> geofenceEvents)
	{
		// Debug.Log("geofenceEvents");
		// foreach (GeofenceEvent geofenceEvent in geofenceEvents) {
		// 	Debug.Log(geofenceEvent.EventType);
		// 	Debug.Log(geofenceEvent.VenueID);
		// 	Debug.Log(geofenceEvent.CategoryIDs.Count);
		// 	Debug.Log(geofenceEvent.ChainIDs.Count);
		// 	Debug.Log(geofenceEvent.PartnerVenueID);
		// }
	}
	
}
