
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Foursquare
{

	public static class PilgrimUnitySDK
	{

		#if !UNITY_EDITOR && UNITY_IOS

		[DllImport ("__Internal")]
		public static extern float RequestLocationPermission();

		[DllImport ("__Internal")]
		public static extern void Start(string consumerKey, string consumerSecret);

		[DllImport ("__Internal")]
		private static extern string DoGetLogs();

		#elif !UNITY_EDITOR && UNITY_ANDROID

		public static void RequestLocationPermission() {
			AndroidJavaClass jc = new AndroidJavaClass("com.foursquare.pilgrimunitysdk.PilgrimUnitySDK");
			jc.CallStatic("requestPermissions");
		}

		public static void Start(string consumerKey, string consumerSecret) {

		}

		private static string DoGetLogs() { return return "{\"Items\":[]}"; }
		
		#else

		public static void RequestLocationPermission() {
			UnityEngine.Object.FindObjectOfType<PilgrimBehavior>().onLocationPermissionGranted.Invoke(true);
		}

		public static void Start(string consumerKey, string consumerSecret) {}

		private static string DoGetLogs() { return "{\"Items\":[]}"; }

		#endif

		// NOTE(rojas): This should probably be removed don't think 3rd parties actually use logs, good for internal debugging
		public static List<Log> GetLogs()
		{
			List<Log> logs = new List<Log>();
			try {
				logs = new List<Log>(JsonHelper.FromJson<Log>(DoGetLogs()));
			} catch (Exception e) {
				Debug.Log("Error parsing logs json: " + e);
			}
			return logs;
		}

	}

}
