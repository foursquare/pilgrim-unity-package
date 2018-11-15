using Foursquare;
using UnityEngine;
using UnityEngine.UI;

public class CurrentLocationUI : MonoBehaviour 
{

	public Text currentPlaceInfoText;

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
				text += venue.Location.Address + "\n";
				text += venue.Location.City + ", " + venue.Location.State + " " + venue.Location.PostalCode + "\n\n";
				text += "lat: " + string.Format("{0:0.000000}", value.CurrentPlace.Location.Latitude) + ", lng: " + string.Format("{0:0.000000}", value.CurrentPlace.Location.Longitude) + "\n";
				text += value.CurrentPlace.ArrivalTime;
			}
			currentPlaceInfoText.text = text;
		}
	}

	public void OnPressClose()
	{
		Destroy(gameObject);
	}

}
