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
        private long arrivalTime;

        public long ArrivalTime { get { return arrivalTime; } }

        [SerializeField]
        private Venue venue;

        public Venue Venue { get { return venue; } }

        // internal Visit(Location location, long arrivalTime, Venue venue)
        // {
        //     this.location = location;
        //     this.arrivalTime = arrivalTime;
        //     this.venue = venue;
        // }

    }

}