using Foursquare;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

	void Start() 
	{
		PilgrimUnitySDK.RequestPermissions();
	}

	public void OnPermissionsGranted(bool didGrant)
	{
		PilgrimUnitySDK.Start("TMKHTRWRRYO4WIZPVJNHA1Q3JU0YBED5XIONMQTOC00YYCLY", "01IYW3XKATTKF40RHUOTFPU0TTFJTJ5QC1IIIXX0NLJDV1FH");
	}

	public void OnGeofenceEvents(List<Foursquare.GeofenceEvent> geofenceEvents)
	{
		Debug.Log("geofenceEvents");
		foreach (GeofenceEvent geofenceEvent in geofenceEvents) {
			Debug.Log(geofenceEvent.EventType);
			Debug.Log(geofenceEvent.VenueID);
			Debug.Log(geofenceEvent.CategoryIDs.Count);
			Debug.Log(geofenceEvent.ChainIDs.Count);
			Debug.Log(geofenceEvent.PartnerVenueID);
		}
	}
	
}
