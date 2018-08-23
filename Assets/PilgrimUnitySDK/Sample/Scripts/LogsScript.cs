using Foursquare;
using UnityEngine;
using UnityEngine.UI;

public class LogsScript : MonoBehaviour 
{

	public Transform logItemPrefab;

	private ScrollRect scrollRect;

	void Awake()
	{
		scrollRect = GetComponent<ScrollRect>();
	}

	void Start()
	{
		// PilgrimUnitySDK.GetLogs();
		for (int i = 0; i < 100; i++) {
			Transform logItem = Instantiate(logItemPrefab, Vector3.zero, Quaternion.identity);
			logItem.transform.SetParent(scrollRect.content);
			logItem.localScale = Vector3.one;
			Text logItemText = logItem.Find("Text").GetComponent<Text>();
			logItemText.text = "Pilgrim Resumed. Pilgrim Resumed. Pilgrim Resumed. Pilgrim Resumed. Pilgrim Resumed. Pilgrim Resumed. Pilgrim Resumed. Pilgrim Resumed. Pilgrim Resumed. Pilgrim Resumed. ";
		}
		
	}
	
}
