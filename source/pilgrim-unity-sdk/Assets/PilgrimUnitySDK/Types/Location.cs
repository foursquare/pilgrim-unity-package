using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Location
    {

#pragma warning disable 0649

        [SerializeField]
        private double _latitude;

        public double Latitude { get { return _latitude; } }

        [SerializeField]
        private double _longitude;

        public double Longitude { get { return _longitude; } }

#pragma warning restore 0649

        public Location(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

    }

}