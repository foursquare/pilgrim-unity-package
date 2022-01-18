package com.foursquare.swiftsure;

import android.app.Application;

import com.foursquare.pilgrimunitysdk.PilgrimUnitySDK;

public final class App extends Application {

    @Override
    public void onCreate() {
        super.onCreate();
        PilgrimUnitySDK.init(this, "CONSUMER_KEY", "CONSUMER_SECRET");
    }

}
