using Foursquare;
using UnityEngine;
using UnityEngine.UI;

public class CurrentLocationUI : MonoBehaviour
{

    public Text _currentPlaceInfoText;

    public ScrollRect _scrollRect;

    public GameObject _geofenceEventCellPrefab;

    public CurrentLocation CurrentLocation
    {
        set
        {
            var text = "";
            if (value.CurrentPlace.Venue == null)
            {
                text += "No Venue Information\n\n\n";
            }
            else
            {
                Venue venue = value.CurrentPlace.Venue;
                text += venue.Name + "\n";
                if (venue.LocationInformation != null)
                {
                    text += venue.LocationInformation.Address + "\n";
                    text += venue.LocationInformation.City + ", " + venue.LocationInformation.State + " " + venue.LocationInformation.PostalCode + "\n\n";
                }
                else
                {
                    text += "No Location Information\n\n";
                }
                text += "lat: " + string.Format("{0:0.000000}", value.CurrentPlace.Location.Latitude) + ", lng: " + string.Format("{0:0.000000}", value.CurrentPlace.Location.Longitude) + "\n";
                text += value.CurrentPlace.ArrivalTime;
            }
            _currentPlaceInfoText.text = text;

            foreach (var geofenceEvent in value.MatchedGeofences)
            {
                var gameObject = Instantiate<GameObject>(_geofenceEventCellPrefab, Vector3.zero, Quaternion.identity);
                gameObject.transform.SetParent(_scrollRect.content, false);

                var geofenceEventCell = gameObject.GetComponent<GeofenceEventCell>();
                geofenceEventCell.GeofenceEvent = geofenceEvent;
            }

            _scrollRect.normalizedPosition = new Vector2(0, 1);
        }
    }

    public void OnPressClose()
    {
        Destroy(gameObject);
    }

}
