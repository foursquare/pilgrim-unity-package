using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Venue
    {

        [SerializeField]        
        private string name;

        public string Name { get { return name; } }

        // internal Venue(string name)
        // {
        //     this.name = name;
        // }

    }

}