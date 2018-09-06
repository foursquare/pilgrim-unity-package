using UnityEngine;

public class EventDialog : MonoBehaviour 
{

	public EventStore.Event Event { get; set; }

	void Start()
	{
		AvatarController.ignoreTouches = true;
	}

	public void OnClickDelete()
	{
		EventStore.DeleteEvent(Event);
		MainSceneManager mainSceneManager = (MainSceneManager)GameObject.FindObjectOfType(typeof(MainSceneManager));
		mainSceneManager.ReloadEvents();
		AvatarController avatarController = (AvatarController)GameObject.FindObjectOfType(typeof(AvatarController));
		avatarController.LoadTopCategoryIcon();

		AvatarController.ignoreTouches = false;
		Destroy(gameObject);
	}

	public void OnClickCancel()
	{
		AvatarController.ignoreTouches = false;
		Destroy(gameObject);
	}

}
