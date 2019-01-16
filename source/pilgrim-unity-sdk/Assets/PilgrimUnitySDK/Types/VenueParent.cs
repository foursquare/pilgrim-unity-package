using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class VenueParent
    {

        [SerializeField]
        private string id;

        public string ID { get { return id; } }

        [SerializeField]
        private string name;

        public string Name { get { return name; } }

        [SerializeField]
        private Category[] categories;

        public IList<Category> Categories { get { return new List<Category>(categories); } }

    }

}