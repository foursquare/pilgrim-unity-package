using Foursquare;
using UnityEngine;
using UnityEngine.UI;

public class CurrentLocationUI : MonoBehaviour 
{

	public Text currentPlaceInfoText;

	public ScrollRect scrollRect;

	public GameObject geofenceEventCellPrefab;

	public CurrentLocation CurrentLocation
	{
		set
		{	
			var text = "";
			if (value.CurrentPlace.Venue == null) {
				text += "No Venue Information\n\n\n";
			} else {
				Venue venue = value.CurrentPlace.Venue;
				text += venue.Name + "\n";
				text += venue.LocationInformation.Address + "\n";
				text += venue.LocationInformation.City + ", " + venue.LocationInformation.State + " " + venue.LocationInformation.PostalCode + "\n\n";
				text += "lat: " + string.Format("{0:0.000000}", value.CurrentPlace.Location.Latitude) + ", lng: " + string.Format("{0:0.000000}", value.CurrentPlace.Location.Longitude) + "\n";
				text += value.CurrentPlace.ArrivalTime;
			}
			currentPlaceInfoText.text = text;
			
			foreach (var geofenceEvent in value.MatchedGeofences) {
				var gameObject = Instantiate<GameObject>(geofenceEventCellPrefab, Vector3.zero, Quaternion.identity);
				gameObject.transform.SetParent(scrollRect.content, false);
			
				var geofenceEventCell = gameObject.GetComponent<GeofenceEventCell>();
				geofenceEventCell.GeofenceEvent = geofenceEvent;
			}
		}
	}

	public void OnPressClose()
	{
		Destroy(gameObject);
	}

}
