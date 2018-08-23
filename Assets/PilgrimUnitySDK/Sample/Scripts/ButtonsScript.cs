using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ButtonsScript : MonoBehaviour
{

	public Button mapButton;
	public Button eventsButton;
	public Button logsButton;

	public Sprite normalSprite;
	public Sprite selectedSprite;

	public Color normalColor;
	public Color selectedColor;

	public enum ButtonType {
		Map,
		Events,
		Logs
	}

	public ButtonType activeButton;

    public void ShowMap()
	{
		SceneManager.LoadScene("MapScene");
	}

	public void ShowEvents()
	{
		SceneManager.LoadScene("EventsScene");
	}

	public void ShowLogs()
	{
		SceneManager.LoadScene("LogsScene");
	}

	void Start()
	{
		SetButtonImage();
	}

	void Update()
	{
		#if UNITY_EDITOR
		if (!Application.isPlaying) {
			SetButtonImage();
		}
		#endif
	}

	private void SetButtonImage()
	{
		switch (activeButton) {
			case ButtonType.Map:
				mapButton.image.sprite = selectedSprite;
				mapButton.GetComponentInChildren<Text>().color = selectedColor;
				eventsButton.image.sprite = normalSprite;
				eventsButton.GetComponentInChildren<Text>().color = normalColor;
				logsButton.image.sprite = normalSprite;
				logsButton.GetComponentInChildren<Text>().color = normalColor;
				break;
			case ButtonType.Events:
				mapButton.image.sprite = normalSprite;
				mapButton.GetComponentInChildren<Text>().color = normalColor;
				eventsButton.image.sprite = selectedSprite;
				eventsButton.GetComponentInChildren<Text>().color = selectedColor;
				logsButton.image.sprite = normalSprite;
				logsButton.GetComponentInChildren<Text>().color = normalColor;
				break;
			case ButtonType.Logs:
				mapButton.image.sprite = normalSprite;
				mapButton.GetComponentInChildren<Text>().color = normalColor;
				eventsButton.image.sprite = normalSprite;
				eventsButton.GetComponentInChildren<Text>().color = normalColor;
				logsButton.image.sprite = selectedSprite;
				logsButton.GetComponentInChildren<Text>().color = selectedColor;
				break;
		}
	}

}