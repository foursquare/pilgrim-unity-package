using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Visit
    {

#pragma warning disable 0649

        [SerializeField]
        private Location location;

        public Location Location { get { return location; } }

        [SerializeField]
        private double arrivalTime;

        public DateTime ArrivalTime { get { return arrivalTime.DateTimeFromUnixTime(); } }

        [SerializeField]
        private LocationType locationType;

        public LocationType LocationType { get { return locationType; } }

        [SerializeField]
        private Confidence confidence;

        public Confidence Confidence { get { return confidence; } }

        [SerializeField]
        private Venue venue;

        public Venue Venue { get { return venue; } }

#pragma warning restore 0649

    }

}