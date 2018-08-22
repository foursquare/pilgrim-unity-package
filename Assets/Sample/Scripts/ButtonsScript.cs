using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    
	// TODO(rojas): This is not ideal should be on GameManager (ScriptableObject?!?)

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

}