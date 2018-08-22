using System.Runtime.InteropServices;
using UnityEngine;

namespace Foursquare
{

	public static class PilgrimUnitySDK
	{

		#if !UNITY_EDITOR && UNITY_IOS

		[DllImport ("__Internal")]
		public static extern float RequestPermissions();

		[DllImport ("__Internal")]
		public static extern void Start(string consumerKey, string consumerSecret);

		#elif !UNITY_EDITOR && UNITY_ANDROID

		public static void RequestPermissions() {
			AndroidJavaClass jc = new AndroidJavaClass("com.foursquare.pilgrimunitysdk.PilgrimUnitySDK");
			jc.CallStatic("requestPermissions");
		}

		public static void Start(string consumerKey, string consumerSecret) {

		}
		
		#else

		public static void RequestPermissions() {
			Object.FindObjectOfType<PilgrimBehavior>().onPermissionsGranted.Invoke(true);
		}

		public static void Start(string consumerKey, string consumerSecret) {}

		#endif

	}

}
