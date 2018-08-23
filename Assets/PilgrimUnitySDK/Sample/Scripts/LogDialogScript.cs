using Foursquare;
using UnityEngine;
using UnityEngine.UI;

public class LogDialogScript : MonoBehaviour 
{

	public Text titleText;

	public Text descriptionText;

	public Log Log {
		set {
			titleText.text = value.Title;
			descriptionText.text = value.Description;
		}
	}

	public void OnClickDone()
	{
		Destroy(gameObject);
	}

}
