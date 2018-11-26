using UnityEngine;

public class UserInfoUI : MonoBehaviour 
{

	public void OnPressClose()
	{
		// PilgrimUserInfo userInfo = new PilgrimUserInfo();
		// if (userIDInputField.text != null && userIDInputField.text.Length > 0) {
		// 	userInfo.SetUserId(userIDInputField.text);
		// }
		// if (birthdayInputField.text != null && birthdayInputField.text.Length > 0) {
		// 	DateTime birthday;
		// 	if (DateTime.TryParseExact(birthdayInputField.text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday)) {
		// 		userInfo.SetBirthday(birthday);
		// 	}
		// }
		// if (genderDropdown.value == 1) {
		// 	userInfo.SetGender(PilgrimUserInfo.Gender.Male);	
		// } else if (genderDropdown.value == 2) {
		// 	userInfo.SetGender(PilgrimUserInfo.Gender.Female);
		// }
		// PilgrimUnitySDK.SetUserInfo(userInfo);
		
		Destroy(gameObject);
	}

}
