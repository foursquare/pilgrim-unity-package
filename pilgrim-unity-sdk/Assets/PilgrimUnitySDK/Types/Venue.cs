using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Venue
    {

        [SerializeField]
        private string id;

        public string ID { get { return id; } }

        [SerializeField]        
        private string name;

        public string Name { get { return name; } }

        [SerializeField]
        private VenueLocation location;

        public VenueLocation Location { get { return location; } }

    }

}