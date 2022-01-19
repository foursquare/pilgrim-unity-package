using System;

namespace Foursquare
{

    public static class DateTimeExtensions
    {
        public static DateTime DateTimeFromUnixTime(this double unixTime)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(unixTime).ToLocalTime();
        }

        public static double UnixSecondsFromDateTime(this DateTime dateTime)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = dateTime.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public static double UnixMillisecondsFromDateTime(this DateTime dateTime)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = dateTime.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

    }

}