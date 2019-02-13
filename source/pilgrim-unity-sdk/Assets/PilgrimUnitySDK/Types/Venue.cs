using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Venue
    {

#pragma warning disable 0649

        [SerializeField]
        private string id;

        public string ID { get { return id; } }

        [SerializeField]
        private string name;

        public string Name { get { return name; } }

        [SerializeField]
        private VenueLocation locationInformation;

        public VenueLocation LocationInformation { get { return locationInformation; } }

        [SerializeField]
        private string partnerVenueId;

        public string PartnerVenueId { get { return partnerVenueId; } }

        [SerializeField]
        private double probability;

        public double Probability { get { return probability; } }

        [SerializeField]
        private Chain[] chains;

        public IList<Chain> Chains { get { return new List<Chain>(chains); } }

        [SerializeField]
        private Category[] categories;

        public IList<Category> Categories { get { return new List<Category>(categories); } }

        [SerializeField]
        private VenueParent[] hierarchy;

        public IList<VenueParent> Hierarchy { get { return new List<VenueParent>(hierarchy); } }

#pragma warning restore 0649

    }

}