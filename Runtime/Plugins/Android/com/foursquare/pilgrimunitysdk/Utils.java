package com.foursquare.pilgrimunitysdk;

import com.foursquare.api.FoursquareLocation;
import com.foursquare.api.types.Category;
import com.foursquare.api.types.Photo;
import com.foursquare.api.types.Venue;
import com.foursquare.api.types.geofence.GeofenceEvent;
import com.foursquare.pilgrim.CurrentLocation;
import com.foursquare.pilgrim.PilgrimUserInfo;
import com.foursquare.pilgrim.Visit;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.Date;
import java.util.List;
import java.util.Map;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

final class Utils {

    private Utils() {

    }

    static String jsonFromUserInfo(@NonNull PilgrimUserInfo userInfo) {
        JSONArray keys = new JSONArray();
        JSONArray values = new JSONArray();

        for (Map.Entry<String, String> entry : userInfo.entrySet()) {
            keys.put(entry.getKey());
            values.put(entry.getValue());
        }

        try {
            JSONObject json = new JSONObject();
            json.put("_keys", keys);
            json.put("_values", values);
            return json.toString();
        } catch (JSONException e) {
            return null;
        }
    }

    static PilgrimUserInfo userInfoFromJSON(@NonNull String jsonString) {
        try {
            JSONObject json = new JSONObject(jsonString);
            JSONArray keys = json.getJSONArray("_keys");
            JSONArray values = json.getJSONArray("_values");

            if (keys.length() != values.length()) {
                return null;
            }

            PilgrimUserInfo userInfo = new PilgrimUserInfo();

            for (int i = 0; i < keys.length(); i++) {
                String key = keys.getString(i);
                String value = values.getString(i);

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
        } catch (JSONException e) {
            return null;
        }
    }

    static JSONObject currentLocationJson(@NonNull CurrentLocation currentLocation) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("_currentPlace", visitJson(currentLocation.getCurrentPlace()));

        JSONArray geofenceEvents = new JSONArray();
        for (GeofenceEvent event : currentLocation.getMatchedGeofences()) {
            geofenceEvents.put(geofenceEventJson(event));
        }
        json.put("_matchedGeofences", geofenceEvents);

        return json;
    }

    private static JSONObject visitJson(@NonNull Visit visit) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("_location", foursquareLocationJson(visit.getLocation()));

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
        json.put("_locationType", locationType);

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
        json.put("_confidence", confidence);

        json.put("_arrivalTime", visit.getArrival() / 1000);

        if (visit.getVenue() != null) {
            json.put("_venue", venueJson(visit.getVenue()));
        }

        JSONArray otherPossibleVenuesArray = new JSONArray();
        for (Venue venue : visit.getOtherPossibleVenues()) {
            otherPossibleVenuesArray.put(venueJson(venue));
        }
        json.put("_otherPossibleVenues", otherPossibleVenuesArray);

        return json;
    }

    private static JSONObject geofenceEventJson(@NonNull GeofenceEvent geofenceEvent) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("_id", geofenceEvent.getId());

        if (geofenceEvent.getVenue() != null) {
            Venue venue = geofenceEvent.getVenue();
            json.put("_venueId", venue.getId());
            json.put("_venue", venueJson(venue));
        }

        if (geofenceEvent.getPartnerVenueId() != null) {
            json.put("_partnerVenueId", geofenceEvent.getPartnerVenueId());
        }

        json.put("_location", locationJson(geofenceEvent.getLat(), geofenceEvent.getLng()));
        json.put("_timestamp", geofenceEvent.getTimestamp() / 1000);
        return json;
    }

    private static JSONObject chainJson(@NonNull Venue.VenueChain chain) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("_id", chain.getId());
        json.put("_name", chain.getName());
        return json;
    }

    private static JSONArray chainsArrayJson(@Nullable List<Venue.VenueChain> chains) throws JSONException {
        JSONArray json = new JSONArray();
        if (chains != null) {
            for (Venue.VenueChain chain : chains) {
                json.put(chainJson(chain));
            }
        }
        return json;
    }

    private static JSONArray categoryArrayJson(@NonNull List<Category> categories) throws JSONException {
        JSONArray json = new JSONArray();
        for (Category category : categories) {
            json.put(categoryJson(category));
        }
        return json;
    }

    private static JSONObject categoryJson(@NonNull Category category) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("_id", category.getId());
        json.put("_name", category.getName());

        if (category.getPluralName() != null) {
            json.put("_pluralName", category.getPluralName());
        }

        if (category.getShortName() != null) {
            json.put("_shortName", category.getShortName());
        }

        if (category.getImage() != null) {
            json.put("_icon", categoryIconJson(category.getImage()));
        }

        json.put("_isPrimary", category.isPrimary());

        return json;
    }

    private static JSONObject categoryIconJson(@NonNull Photo photo) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("_prefix", photo.getPrefix());
        json.put("_suffix", photo.getSuffix());
        return json;
    }

    private static JSONArray hierarchyJson(@Nullable List<Venue.VenueParent> hierarchy) throws JSONException {
        JSONArray json = new JSONArray();
        if (hierarchy != null) {
            for (Venue.VenueParent parent : hierarchy) {
                JSONObject parentJson = new JSONObject();
                parentJson.put("_id", parent.getId());
                parentJson.put("_name", parent.getName());
                parentJson.put("_categories", categoryArrayJson(parent.getCategories()));
                json.put(parentJson);
            }
        }
        return json;
    }

    private static JSONObject venueJson(@NonNull Venue venue) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("_id", venue.getId());
        json.put("_name", venue.getName());

        json.put("_locationInformation", venueLocationJson(venue.getLocation()));

        if (venue.getPartnerVenueId() != null) {
            json.put("_partnerVenueId", venue.getPartnerVenueId());
        }

        json.put("_probability", venue.getProbability());

        json.put("_chains", chainsArrayJson(venue.getVenueChains()));

        json.put("_categories", categoryArrayJson(venue.getCategories()));

        json.put("_hierarchy", hierarchyJson(venue.getHierarchy()));

        return json;
    }

    private static JSONObject venueLocationJson(@NonNull Venue.Location venueLocation) throws JSONException {
        JSONObject json = new JSONObject();

        if (venueLocation.getAddress() != null) {
            json.put("_address", venueLocation.getAddress());
        }
        if (venueLocation.getCrossStreet() != null) {
            json.put("_crossStreet", venueLocation.getCrossStreet());
        }
        if (venueLocation.getCity() != null) {
            json.put("_city", venueLocation.getCity());
        }
        if (venueLocation.getState() != null) {
            json.put("_state", venueLocation.getState());
        }
        if (venueLocation.getPostalCode() != null) {
            json.put("_postalCode", venueLocation.getPostalCode());
        }
        if (venueLocation.getCountry() != null) {
            json.put("_country", venueLocation.getCountry());
        }
        json.put("_location", locationJson(venueLocation.getLat(), venueLocation.getLng()));

        return json;
    }

    private static JSONObject foursquareLocationJson(@NonNull FoursquareLocation foursquareLocation)
            throws JSONException {
        return locationJson(foursquareLocation.getLat(), foursquareLocation.getLng());
    }

    private static JSONObject locationJson(double lat, double lng) throws JSONException {
        JSONObject json = new JSONObject();
        json.put("_latitude", lat);
        json.put("_longitude", lng);
        return json;
    }

}
