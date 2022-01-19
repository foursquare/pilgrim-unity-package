using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Category
    {

#pragma warning disable 0649

        [SerializeField]
        private string _id;

        public string ID { get { return _id; } }

        [SerializeField]
        private string _name;

        public string Name { get { return _name; } }

        [SerializeField]
        private string _pluralName;

        public string PluralName { get { return _pluralName; } }

        [SerializeField]
        private string _shortName;

        public string ShortName { get { return _shortName; } }

        [SerializeField]
        private CategoryIcon _icon;

        public CategoryIcon Icon { get { return _icon; } }

        [SerializeField]
        private bool _isPrimary;

        public bool IsPrimary { get { return _isPrimary; } }

#pragma warning restore 0649

    }

}
