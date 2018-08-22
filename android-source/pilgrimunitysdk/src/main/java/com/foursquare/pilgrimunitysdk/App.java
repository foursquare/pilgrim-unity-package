package com.foursquare.pilgrimunitysdk;

import android.app.Application;

public final class App extends Application {

    private static App instance;

    @Override
    public void onCreate() {
        super.onCreate();
        instance = this;
    }

    public static App getInstance() {
        return instance;
    }

}
