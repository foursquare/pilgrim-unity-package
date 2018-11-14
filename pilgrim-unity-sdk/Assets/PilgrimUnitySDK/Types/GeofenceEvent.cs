using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class GeofenceEvent
    {
        
        [SerializeField]
        private Venue venue;

        public Venue Venue { get { return venue; } }

        // internal GeofenceEvent(Venue venue)
        // {
        //     this.venue = venue;
        // }

    }

}