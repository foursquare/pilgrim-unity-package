package com.foursquare.pilgrimunitysdk;

import android.content.Context;
import android.content.SharedPreferences;
import android.support.annotation.NonNull;

import com.foursquare.pilgrim.PilgrimSdk;
import com.foursquare.pilgrim.PilgrimUserInfo;

import java.util.Date;
import java.util.Map;

@SuppressWarnings("unused")
public final class PilgrimUnitySDK {

    private PilgrimUnitySDK() {

    }

    public static void init(@NonNull Context context, @NonNull String consumerKey, @NonNull String consumerSecret) {
        PilgrimSdk.with(new PilgrimSdk.Builder(context).consumer(consumerKey, consumerSecret));
        setUserInfo(context);
        restartIfPreviouslyStarted(context);
    }

    private static void setUserInfo(@NonNull Context context) {
        SharedPreferences sharedPref = context.getSharedPreferences("userInfo", Context.MODE_PRIVATE);

        PilgrimUserInfo userInfo = new PilgrimUserInfo();
        for (Map.Entry<String, ?> entry : sharedPref.getAll().entrySet()) {
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

        PilgrimSdk.get().setUserInfo(userInfo);

        for (Map.Entry<String, String> entry : userInfo.entrySet()) {
            String key = entry.getKey();
            String value = entry.getValue();
        }
    }

    private static void restartIfPreviouslyStarted(@NonNull Context context) {
        SharedPreferences sharedPref = context.getSharedPreferences("general", Context.MODE_PRIVATE);
        boolean started = sharedPref.getBoolean("started", false);
        if (started) {
            PilgrimSdk.start(context);
        }
    }

}
