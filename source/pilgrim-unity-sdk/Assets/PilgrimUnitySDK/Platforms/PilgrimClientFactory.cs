namespace Foursquare
{

    public static class PilgrimClientFactory
    {

        public static IPilgrimClient PilgrimClient()
        {
#if UNITY_EDITOR
            return new EditorClient();
#elif UNITY_IOS
            return new Foursquare.iOS.PilgrimClient();
#elif UNITY_ANDROID
            return new Foursquare.Android.PilgrimClient();
#else
            return new EditorClient();
#endif
        }

    }

}