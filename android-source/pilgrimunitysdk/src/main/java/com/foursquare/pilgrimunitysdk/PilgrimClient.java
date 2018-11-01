package com.foursquare.pilgrimunitysdk;

import android.content.Context;
import android.util.Log;

import com.foursquare.pilgrim.PilgrimSdk;

@SuppressWarnings("unused")
public final class PilgrimClient {

    private Context context;

    public PilgrimClient(Context context) {
        this.context = context;
    }

    public void start() {
        PilgrimSdk.start(context);
        Log.i("PilgrimClient", "start");
    }

    public void stop() {
        PilgrimSdk.stop(context);
        Log.i("PilgrimClient", "stop");
    }

}
