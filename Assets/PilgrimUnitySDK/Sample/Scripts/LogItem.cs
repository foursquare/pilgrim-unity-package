using Foursquare;
using UnityEngine;
using UnityEngine.UI;

public class LogItem : MonoBehaviour 
{

	public GameObject logDialogPrefab;

	public Text titleText;

	public Text timestampText;

	private Log log;

	public Log Log {
		set {
			log = value;
			titleText.text = value.Title;
			timestampText.text = value.Timestamp.ToString();
		}
	}

	public void OnClickLogItem()
	{
		GameObject logDialogGO = Instantiate(logDialogPrefab, Vector3.zero, Quaternion.identity);
		logDialogGO.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
		LogDialog logDialog = logDialogGO.GetComponent<LogDialog>();
		logDialog.Log = log;
	}

}
