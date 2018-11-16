package com.foursquare.pilgrimunitysdk;

import android.content.Context;
import android.content.SharedPreferences;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;

import com.foursquare.api.FoursquareLocation;
import com.foursquare.api.types.GeofenceEvent;
import com.foursquare.api.types.Venue;
import com.foursquare.pilgrim.CurrentLocation;
import com.foursquare.pilgrim.PilgrimUserInfo;
import com.foursquare.pilgrim.Visit;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

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

    static JSONObject currentLocationJson(@NonNull CurrentLocation currentLocation) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("currentPlace", visitJson(currentLocation.getCurrentPlace()));

        JSONArray geofenceEvents = new JSONArray();
        for (GeofenceEvent event : currentLocation.getMatchedGeofences()) {
            geofenceEvents.put(geofenceEventJson(event));
        }
        json.put("matchedGeofences", geofenceEvents);

        return json;
    }

    private static JSONObject visitJson(@NonNull Visit visit) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("location", foursquareLocationJson(visit.getLocation()));
        json.put("arrivalTime", visit.getArrival() / 1000);

        if (visit.getVenue() != null) {
            json.put("venue", venueJson(visit.getVenue()));
        }

        return json;
    }

    private static JSONObject geofenceEventJson(@NonNull GeofenceEvent geofenceEvent) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("venue", venueJson(geofenceEvent.getVenue()));
        json.put("location", locationJson(geofenceEvent.getLat(), geofenceEvent.getLng()));
        json.put("timestamp", geofenceEvent.getTimestamp() / 1000);
        return json;
    }

    private static JSONObject venueJson(@NonNull Venue venue) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("id", venue.getId());
        json.put("name", venue.getName());

        if (venue.getLocation() != null) {
            json.put("location", venueLocationJson(venue.getLocation()));
        }

        return json;
    }

    private static JSONObject venueLocationJson(@NonNull Venue.Location venueLocation) throws JSONException {
        JSONObject json = new JSONObject();

        if (venueLocation.getAddress() != null) {
            json.put("address", venueLocation.getAddress());
        }
        if (venueLocation.getCrossStreet() != null) {
            json.put("crossStreet", venueLocation.getCrossStreet());
        }
        if (venueLocation.getCity() != null) {
            json.put("city", venueLocation.getCity());
        }
        if (venueLocation.getState() != null) {
            json.put("state", venueLocation.getState());
        }
        if (venueLocation.getPostalCode() != null) {
            json.put("postalCode", venueLocation.getPostalCode());
        }
        if (venueLocation.getCountry() != null) {
            json.put("country", venueLocation.getCountry());
        }
        json.put("coordinate", locationJson(venueLocation.getLat(), venueLocation.getLng()));

        return json;
    }

    private static JSONObject foursquareLocationJson(@NonNull FoursquareLocation foursquareLocation) throws JSONException {
        return locationJson(foursquareLocation.getLat(), foursquareLocation.getLng());
    }

    private static JSONObject locationJson(double lat, double lng) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("latitude", lat);
        json.put("longitude", lng);
        return json;
    }

}
