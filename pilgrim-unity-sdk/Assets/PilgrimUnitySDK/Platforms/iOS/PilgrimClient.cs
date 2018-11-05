using System.Runtime.InteropServices;

#if UNITY_IOS

namespace Foursquare.iOS
{

    public class PilgrimClient : IPilgrimClient
    {

        [DllImport("__Internal")]
        private static extern void PilgrimStart();

        [DllImport("__Internal")]
        private static extern void PilgrimStop();

        [DllImport("__Internal")]
        private static extern void PilgrimClearAllData();

        public void SetUserInfo(PilgrimUserInfo userInfo)
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