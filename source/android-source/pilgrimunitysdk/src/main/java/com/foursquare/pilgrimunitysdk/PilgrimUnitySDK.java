package com.foursquare.pilgrimunitysdk;

import android.content.Context;
import android.support.annotation.NonNull;

import com.foursquare.pilgrim.PilgrimSdk;
import com.foursquare.pilgrim.PilgrimUserInfo;

@SuppressWarnings("unused")
public final class PilgrimUnitySDK {

    private PilgrimUnitySDK() {

    }

    public static void init(@NonNull Context context, @NonNull String consumerKey, @NonNull String consumerSecret) {
        PilgrimSdk.with(new PilgrimSdk.Builder(context).consumer(consumerKey, consumerSecret));
        setUserInfo(context);
        restartIfPreviouslyStarted(context);
    }

    private static void setUserInfo(@NonNull Context context) {
        PilgrimUserInfo userInfo = Utils.loadUserInfo(context);
        if (userInfo != null) {
            PilgrimSdk.get().setUserInfo(userInfo);
        }
    }

    private static void restartIfPreviouslyStarted(@NonNull Context context) {
        if (Utils.isStarted(context)) {
            PilgrimSdk.start(context);
        }
    }

}
