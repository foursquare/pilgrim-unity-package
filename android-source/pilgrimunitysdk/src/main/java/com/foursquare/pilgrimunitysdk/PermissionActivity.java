package com.foursquare.pilgrimunitysdk;

import android.Manifest;
import android.app.Activity;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

public final class PermissionActivity extends Activity {

    private int PERMISSION_REQUEST = 1000;

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Log.e("PermissionActivity", "onCreate");
        ActivityCompat.requestPermissions(this,
                new String[]{Manifest.permission.ACCESS_FINE_LOCATION}, PERMISSION_REQUEST);
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        Log.e("PermissionActivity", "onRequestPermissionsResult");
        finish();
        if (requestCode == PERMISSION_REQUEST) {
            if (grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                UnityPlayer.UnitySendMessage("_PilgrimCallbacks", "OnPermissionsGranted", "true");
            } else {
                UnityPlayer.UnitySendMessage("_PilgrimCallbacks", "OnPermissionsGranted", "false");
            }
        }
    }

}
