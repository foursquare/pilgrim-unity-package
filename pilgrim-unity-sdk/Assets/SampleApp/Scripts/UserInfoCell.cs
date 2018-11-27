using UnityEngine;
using UnityEngine.UI;

public class UserInfoCell : MonoBehaviour 
{

	public InputField keyInputField;
	public InputField valueInputField;

	public void OnPressRemove()
	{
		Destroy(gameObject);
	}

}
