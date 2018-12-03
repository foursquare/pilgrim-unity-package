namespace Foursquare
{

    public delegate void LocationPermissionsResult(bool granted);

    public delegate void GetCurrentLocationResult(bool success, CurrentLocation currentLocation);

}