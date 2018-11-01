using Foursquare;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogSceneManager : MonoBehaviour 
{

	public GameObject logItemPrefab;

	public ScrollRect logsScrollRect;

	void Start()
	{
		List<Log> logs = PilgrimUnitySDK.GetLogs();
		foreach (Log log in logs) {
			GameObject logItemGO = Instantiate(logItemPrefab, Vector3.zero, Quaternion.identity);
			logItemGO.transform.SetParent(logsScrollRect.content, false);
			LogItem logItem = logItemGO.GetComponent<LogItem>();
			logItem.Log = log;
		}
	}
	
	public void OnClickCloseButton()
	{
		SceneManager.LoadScene("MainScene");
	}

}
