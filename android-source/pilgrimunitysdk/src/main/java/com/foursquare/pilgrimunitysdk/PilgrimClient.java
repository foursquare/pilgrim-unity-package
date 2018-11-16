package com.foursquare.pilgrimunitysdk;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Build;
import android.os.Handler;
import android.support.annotation.NonNull;

import com.foursquare.pilgrim.CurrentLocation;
import com.foursquare.pilgrim.PilgrimSdk;
import com.foursquare.pilgrim.PilgrimUserInfo;
import com.foursquare.pilgrim.Result;

import org.json.JSONException;

import java.util.HashMap;

@SuppressWarnings("unused")
public final class PilgrimClient {

    private Context context;

    private PilgrimClientListener listener;

    private Handler handler = new Handler();

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

    public void getCurrentLocation() {
        new Thread(new Runnable() {
            @Override
            public void run() {
                try {
                    final Result<CurrentLocation, Exception> result = PilgrimSdk.get().getCurrentLocation();
                    handler.post(new Runnable() {
                        @Override
                        public void run() {
                            handleResult(result);
                        }
                    });
                } catch (SecurityException e) {
                    handler.post(new Runnable() {
                        @Override
                        public void run() {
                            listener.onGetCurrentLocationResult(false, null);
                        }
                    });
                }
            }
        }).start();
    }

    private void handleResult(Result<CurrentLocation, Exception> result) {
        if (result.isOk()) {
            try {
                CurrentLocation currentLocation = result.getOrNull();
                if (currentLocation != null) {
                    String json = Utils.currentLocationJson(currentLocation).toString();
                    listener.onGetCurrentLocationResult(true, json);
                } else {
                    listener.onGetCurrentLocationResult(false, null);
                }
            } catch (JSONException e) {
                listener.onGetCurrentLocationResult(false, null);
            }
        } else {
            listener.onGetCurrentLocationResult(false, null);
        }
    }

}
