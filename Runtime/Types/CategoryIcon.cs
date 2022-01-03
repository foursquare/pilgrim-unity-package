using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class CategoryIcon
    {

#pragma warning disable 0649

        [SerializeField]
        private string _prefix;

        public string Prefix { get { return _prefix; } }

        [SerializeField]
        private string _suffix;

        public string Suffix { get { return _suffix; } }

#pragma warning restore 0649

    }

}