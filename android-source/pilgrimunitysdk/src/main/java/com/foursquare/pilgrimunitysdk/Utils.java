package com.foursquare.pilgrimunitysdk;

import android.content.Context;
import android.content.SharedPreferences;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;

import com.foursquare.pilgrim.PilgrimUserInfo;

import java.util.Date;
import java.util.Map;

final class Utils {

    private static final String PREFS_USER_INFO = "userInfo";

    private static final String PREFS_GENERAL = "general";
    private static final String KEY_STARTED = "started";

    private Utils() {
    }

    static PilgrimUserInfo fromMap(Map<String, ?> map) {
        if (map.isEmpty()) {
            return null;
        }

        PilgrimUserInfo userInfo = new PilgrimUserInfo();

        for (Map.Entry<String, ?> entry : map.entrySet()) {
            String key = entry.getKey();
            String value = (String) entry.getValue();

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

        return userInfo;
    }

    static void saveUserInfo(@NonNull Context context, @Nullable PilgrimUserInfo userInfo) {
        SharedPreferences sharedPref = context.getSharedPreferences(PREFS_USER_INFO, Context.MODE_PRIVATE);
        sharedPref.edit().clear().apply();

        if (userInfo == null) {
            return;
        }

        for (Map.Entry<String, String> entry : userInfo.entrySet()) {
            String key = entry.getKey();
            String value = entry.getValue();
            sharedPref.edit().putString(key, value).apply();
        }
    }

    static PilgrimUserInfo loadUserInfo(@NonNull Context context) {
        SharedPreferences sharedPref = context.getSharedPreferences(PREFS_USER_INFO, Context.MODE_PRIVATE);
        if (sharedPref.getAll().isEmpty()) {
            return null;
        }
        return fromMap(sharedPref.getAll());
    }

    static void setStarted(@NonNull Context context, boolean started) {
        SharedPreferences sharedPref = context.getSharedPreferences(PREFS_GENERAL, Context.MODE_PRIVATE);
        sharedPref.edit().putBoolean(KEY_STARTED, started).apply();
    }

    static boolean isStarted(@NonNull Context context) {
        SharedPreferences sharedPref = context.getSharedPreferences(PREFS_GENERAL, Context.MODE_PRIVATE);
        return sharedPref.getBoolean(KEY_STARTED, false);
    }

}
