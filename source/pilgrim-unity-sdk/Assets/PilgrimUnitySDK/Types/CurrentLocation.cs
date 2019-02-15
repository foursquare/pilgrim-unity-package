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
        private Visit _currentPlace;

        public Visit CurrentPlace { get { return _currentPlace; } }

        [SerializeField]
        private GeofenceEvent[] _matchedGeofences;

        public IList<GeofenceEvent> MatchedGeofences { get { return new List<GeofenceEvent>(_matchedGeofences); } }

#pragma warning restore 0649

    }

}