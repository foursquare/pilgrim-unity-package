using Foursquare;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PilgrimBehavior : MonoBehaviour 
{

    [Serializable]
    public class LocationPermissionGrantedEvent : UnityEvent<bool> {}
    
    public LocationPermissionGrantedEvent onLocationPermissionGranted;

    [Serializable]
    public class GeofenceEvent : UnityEvent<List<Foursquare.GeofenceEvent>> {}

    public GeofenceEvent onGeofenceEvents;

    [Serializable]
    public class VisitEvent : UnityEvent<Visit> {}

    public VisitEvent onVisit;

    private static PilgrimBehavior instance = null;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);  
        }  
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GameObject callbackGO = new GameObject("_PilgrimCallbacks");
        callbackGO.transform.position = Vector3.zero;
        callbackGO.transform.rotation = Quaternion.identity;
        callbackGO.transform.localScale = Vector3.one;
        callbackGO.AddComponent<PilgrimCallbacks>();
    }

}
