using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class VenueLocation
    {

#pragma warning disable 0649

        [SerializeField]
        private string _address;

        public string Address { get { return _address; } }

        [SerializeField]
        private string _crossStreet;

        public string CrossStreet { get { return _crossStreet; } }

        [SerializeField]
        private string _city;

        public string City { get { return _city; } }

        [SerializeField]
        private string _state;

        public string State { get { return _state; } }

        [SerializeField]
        private string _postalCode;

        public string PostalCode { get { return _postalCode; } }

        [SerializeField]
        private string _country;

        public string Country { get { return _country; } }

        [SerializeField]
        private Location _location;

        public Location Location { get { return _location; } }

#pragma warning restore 0649

    }

}