using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Visit
    {

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

    }

}