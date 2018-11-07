using Foursquare;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

	void Start() 
	{
		PilgrimUserInfo userInfo = new PilgrimUserInfo();
		userInfo.Set("a", "b");
		userInfo.SetBirthday(new System.DateTime(1984, 11, 26));
		userInfo.SetGender(PilgrimUserInfo.Gender.Male);
		userInfo.SetUserId("marchinram");
		PilgrimUnitySDK.SetUserInfo(userInfo);

		PilgrimUnitySDK.Start();
	}
	
}
