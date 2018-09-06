using Foursquare;
using UnityEngine;
using UnityEngine.UI;

public class EventItem : MonoBehaviour 
{

	public GameObject eventDialogPrefab;

	public Text titleText;

	public Text descriptionText;

	public Text timestampText;

	private EventStore.Event evt;

	public EventStore.Event Event {
		get {
			return evt;
		}
		set {
			titleText.text = value.Title;
			descriptionText.text = value.Description;
			timestampText.text = value.Timestamp.ToString();
			evt = value;
		}
	}

	public void OnClickEventItem()
	{
		GameObject eventDialogGO = Instantiate(eventDialogPrefab, new Vector3(0.0f, 0.0f, -2500.0f), Quaternion.identity);
		eventDialogGO.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
		EventDialog eventDialog = eventDialogGO.GetComponent<EventDialog>();
		eventDialog.Event = Event;
	}

}
