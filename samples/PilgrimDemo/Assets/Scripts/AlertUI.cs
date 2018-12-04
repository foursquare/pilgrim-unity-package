using UnityEngine;
using UnityEngine.UI;

public class AlertUI : MonoBehaviour 
{

	public Text alertText;

	public string Message
	{
		set { alertText.text = value; }
	}

	public void OnPressClose()
	{
		Destroy(gameObject);
	}

}
