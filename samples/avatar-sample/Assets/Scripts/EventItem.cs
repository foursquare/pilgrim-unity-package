using Foursquare;
using UnityEngine;
using UnityEngine.UI;

public class EventItem : MonoBehaviour 
{

	public Text titleText;

	public Text descriptionText;

	public Text timestampText;

	public EventStore.Item Item {
		set {
			titleText.text = value.Title;
			descriptionText.text = value.Description;
			timestampText.text = value.Timestamp.ToString();
		}
	}

}
