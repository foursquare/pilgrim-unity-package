
namespace Foursquare
{

	public static class PilgrimUnitySDK
	{

		private static IPilgrimClient client = PilgrimClientFactory.PilgrimClient();

		public static void Start()
		{
			client.Start();
		}

	// 	#if UNITY_EDITOR

	// 	public static void Start(string consumerKey, string consumerSecret) {}

	// 	#elif !UNITY_EDITOR && UNITY_IOS

	// 	[DllImport ("__Internal")]
	// 	public static extern void Start(string consumerKey, string consumerSecret);

	// 	#elif !UNITY_EDITOR && UNITY_ANDROID

	// 	public static void Start(string consumerKey, string consumerSecret) 
	// 	{
	// 		AndroidJavaClass jc = new AndroidJavaClass("com.foursquare.pilgrimunitysdk.PilgrimUnitySDK");
	// 		jc.CallStatic("start", consumerKey, consumerSecret);
	// 	}
		
	// 	#endif

	}

}
