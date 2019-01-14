using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Chain
    {

        [SerializeField]
        private string id;

        public string ID { get { return id; } }

        [SerializeField]
        private string name;

        public string Name { get { return name; } }

    }

}