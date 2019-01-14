using UnityEngine;
using UnityEngine.UI;

public class AlertUI : MonoBehaviour 
{

	[SerializeField]
	private Text _alertText;

	public string Message
	{
		set { _alertText.text = value; }
	}

	public void OnPressClose()
	{
		Destroy(gameObject);
	}

}
