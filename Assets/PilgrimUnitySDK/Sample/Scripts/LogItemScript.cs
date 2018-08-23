using Foursquare;
using UnityEngine;
using UnityEngine.UI;

public class LogItemScript : MonoBehaviour 
{

	private Log log;

	public Log Log {
		set {
			log = value;
			title.text = value.Title;
		}
	}

	private Text title;

	void Awake()
	{
		title = GetComponentInChildren<Text>();
	}

	public void OnClickLogItem()
	{
		Debug.Log(log.Description);
	}

}
