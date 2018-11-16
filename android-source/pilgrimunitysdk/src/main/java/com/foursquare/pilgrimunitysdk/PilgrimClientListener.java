package com.foursquare.pilgrimunitysdk;

public interface PilgrimClientListener {

    void onLocationPermissionResult(boolean granted);

    void onGetCurrentLocationResult(boolean success, String currentLocationJson);

}
