using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class CategoryIcon
    {

        [SerializeField]
        private string prefix;

        public string Prefix { get { return prefix; } }

        [SerializeField]
        private string suffix;

        public string Suffix { get { return suffix; } }

    }

}