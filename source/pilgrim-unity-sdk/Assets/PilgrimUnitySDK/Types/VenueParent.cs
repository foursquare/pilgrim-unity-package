using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class VenueParent
    {

#pragma warning disable 0649

        [SerializeField]
        private string id;

        public string ID { get { return id; } }

        [SerializeField]
        private string name;

        public string Name { get { return name; } }

        [SerializeField]
        private Category[] categories;

        public IList<Category> Categories { get { return new List<Category>(categories); } }

#pragma warning restore 0649

    }

}