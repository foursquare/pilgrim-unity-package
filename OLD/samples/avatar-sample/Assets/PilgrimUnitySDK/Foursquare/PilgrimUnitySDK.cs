
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

		private static string DoGetLogs() { return "{\"Items\":[{\"title\":\"api-request\",\"description\":\"POST: https://sdk.foursquare.com/v2/TMKHTRWRRYO4WIZPVJNHA1Q3JU0YBED5XIONMQTOC00YYCLY/pilgrim/enable\",\"timestamp\":1535160576},{\"title\":\"saved home/work regions\",\"description\":\"()\",\"timestamp\":1535160576},{\"title\":\"recalculating home/work\",\"description\":\"{    previousCalculationDate = \\\"<null>\\\";}\",\"timestamp\":1535160576},{\"title\":\"No home/work file or error reading it\",\"description\":\"Error Domain=NSCocoaErrorDomain Code=260 \\\"The file “FSQPRegions.archive” couldn’t be opened because there is no such file.\\\" UserInfo={NSFilePath=/var/mobile/Containers/Data/Application/01CEF01E-9C26-4063-A0C5-FE9FB7EF93B6/Documents/FSQPRegions.archive, NSUnderlyingError=0x1c464d650 {Error Domain=NSPOSIXErrorDomain Code=2 \\\"No such file or directory\\\"}}\",\"timestamp\":1535160576},{\"title\":\"api-request\",\"description\":\"POST: https://sdk.foursquare.com/v2/TMKHTRWRRYO4WIZPVJNHA1Q3JU0YBED5XIONMQTOC00YYCLY/pilgrim/install\",\"timestamp\":1535160576}]}"; }

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
