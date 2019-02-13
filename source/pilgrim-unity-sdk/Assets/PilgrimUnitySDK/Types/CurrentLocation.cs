using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class CurrentLocation
    {

#pragma warning disable 0649

        [SerializeField]
        private Visit currentPlace;

        public Visit CurrentPlace { get { return currentPlace; } }

        [SerializeField]
        private GeofenceEvent[] matchedGeofences;

        public IList<GeofenceEvent> MatchedGeofences { get { return new List<GeofenceEvent>(matchedGeofences); } }

#pragma warning restore 0649

    }

}