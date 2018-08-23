using Foursquare;
using UnityEngine;
using UnityEngine.UI;

public class LogItemScript : MonoBehaviour 
{

	public GameObject logDialogPrefab;

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
		GameObject logDialogGO = Instantiate(logDialogPrefab, Vector3.zero, Quaternion.identity);
		logDialogGO.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
		LogDialogScript logDialog = logDialogGO.GetComponent<LogDialogScript>();
		logDialog.Log = log;
	}

}
