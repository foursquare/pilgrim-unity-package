using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Visit
    {

#pragma warning disable 0649

        [SerializeField]
        private Location _location;

        public Location Location { get { return _location; } }

        [SerializeField]
        private LocationType _locationType;

        public LocationType LocationType { get { return _locationType; } }

        [SerializeField]
        private Confidence _confidence;

        public Confidence Confidence { get { return _confidence; } }

        [SerializeField]
        private double _arrivalTime;

        public DateTime ArrivalTime { get { return _arrivalTime.DateTimeFromUnixTime(); } }

        [SerializeField]
        private Venue _venue;

        public Venue Venue { get { return _venue; } }

        [SerializeField]
        private Venue[] _otherPossibleVenues;

        public IList<Venue> OtherPossibleVenues { get { return new List<Venue>(_otherPossibleVenues); } }

#pragma warning restore 0649

    }

}