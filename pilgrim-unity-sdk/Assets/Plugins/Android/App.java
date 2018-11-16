package com.foursquare.unitysample;

import android.app.Application;

import com.foursquare.pilgrimunitysdk.PilgrimUnitySDK;

public final class App extends Application {

    @Override
    public void onCreate() {
        super.onCreate();
        PilgrimUnitySDK.init(this, "SF45TRX2FDJBCZ3W2HT5J3UBU2WBLQKBCCVRBIX01AOB2GFW", "0BGOFDZK42TDSZN305VI5P241WIOMJNGWR0RXGLOZH4SPTK4");
    }

}
