package com.foursquare.pilgrimunitysdk;

import android.content.Context;
import android.content.SharedPreferences;
import android.support.annotation.NonNull;

import com.foursquare.pilgrim.PilgrimSdk;
import com.foursquare.pilgrim.PilgrimUserInfo;

import java.util.Date;
import java.util.Map;

@SuppressWarnings("unused")
public final class PilgrimClient {

    private Context context;

    public PilgrimClient(@NonNull Context context) {
        this.context = context;
    }

    public void setUserInfo(@NonNull Map<String, String> userInfoMap) {
        PilgrimUserInfo userInfo = new PilgrimUserInfo();

        for (Map.Entry<String, String> entry : userInfoMap.entrySet()) {
            String key = entry.getKey();
            String value = entry.getValue();

            switch (key) {
                case "userId":
                    userInfo.setUserId(value);
                    break;
                case "birthday":
                    userInfo.setBirthday(new Date(Long.parseLong(value)));
                    break;
                case "gender":
                    switch (value) {
                        case "male":
                            userInfo.setGender(PilgrimUserInfo.Gender.MALE);
                            break;
                        case "female":
                            userInfo.setGender(PilgrimUserInfo.Gender.FEMALE);
                            break;
                    }
                    break;
                default:
                    userInfo.put(key, value);
                    break;
            }
        }

        PilgrimSdk.get().setUserInfo(userInfo);

        SharedPreferences sharedPref = context.getSharedPreferences("userInfo", Context.MODE_PRIVATE);
        sharedPref.edit().clear().apply();

        for (Map.Entry<String, String> entry : userInfo.entrySet()) {
            String key = entry.getKey();
            String value = entry.getValue();
            sharedPref.edit().putString(key, value).apply();
        }
    }

    public void start() {
        PilgrimSdk.start(context);

        SharedPreferences sharedPref = context.getSharedPreferences("general", Context.MODE_PRIVATE);
        sharedPref.edit().putBoolean("started", true).apply();
    }

    public void stop() {
        PilgrimSdk.stop(context);

        SharedPreferences sharedPref = context.getSharedPreferences("general", Context.MODE_PRIVATE);
        sharedPref.edit().putBoolean("started", false).apply();
    }

    public void clearAllData() {
        PilgrimSdk.clearAllData(context);
    }

}
