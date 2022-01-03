package com.foursquare.pilgrimunitysdk;

import android.content.Context;

import com.foursquare.pilgrim.LogLevel;
import com.foursquare.pilgrim.PilgrimSdk;

import androidx.annotation.NonNull;

@SuppressWarnings("unused")
public final class PilgrimUnitySDK {

    private PilgrimUnitySDK() {

    }

    public static void init(@NonNull Context context, @NonNull String consumerKey, @NonNull String consumerSecret) {
        PilgrimSdk.with(new PilgrimSdk.Builder(context)
                .consumer(consumerKey, consumerSecret)
                .logLevel(LogLevel.DEBUG)
                .enableDebugLogs());
    }

}
