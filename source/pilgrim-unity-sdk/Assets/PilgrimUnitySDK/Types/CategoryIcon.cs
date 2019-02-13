using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class CategoryIcon
    {

#pragma warning disable 0649

        [SerializeField]
        private string prefix;

        public string Prefix { get { return prefix; } }

        [SerializeField]
        private string suffix;

        public string Suffix { get { return suffix; } }

#pragma warning restore 0649

    }

}