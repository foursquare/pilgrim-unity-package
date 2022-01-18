package com.foursquare.pilgrimunitysdk;

import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.os.Looper;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import com.foursquare.pilgrim.CurrentLocation;
import com.foursquare.pilgrim.PilgrimNotificationTester;
import com.foursquare.pilgrim.PilgrimSdk;
import com.foursquare.pilgrim.PilgrimUserInfo;
import com.foursquare.pilgrim.Result;
import com.foursquare.pilgrimsdk.debugging.PilgrimSdkDebugActivity;

@SuppressWarnings("unused")
public final class PilgrimClient {

    private final Context context;

    private final PilgrimClientListener listener;

    private final Handler handler = new Handler(Looper.getMainLooper());

    public PilgrimClient(@NonNull Context context, @NonNull final PilgrimClientListener listener) {
        this.context = context;
        this.listener = listener;
    }

    public String getUserInfo() {
        PilgrimUserInfo userInfo = PilgrimSdk.get().getUserInfo();
        if (userInfo != null) {
            return Utils.jsonFromUserInfo(userInfo);
        }
        return null;
    }

    public void setUserInfo(String userInfoJson, boolean persisted) {
        PilgrimUserInfo userInfo = null;
        if (userInfoJson != null) {
            userInfo = Utils.userInfoFromJSON(userInfoJson);
        }
        PilgrimSdk.get().setUserInfo(userInfo, persisted);
    }

    public void start() {
        PilgrimSdk.start(context);
    }

    public void stop() {
        PilgrimSdk.stop(context);
    }

    public void clearAllData() {
        PilgrimSdk.clearAllData(context);
    }

    public void getCurrentLocation() {
        new Thread(() -> {
            try {
                final Result<CurrentLocation, Exception> result = PilgrimSdk.get().getCurrentLocation();
                handler.post(() -> handleResult(result));
            } catch (final SecurityException | IllegalStateException e) {
                handler.post(() -> listener.onGetCurrentLocationResult(false, "", getErrorMessage(e)));
            }
        }).start();
    }

    public void showDebugScreen() {
        context.startActivity(new Intent(context, PilgrimSdkDebugActivity.class));
    }

    public void fireTestVisit(double latitude, double longitude) {
        PilgrimNotificationTester.sendTestVisitArrivalAtLocation(context, latitude, longitude, false);
    }

    private void handleResult(Result<CurrentLocation, Exception> result) {
        if (result.isOk()) {
            try {
                CurrentLocation currentLocation = result.getOrNull();
                if (currentLocation != null) {
                    String json = Utils.currentLocationJson(currentLocation).toString();
                    listener.onGetCurrentLocationResult(true, json, "");
                } else {
                    listener.onGetCurrentLocationResult(false, "", "Unknown Error");
                }
            } catch (Exception e) {
                listener.onGetCurrentLocationResult(false, "", getErrorMessage(e));
            }
        } else {
            listener.onGetCurrentLocationResult(false, "", getErrorMessage(result.getErr()));
        }
    }

    private static String getErrorMessage(@Nullable Exception e) {
        return (e != null && e.getMessage() != null) ? e.getMessage() : "Unknown Error";
    }

}
