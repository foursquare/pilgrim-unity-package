#if UNITY_IOS

using System.Runtime.InteropServices;

namespace Foursquare.iOS
{

    public class PilgrimClient : IPilgrimClient
    {

        public event OnLocationPermissionsGranted OnLocationPermissionsGranted;

        [DllImport("__Internal")]
        private static extern void PilgrimStart();

        [DllImport("__Internal")]
        private static extern void PilgrimStop();

        [DllImport("__Internal")]
        private static extern void PilgrimClearAllData();

        public void SetUserInfo(PilgrimUserInfo userInfo)
        {

        }

        public void RequestLocationPermissions()
        {
            
        }

        public void Start()
        {
            PilgrimStart();
        }

        public void Stop()
        {
            PilgrimStop();
        }

        public void ClearAllData()
        {
            PilgrimClearAllData();
        }

    }

}

#endif