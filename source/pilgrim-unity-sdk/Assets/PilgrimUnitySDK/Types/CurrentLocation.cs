using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class CurrentLocation
    {
        
        [SerializeField]
        private Visit currentPlace;

        public Visit CurrentPlace { get { return currentPlace; } }

        [SerializeField]
        private GeofenceEvent[] matchedGeofences;

        public IList<GeofenceEvent> MatchedGeofences { get { return new List<GeofenceEvent>(matchedGeofences); } }

    }

}