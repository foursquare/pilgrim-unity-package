using System;

namespace Foursquare
{

    internal static class Utils
    {
        internal static DateTime FromUnixSeconds(double unixTime)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return dateTime.AddSeconds(unixTime).ToLocalTime();
        }

    }

}