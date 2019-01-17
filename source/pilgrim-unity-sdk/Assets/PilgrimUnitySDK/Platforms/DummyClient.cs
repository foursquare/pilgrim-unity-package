using System;
using UnityEngine;

namespace Foursquare
{

    public class DummyClient : IPilgrimClient
    {

        public event Action<bool> OnLocationPermissionResult = delegate { };

        public event Action<CurrentLocation, Exception> OnGetCurrentLocationResult = delegate { };

        public void SetUserInfo(UserInfo userInfo, bool persisted)
        {

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
                editorMock.mockLocation = Resources.Load<CurrentLocationMock>("4sqChicago");
            }
            else if (editorMock.mockLocation == null)
            {
                Debug.LogError(string.Format("PilgrimUnitySDK Error: Null mock location on GetCurrentLocationEditorMock script attached to {0}", editorMock.gameObject.name));
                return;
            }
            var currentLocation = editorMock.mockLocation.CurrentLocation;
            OnGetCurrentLocationResult(currentLocation, null);
        }

    }

}