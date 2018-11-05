
namespace Foursquare
{

	public static class PilgrimUnitySDK
	{

		private static IPilgrimClient client = PilgrimClientFactory.PilgrimClient();

		public static void SetUserInfo(PilgrimUserInfo userInfo)
		{
			client.SetUserInfo(userInfo);
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
