using System;
using System.Collections;
using UnityEngine;

namespace Foursquare
{

    public class EditorClient : IPilgrimClient
    {

        private const string UserInfoKey = "userInfo";

        private UserInfo _userInfo;

        private MonoBehaviour _editorClientBehavior;

        public event Action<bool> OnLocationPermissionResult = delegate { };

        public event Action OnLocationPermissionShowRationale = delegate { };

        public event Action<CurrentLocation, Exception> OnGetCurrentLocationResult = delegate { };

        public EditorClient()
        {
            var userInfoJson = PlayerPrefs.GetString(UserInfoKey);
            _userInfo = JsonUtility.FromJson<UserInfo>(userInfoJson);

            var clientGameObject = new GameObject("_EditorClient");
            _editorClientBehavior = clientGameObject.AddComponent<EditorClientBehavior>();
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
                var userInfoJson = JsonUtility.ToJson(userInfo);
                PlayerPrefs.SetString(UserInfoKey, userInfoJson);
                PlayerPrefs.Save();
            }
            else
            {
                PlayerPrefs.DeleteKey(UserInfoKey);
            }
        }

        public void RequestLocationPermissions()
        {
            _editorClientBehavior.StartCoroutine(SendLocationPermissionResult());
        }

        private IEnumerator SendLocationPermissionResult()
        {
            yield return null;
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
            _editorClientBehavior.StartCoroutine(SendGetCurrentLocationResult());
        }

        private IEnumerator SendGetCurrentLocationResult()
        {
            var editorMock = GameObject.FindObjectOfType<GetCurrentLocationEditorMock>();
            if (editorMock == null)
            {
                editorMock = new GameObject("_PilgrimMockLocation").AddComponent<GetCurrentLocationEditorMock>();
                editorMock.mockLocation = Resources.Load<CurrentLocationMock>("4sqHQ");
            }
            if (editorMock.mockLocation == null)
            {
                OnGetCurrentLocationResult(null, new Exception(string.Format("PilgrimUnitySDK Error: Null mock location on GetCurrentLocationEditorMock script attached to {0}", editorMock.gameObject.name)));
                yield return null;
            }
            var currentLocation = editorMock.mockLocation.CurrentLocation;
            OnGetCurrentLocationResult(currentLocation, null);
        }

        public void ShowDebugScreen()
        {

        }

        public void FireTestVisit(Location location)
        {

        }

    }

    public class EditorClientBehavior : MonoBehaviour
    {

    }

}