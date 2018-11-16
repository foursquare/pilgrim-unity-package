package com.foursquare.pilgrimunitysdk;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Build;
import android.support.annotation.NonNull;

import com.foursquare.pilgrim.PilgrimSdk;
import com.foursquare.pilgrim.PilgrimUserInfo;

import java.util.HashMap;

@SuppressWarnings("unused")
public final class PilgrimClient {

    private Context context;

    private PilgrimClientListener listener;

    public PilgrimClient(@NonNull Context context, @NonNull final PilgrimClientListener listener) {
        this.context = context;
        this.listener = listener;

        context.registerReceiver(new BroadcastReceiver() {
            @Override
            public void onReceive(Context context, Intent intent) {
                boolean granted = intent.getBooleanExtra("granted", false);
                listener.onLocationPermissionResult(granted);
            }
        }, new IntentFilter("com.foursquare.pilgrimunitysdk.LOCATION_PERMISSION_GRANTED"));
    }

    public void setUserInfo(@NonNull HashMap<String, String> userInfoMap) {
        PilgrimUserInfo userInfo = Utils.fromMap(userInfoMap);
        if (userInfo != null) {
            PilgrimSdk.get().setUserInfo(userInfo);
        }
        Utils.saveUserInfo(context, userInfo);
    }

    public void requestLocationPermissions() {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
            Intent intent = new Intent(context, PermissionActivity.class);
            context.startActivity(intent);
        } else {
            listener.onLocationPermissionResult(true);
        }
    }

    public void start() {
        PilgrimSdk.start(context);
        Utils.setStarted(context, true);
    }

    public void stop() {
        PilgrimSdk.stop(context);
        Utils.setStarted(context, false);
    }

    public void clearAllData() {
        PilgrimSdk.clearAllData(context);
    }

}
