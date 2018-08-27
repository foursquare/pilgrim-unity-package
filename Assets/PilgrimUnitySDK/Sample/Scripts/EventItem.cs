using Foursquare;
using UnityEngine;
using UnityEngine.UI;

public class EventItem : MonoBehaviour 
{

	public Text titleText;

	public Text descriptionText;

	public Text timestampText;

	public GeofenceEvent GeofenceEvent {
		set {
			titleText.text = "Geofence event";
			descriptionText.text = value.EventType.ToString() + " @ " + value.Venue.Name;
			timestampText.text = value.Timestamp.ToString();
		}
	}

}
