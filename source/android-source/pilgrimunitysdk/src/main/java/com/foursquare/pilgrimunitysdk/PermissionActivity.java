package com.foursquare.pilgrimunitysdk;

import android.Manifest;
import android.app.Activity;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;

public class PermissionActivity extends Activity {

    private int PERMISSION_REQUEST = 1000;

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        ActivityCompat.requestPermissions(this,
                new String[]{Manifest.permission.ACCESS_FINE_LOCATION}, PERMISSION_REQUEST);
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        finish();

        if (requestCode == PERMISSION_REQUEST) {
            Intent intent = new Intent("com.foursquare.pilgrimunitysdk.LOCATION_PERMISSION_GRANTED");
            if (grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                intent.putExtra("granted", true);
            } else {
                intent.putExtra("granted", false);
            }
            sendBroadcast(intent);
        }
    }

}
