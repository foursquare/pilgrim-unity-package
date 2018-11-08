using System;

namespace Foursquare
{

	public delegate void OnLocationPermissionsGranted(bool granted);

	public static class PilgrimUnitySDK
	{

		public static event OnLocationPermissionsGranted OnLocationPermissionsGranted {
			add {
				client.OnLocationPermissionsGranted += value;
			}
			remove {
				client.OnLocationPermissionsGranted -= value;
			}
		}

		private static IPilgrimClient client = PilgrimClientFactory.PilgrimClient();

		public static void SetUserInfo(PilgrimUserInfo userInfo)
		{
			client.SetUserInfo(userInfo);
		}

		public static void RequestLocationPermissions()
		{
			client.RequestLocationPermissions();
		}

		public static void Start()
		{
			client.Start();
		}

		public static void Stop()
		{
			client.Stop();
		}

		public static void ClearAllData()
		{
			client.ClearAllData();
		}

	}

}
