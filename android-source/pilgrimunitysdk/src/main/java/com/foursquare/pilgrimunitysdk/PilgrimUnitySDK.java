package com.foursquare.pilgrimunitysdk;

import android.content.Context;
import android.content.Intent;

import com.foursquare.pilgrim.PilgrimNotificationHandler;
import com.foursquare.pilgrim.PilgrimSdk;
import com.foursquare.pilgrim.PilgrimSdkPlaceNotification;

@SuppressWarnings("unused")
public final class PilgrimUnitySDK {

    private PilgrimUnitySDK() {

    }

    public static void requestPermissions() {
        Intent intent = new Intent(App.getInstance(), PermissionActivity.class);
        App.getInstance().startActivity(intent);
    }

    public static void start(String consumerKey, String consumerSecret) {
        PilgrimSdk.with(
                new PilgrimSdk.Builder(App.getInstance())
                        .consumer(consumerKey, consumerSecret)
                        .notificationHandler(pilgrimNotificationHandler)
        );
        PilgrimSdk.start(App.getInstance());
    }

    private static PilgrimNotificationHandler pilgrimNotificationHandler = new PilgrimNotificationHandler() {
        @Override
        public void handlePlaceNotification(Context context, PilgrimSdkPlaceNotification pilgrimSdkPlaceNotification) {

        }
    };

}
