package com.foursquare.pilgrimunitysdk;

public interface PilgrimClientListener {

    // Can't pass null, Unity assumes it's an AndroidJavaObject,
    // so pass empty string instead, check bool 'success' flag
    void onGetCurrentLocationResult(boolean success, String currentLocationJson, String errorMessage);

}
