package com.foursquare.pilgrimunitysdk;

import android.content.Context;
import android.content.SharedPreferences;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;

import com.foursquare.api.FoursquareLocation;
import com.foursquare.api.types.Category;
import com.foursquare.api.types.GeofenceEvent;
import com.foursquare.api.types.Photo;
import com.foursquare.api.types.Venue;
import com.foursquare.pilgrim.CurrentLocation;
import com.foursquare.pilgrim.PilgrimUserInfo;
import com.foursquare.pilgrim.Visit;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.Date;
import java.util.List;
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

        int locationType;
        switch (visit.getType()) {
            case HOME:
                locationType = 1;
                break;
            case WORK:
                locationType = 2;
                break;
            case VENUE:
                locationType = 3;
                break;
            default:
                locationType = 0;
                break;
        }
        json.put("locationType", locationType);

        int confidence;
        switch (visit.getConfidence()) {
            case LOW:
                confidence = 1;
                break;
            case MEDIUM:
                confidence = 2;
                break;
            case HIGH:
                confidence = 3;
                break;
            default:
                confidence = 0;
                break;
        }
        json.put("confidence", confidence);

        json.put("arrivalTime", visit.getArrival() / 1000);

        if (visit.getVenue() != null) {
            json.put("venue", venueJson(visit.getVenue()));
        }

        if (visit.getOtherPossibleVenues() != null) {
            JSONArray otherPossibleVenuesArray = new JSONArray();
            for (Venue venue : visit.getOtherPossibleVenues()) {
                otherPossibleVenuesArray.put(venueJson(venue));
            }
            json.put("otherPossibleVenues", otherPossibleVenuesArray);
        }

        return json;
    }

    private static JSONObject geofenceEventJson(@NonNull GeofenceEvent geofenceEvent) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("venueId", geofenceEvent.getVenueId());

        if (geofenceEvent.getCategoryIds() != null) {
            JSONArray categoryIdsJson = new JSONArray();
            for (String categoryId : geofenceEvent.getCategoryIds()) {
                categoryIdsJson.put(categoryId);
            }
            json.put("categoryIds", categoryIdsJson);
        }

        if (geofenceEvent.getChainIds() != null) {
            JSONArray chainIdsJson = new JSONArray();
            for (String chainId : geofenceEvent.getChainIds()) {
                chainIdsJson.put(chainId);
            }
            json.put("chainIds", chainIdsJson);
        }

        if (geofenceEvent.getPartnerVenueId() != null) {
            json.put("partnerVenueId", geofenceEvent.getPartnerVenueId());
        }

        json.put("venue", venueJson(geofenceEvent.getVenue()));
        json.put("location", locationJson(geofenceEvent.getLat(), geofenceEvent.getLng()));
        json.put("timestamp", geofenceEvent.getTimestamp() / 1000);
        return json;
    }

    private static JSONObject chainJson(@NonNull Venue.VenueChain chain) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("id", chain.getId());
        json.put("name", chain.getName());
        return json;
    }

    private static JSONArray categoryArrayJson(@NonNull List<Category> categories) throws JSONException {
        JSONArray categoriesJson = new JSONArray();
        for (Category category : categories) {
            categoriesJson.put(categoryJson(category));
        }
        return categoriesJson;
    }

    private static JSONObject categoryJson(@NonNull Category category) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("id", category.getId());
        json.put("name", category.getName());

        if (category.getPluralName() != null) {
            json.put("pluralName", category.getPluralName());
        }

        if (category.getShortName() != null) {
            json.put("shortName", category.getShortName());
        }

        if (category.getImage() != null) {
            json.put("icon", categoryImageJson(category.getImage()));
        }

        json.put("isPrimary", category.isPrimary());

        return json;
    }

    private static JSONObject categoryImageJson(@NonNull Photo photo) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("prefix", photo.getPrefix());
        json.put("suffix", photo.getSuffix());
        return json;
    }

    private static JSONObject venueJson(@NonNull Venue venue) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("id", venue.getId());
        json.put("name", venue.getName());

        if (venue.getLocation() != null) {
            json.put("locationInformation", venueLocationJson(venue.getLocation()));
        }

        if (venue.getPartnerVenueId() != null) {
            json.put("partnerVenueId", venue.getPartnerVenueId());
        }

        json.put("probability", venue.getProbability());

        JSONArray chains = new JSONArray();
        for (Venue.VenueChain chain : venue.getVenueChains()) {
            chains.put(chainJson(chain));
        }
        json.put("chains", chains);

        json.put("categories", categoryArrayJson(venue.getCategories()));

        JSONArray hierarchy = new JSONArray();
        for (Venue.VenueParent parent : venue.getHierarchy()) {
            JSONObject parentJson = new JSONObject();
            parentJson.put("id", parent.getId());
            parentJson.put("name", parent.getName());
            parentJson.put("categories", categoryArrayJson(venue.getCategories()));
        }
        json.put("hierarchy", hierarchy);

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
