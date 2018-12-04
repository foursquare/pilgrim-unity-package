using System;

namespace Foursquare
{

    public delegate void LocationPermissionsResult(bool granted);

    public delegate void GetCurrentLocationResult(CurrentLocation currentLocation, Exception exception);

}