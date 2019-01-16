using Foursquare;
using UnityEngine;
using UnityEngine.UI;

public class GeofenceEventCell : MonoBehaviour
{

    [SerializeField]
    private Text _geofenceEventInfoText;

    public GeofenceEvent GeofenceEvent
    {
        set
        {
            var text = "";
            if (value.Venue == null)
            {
                text += "No Venue Information\n\n\n";
            }
            else
            {
                Venue venue = value.Venue;
                text += venue.Name + "\n";
                text += venue.LocationInformation.Address + "\n";
                text += venue.LocationInformation.City + ", " + venue.LocationInformation.State + " " + venue.LocationInformation.PostalCode + "\n\n";
                text += "lat: " + string.Format("{0:0.000000}", value.Location.Latitude) + ", lng: " + string.Format("{0:0.000000}", value.Location.Longitude) + "\n";
                text += value.Timestamp;
            }
            _geofenceEventInfoText.text = text;
        }
    }

}
