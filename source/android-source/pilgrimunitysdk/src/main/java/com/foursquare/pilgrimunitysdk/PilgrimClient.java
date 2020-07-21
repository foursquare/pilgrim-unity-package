package com.foursquare.pilgrimunitysdk;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Handler;
import android.support.annotation.NonNull;

import com.foursquare.pilgrim.CurrentLocation;
import com.foursquare.pilgrim.PilgrimNotificationTester;
import com.foursquare.pilgrim.PilgrimSdk;
import com.foursquare.pilgrim.PilgrimUserInfo;
import com.foursquare.pilgrim.Result;
import com.foursquare.pilgrimsdk.debugging.PilgrimSdkDebugActivity;

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
            public void onReceive(Context context, final Intent intent) {
                handler.post(new Runnable() {
                    @Override
                    public void run() {
                        boolean granted = intent.getBooleanExtra("granted", false);
                        listener.onLocationPermissionResult(granted);
                    }
                });
            }
        }, new IntentFilter(PermissionActivity.LOCATION_PERMISSION_GRANTED));

        context.registerReceiver(new BroadcastReceiver() {
            @Override
            public void onReceive(Context context, Intent intent) {
                handler.post(new Runnable() {
                    @Override
                    public void run() {
                        listener.onLocationPermissionShowRationale();
                    }
                });
            }
        }, new IntentFilter(PermissionActivity.LOCATION_PERMISSION_SHOW_RATIONALE));
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

    public void requestLocationPermissions() {
        Intent intent = new Intent(context, PermissionActivity.class);
        context.startActivity(intent);
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
                } catch (final SecurityException e) {
                    handler.post(new Runnable() {
                        @Override
                        public void run() {
                            listener.onGetCurrentLocationResult(false, "", getErrorMessage(e));
                        }
                    });
                } catch (final IllegalStateException e) {
                    handler.post(new Runnable() {
                        @Override
                        public void run() {
                            listener.onGetCurrentLocationResult(false, "", getErrorMessage(e));
                        }
                    });
                }
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

    private static String getErrorMessage(Exception e) {
        return e.getMessage() != null ? e.getMessage() : "Unknown Error";
    }

}
