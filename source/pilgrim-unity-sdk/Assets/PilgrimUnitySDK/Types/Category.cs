using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Category
    {

        [SerializeField]
        private string id;

        public string ID { get { return id; } }

        [SerializeField]
        private string name;

        public string Name { get { return name; } }

        [SerializeField]
        private string pluralName;

        public string PluralName { get { return pluralName; } }

        [SerializeField]
        private string shortName;

        public string ShortName { get { return shortName; } }

        [SerializeField]
        private CategoryIcon icon;

        public CategoryIcon Icon { get { return icon; } }

        [SerializeField]
        private bool isPrimary;

        public bool IsPrimary { get { return isPrimary; } }

    }



}
