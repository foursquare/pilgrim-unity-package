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
        private string _id;

        public string ID { get { return _id; } }

        [SerializeField]
        private string _name;

        public string Name { get { return _name; } }

        [SerializeField]
        private VenueLocation _locationInformation;

        public VenueLocation LocationInformation { get { return _locationInformation; } }

        [SerializeField]
        private string _partnerVenueId;

        public string PartnerVenueId { get { return _partnerVenueId; } }

        [SerializeField]
        private double _probability;

        public double Probability { get { return _probability; } }

        [SerializeField]
        private Chain[] _chains;

        public IList<Chain> Chains { get { return new List<Chain>(_chains); } }

        [SerializeField]
        private Category[] _categories;

        public IList<Category> Categories { get { return new List<Category>(_categories); } }

        [SerializeField]
        private VenueParent[] _hierarchy;

        public IList<VenueParent> Hierarchy { get { return new List<VenueParent>(_hierarchy); } }

#pragma warning restore 0649

    }

}