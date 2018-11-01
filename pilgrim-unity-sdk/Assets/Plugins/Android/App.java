package com.foursquare.unitysample;

import android.app.Application;
import android.util.Log;

import com.foursquare.pilgrimunitysdk.PilgrimUnitySDK;

public final class App extends Application {

    @Override
    public void onCreate() {
        super.onCreate();
        PilgrimUnitySDK.init(this, "TMKHTRWRRYO4WIZPVJNHA1Q3JU0YBED5XIONMQTOC00YYCLY", "01IYW3XKATTKF40RHUOTFPU0TTFJTJ5QC1IIIXX0NLJDV1FH");
    }

}
