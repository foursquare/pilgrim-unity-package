package com.foursquare.pilgrimunitysdk;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Build;
import android.os.Bundle;

import androidx.activity.ComponentActivity;
import androidx.activity.result.ActivityResultCallback;
import androidx.activity.result.ActivityResultLauncher;
import androidx.activity.result.contract.ActivityResultContracts.RequestPermission;
import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;
import androidx.core.content.ContextCompat;

public class PermissionActivity extends ComponentActivity {

    public static final String LOCATION_PERMISSION_GRANTED = "com.foursquare.pilgrimunitysdk.LOCATION_PERMISSION_GRANTED";
    public static final String LOCATION_PERMISSION_SHOW_RATIONALE = "com.foursquare.pilgrimunitysdk.LOCATION_PERMISSION_SHOW_RATIONALE";

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        if (ContextCompat.checkSelfPermission(
                this, Manifest.permission.ACCESS_FINE_LOCATION) ==
                PackageManager.PERMISSION_GRANTED) {
            broadcastAndFinish(true);
            return;
        }

        ActivityResultLauncher<String> launcher = registerForActivityResult(new RequestPermission(),
                new ActivityResultCallback<Boolean>() {
                    @Override
                    public void onActivityResult(Boolean isGranted) {
                        broadcastAndFinish(isGranted);
                    }
                });
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
            requestPermissionsM(launcher);
        } else {
            launcher.launch(Manifest.permission.ACCESS_FINE_LOCATION);
        }
    }

    @RequiresApi(api = Build.VERSION_CODES.M)
    private void requestPermissionsM(ActivityResultLauncher<String> launcher) {
        if (shouldShowRequestPermissionRationale(Manifest.permission.ACCESS_FINE_LOCATION)) {
            sendBroadcast(new Intent(LOCATION_PERMISSION_SHOW_RATIONALE));
        } else {
            launcher.launch(Manifest.permission.ACCESS_FINE_LOCATION);
        }
    }

    private void broadcastAndFinish(boolean isGranted) {
        Intent intent = new Intent(LOCATION_PERMISSION_GRANTED);
        intent.putExtra("granted", isGranted);
        sendBroadcast(intent);
        finish();
    }

}
