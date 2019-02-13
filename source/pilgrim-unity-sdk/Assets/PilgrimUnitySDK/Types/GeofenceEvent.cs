using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class GeofenceEvent
    {

#pragma warning disable 0649

        [SerializeField]
        private Venue venue;

        public Venue Venue { get { return venue; } }

        [SerializeField]
        private Location location;

        public Location Location { get { return location; } }

        [SerializeField]
        private double timestamp;

        public DateTime Timestamp { get { return timestamp.DateTimeFromUnixTime(); } }

#pragma warning restore 0649

    }

}