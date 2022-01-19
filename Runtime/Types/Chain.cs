using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Chain
    {

#pragma warning disable 0649

        [SerializeField]
        private string _id;

        public string ID { get { return _id; } }

        [SerializeField]
        private string _name;

        public string Name { get { return _name; } }

#pragma warning restore 0649

    }

}