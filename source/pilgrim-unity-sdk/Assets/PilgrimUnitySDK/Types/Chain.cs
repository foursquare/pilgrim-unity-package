using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Chain
    {

#pragma warning disable 0649

        [SerializeField]
        private string id;

        public string ID { get { return id; } }

        [SerializeField]
        private string name;

        public string Name { get { return name; } }

#pragma warning restore 0649

    }

}