using System;
using UnityEngine;

namespace Foursquare
{

    public class EditorClient : IPilgrimClient
    {

        private const string UserInfoKey = "userInfo";

        private UserInfo _userInfo;

        public event Action<bool> OnLocationPermissionResult = delegate { };

        public event Action<CurrentLocation, Exception> OnGetCurrentLocationResult = delegate { };

        public EditorClient()
        {
            _userInfo = LoadUserInfoFromPlayerPrefs();
        }

        public UserInfo GetUserInfo()
        {
            return _userInfo;
        }

        public void SetUserInfo(UserInfo userInfo, bool persisted)
        {
            _userInfo = userInfo;
            if (persisted)
            {
                SaveUserInfoToPlayerPrefs(userInfo);
            }
            else
            {
                PlayerPrefs.DeleteKey(UserInfoKey);
            }
        }

        public void RequestLocationPermissions()
        {
            OnLocationPermissionResult(true);
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }

        public void ClearAllData()
        {

        }

        public void GetCurrentLocation()
        {
            var editorMock = GameObject.FindObjectOfType<GetCurrentLocationEditorMock>();
            if (editorMock == null)
            {
                editorMock = new GameObject("_PilgrimMockLocation").AddComponent<GetCurrentLocationEditorMock>();
                editorMock.mockLocation = Resources.Load<CurrentLocationMock>("4sqHQ");
            }
            else if (editorMock.mockLocation == null)
            {
                OnGetCurrentLocationResult(null, new Exception(string.Format("PilgrimUnitySDK Error: Null mock location on GetCurrentLocationEditorMock script attached to {0}", editorMock.gameObject.name)));
                return;
            }
            var currentLocation = editorMock.mockLocation.CurrentLocation;
            OnGetCurrentLocationResult(currentLocation, null);
        }

        private UserInfo LoadUserInfoFromPlayerPrefs()
        {
            var keysString = PlayerPrefs.GetString(UserInfoKey);
            if (keysString == null || keysString == "")
            {
                return null;
            }

            var userInfo = new UserInfo();
            if (keysString != null && keysString.Length > 0)
            {
                var keys = keysString.Split(',');
                foreach (var key in keys)
                {
                    var value = PlayerPrefs.GetString(key);
                    if (key == "userId")
                    {
                        userInfo.SetUserId(value);
                    }
                    else if (key == "gender")
                    {
                        userInfo.SetGender(value == "male" ? UserInfo.Gender.Male : UserInfo.Gender.Female);
                    }
                    else if (key == "birthday")
                    {
                        var seconds = long.Parse(value);
                        var epochStart = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                        var birthday = epochStart.AddSeconds(seconds);
                        userInfo.SetBirthday(new DateTime(birthday.Year, birthday.Month, birthday.Day));
                    }
                    else
                    {
                        userInfo.Set(key, value);
                    }
                }
            }
            return userInfo;
        }

        private void SaveUserInfoToPlayerPrefs(UserInfo userInfo)
        {
            string keysString = "";
            foreach (var pair in userInfo.BackingStore)
            {
                if (keysString.Length > 0)
                {
                    keysString += ",";
                }
                keysString += pair.Key;
                PlayerPrefs.SetString(pair.Key, pair.Value);
            }
            PlayerPrefs.SetString(UserInfoKey, keysString);
            PlayerPrefs.Save();
        }


    }

}