using System;

namespace Foursquare
{

	public static class PilgrimUnitySDK
	{

		public static event Action<bool> OnLocationPermissionResult 
		{
			add {
				_client.OnLocationPermissionResult += value;
			}
			remove {
				_client.OnLocationPermissionResult -= value;
			}
		}

		public static event Action<CurrentLocation, Exception> OnGetCurrentLocationResult
		{
			add {
				_client.OnGetCurrentLocationResult += value;
			}
			remove {
				_client.OnGetCurrentLocationResult -= value;
			}
		}

		private static IPilgrimClient _client = PilgrimClientFactory.PilgrimClient();

		public static void SetUserInfo(PilgrimUserInfo userInfo)
		{
			_client.SetUserInfo(userInfo);
		}

		public static void RequestLocationPermissions()
		{
			_client.RequestLocationPermissions();
		}

		public static void Start()
		{
			_client.Start();
		}

		public static void Stop()
		{
			_client.Stop();
		}

		public static void ClearAllData()
		{
			_client.ClearAllData();
		}

		public static void GetCurrentLocation()
		{
			_client.GetCurrentLocation();
		}

	}

}
