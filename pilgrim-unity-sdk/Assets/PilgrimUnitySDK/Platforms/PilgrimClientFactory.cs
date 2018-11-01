
namespace Foursquare
{

    public static class PilgrimClientFactory
    {
        
        public static IPilgrimClient PilgrimClient()
        {
            #if UNITY_EDITOR
                return new DummyClient();
            #elif UNITY_ANDROID
                return new Foursquare.Android.PilgrimClient();
            #elif UNITY_IOS
                return new Foursquare.iOS.PilgrimClient();
            #else
                return new GoogleMobileAds.Common.DummyClient();
            #endif
        }

    }

}