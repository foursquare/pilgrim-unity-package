using System;

namespace Foursquare
{

	public static class PilgrimUnitySDK
	{

		public static event LocationPermissionsResult OnLocationPermissionsResult {
			add {
				client.OnLocationPermissionsResult += value;
			}
			remove {
				client.OnLocationPermissionsResult -= value;
			}
		}

		public static event GetCurrentLocationResult OnGetCurrentLocationResult {
			add {
				client.OnGetCurrentLocationResult += value;
			}
			remove {
				client.OnGetCurrentLocationResult -= value;
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

		public static void GetCurrentLocation()
		{
			client.GetCurrentLocation();
		}

	}

}
