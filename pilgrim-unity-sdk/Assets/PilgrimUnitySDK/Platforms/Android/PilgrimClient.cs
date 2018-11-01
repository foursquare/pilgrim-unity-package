using UnityEngine;

#if UNITY_ANDROID

namespace Foursquare.Android
{

    public class PilgrimClient : IPilgrimClient
    {

        private AndroidJavaObject pilgrimClient;

        public PilgrimClient()
        {
            AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            pilgrimClient = new AndroidJavaObject("com.foursquare.pilgrimunitysdk.PilgrimClient", activity);
        }

        public void Start()
        {
            pilgrimClient.Call("start");
        }

        public void Stop()
        {
            pilgrimClient.Call("stop");
        }

    }

}

#endif