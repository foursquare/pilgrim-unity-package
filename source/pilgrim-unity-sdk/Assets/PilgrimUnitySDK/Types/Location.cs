using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Location
    {

        [SerializeField]
        private double latitude;

        public double Latitude { get { return latitude; } }

        [SerializeField]
        private double longitude;

        public double Longitude { get { return longitude; } }

    }

}