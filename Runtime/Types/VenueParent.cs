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
        private string _id;

        public string ID { get { return _id; } }

        [SerializeField]
        private string _name;

        public string Name { get { return _name; } }

        [SerializeField]
        private Category[] _categories;

        public IList<Category> Categories { get { return new List<Category>(_categories); } }

#pragma warning restore 0649

    }

}