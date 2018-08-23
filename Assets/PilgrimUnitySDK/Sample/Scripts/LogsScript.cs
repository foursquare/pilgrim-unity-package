using Foursquare;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogsScript : MonoBehaviour 
{

	public GameObject logItemPrefab;

	private ScrollRect scrollRect;

	void Awake()
	{
		scrollRect = GetComponent<ScrollRect>();
	}

	void Start()
	{
		List<Log> logs = PilgrimUnitySDK.GetLogs();
		foreach (Log log in logs) {
			GameObject logItemGO = Instantiate(logItemPrefab, Vector3.zero, Quaternion.identity);
			logItemGO.transform.SetParent(scrollRect.content);
			logItemGO.transform.localScale = Vector3.one;
			LogItemScript logItem = logItemGO.GetComponent<LogItemScript>();
			logItem.Log = log;
		}
		
	}
	
}
