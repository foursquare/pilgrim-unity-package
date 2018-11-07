#if UNITY_ANDROID

using UnityEngine;

namespace Foursquare.Android
{

    public class PilgrimClient : IPilgrimClient
    {

        private AndroidJavaObject pilgrimClient;

        public PilgrimClient()
        {
            var playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            pilgrimClient = new AndroidJavaObject("com.foursquare.pilgrimunitysdk.PilgrimClient", activity);
        }

        public void SetUserInfo(PilgrimUserInfo userInfo)
        {
            var userInfoMap = new AndroidJavaObject("java.util.HashMap");
            var putMethod = AndroidJNIHelper.GetMethodID(userInfoMap.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
            
            object[] args = new object[2];
            foreach (var pair in userInfo.BackingStore) {
                using (AndroidJavaObject k = new AndroidJavaObject("java.lang.String", pair.Key)) {
                    using (AndroidJavaObject v = new AndroidJavaObject("java.lang.String", pair.Value)) {
                        args[0] = k;
                        args[1] = v;
                        AndroidJNI.CallObjectMethod(userInfoMap.GetRawObject(), putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
                    }
                }
            }

            pilgrimClient.Call("setUserInfo", userInfoMap);
        }

        public void Start()
        {
            pilgrimClient.Call("start");
        }

        public void Stop()
        {
            pilgrimClient.Call("stop");
        }

        public void ClearAllData()
        {
            pilgrimClient.Call("clearAllData");
        }

    }

}

#endif