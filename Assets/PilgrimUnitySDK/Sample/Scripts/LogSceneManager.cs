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
		#if UNITY_EDITOR
		Log[] logsArray = JsonHelper.FromJson<Log>("{\"Items\":[{\"title\":\"api-request\",\"description\":\"POST: https://sdk.foursquare.com/v2/TMKHTRWRRYO4WIZPVJNHA1Q3JU0YBED5XIONMQTOC00YYCLY/pilgrim/enable\",\"timestamp\":1535160576},{\"title\":\"saved home/work regions\",\"description\":\"()\",\"timestamp\":1535160576},{\"title\":\"recalculating home/work\",\"description\":\"{    previousCalculationDate = \\\"<null>\\\";}\",\"timestamp\":1535160576},{\"title\":\"No home/work file or error reading it\",\"description\":\"Error Domain=NSCocoaErrorDomain Code=260 \\\"The file “FSQPRegions.archive” couldn’t be opened because there is no such file.\\\" UserInfo={NSFilePath=/var/mobile/Containers/Data/Application/01CEF01E-9C26-4063-A0C5-FE9FB7EF93B6/Documents/FSQPRegions.archive, NSUnderlyingError=0x1c464d650 {Error Domain=NSPOSIXErrorDomain Code=2 \\\"No such file or directory\\\"}}\",\"timestamp\":1535160576},{\"title\":\"api-request\",\"description\":\"POST: https://sdk.foursquare.com/v2/TMKHTRWRRYO4WIZPVJNHA1Q3JU0YBED5XIONMQTOC00YYCLY/pilgrim/install\",\"timestamp\":1535160576}]}");
		List<Log> logs = new List<Log>(logsArray);
		#elif UNITY_ANDROID
		Log[] logsArray = JsonHelper.FromJson<Log>("{\"Items\":[{\"title\":\"No logs\",\"description\":\"Not implemented on Android ... yet\",\"timestamp\":1535160576}]}");
		List<Log> logs = new List<Log>(logsArray);
		#else
		List<Log> logs = PilgrimUnitySDK.GetLogs();
		#endif
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
