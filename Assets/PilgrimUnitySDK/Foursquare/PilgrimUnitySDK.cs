
using System.Collections.Generic;
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

		[DllImport ("__Internal")]
		private static extern string _GetLogs();

		#elif !UNITY_EDITOR && UNITY_ANDROID

		public static void RequestPermissions() {
			AndroidJavaClass jc = new AndroidJavaClass("com.foursquare.pilgrimunitysdk.PilgrimUnitySDK");
			jc.CallStatic("requestPermissions");
		}

		public static void Start(string consumerKey, string consumerSecret) {

		}

		private static string _GetLogs() { return "{\"Items\":[{\"description\":\"Not implemented on Android\"}]}"; }
		
		#else

		public static void RequestPermissions() {
			Object.FindObjectOfType<PilgrimBehavior>().onPermissionsGranted.Invoke(true);
		}

		public static void Start(string consumerKey, string consumerSecret) {}

		private static string _GetLogs() { return "{\"Items\":[{\"title\":\"api-request\",\"description\":\"POST: https://sdk.foursquare.com/v2/TMKHTRWRRYO4WIZPVJNHA1Q3JU0YBED5XIONMQTOC00YYCLY/pilgrim/enable\"},{\"title\":\"saved home/work regions\",\"description\":\"()\"},{\"title\":\"recalculating home/work\",\"description\":\"{    previousCalculationDate = \\\"<null>\\\";}\"},{\"title\":\"No home/work file or error reading it\",\"description\":\"Error Domain=NSCocoaErrorDomain Code=260 \\\"The file “FSQPRegions.archive” couldn’t be opened because there is no such file.\\\" UserInfo={NSFilePath=/var/mobile/Containers/Data/Application/01CEF01E-9C26-4063-A0C5-FE9FB7EF93B6/Documents/FSQPRegions.archive, NSUnderlyingError=0x1c464d650 {Error Domain=NSPOSIXErrorDomain Code=2 \\\"No such file or directory\\\"}}\"},{\"title\":\"api-request\",\"description\":\"POST: https://sdk.foursquare.com/v2/TMKHTRWRRYO4WIZPVJNHA1Q3JU0YBED5XIONMQTOC00YYCLY/pilgrim/install\"}]}"; }

		#endif

		// NOTE(rojas): This should probably be removed don't think 3rd parties actually use logs, good for internal debugging
		public static List<Log> GetLogs()
		{
			return new List<Log>(JsonHelper.FromJson<Log>(_GetLogs()));
		}

	}

}
