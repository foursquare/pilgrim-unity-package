using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Log
    {

        [SerializeField]
        private string title;

        public string Title { get { return title; } }

        [SerializeField]
        private string description;

        public string Description { get { return description; } }

    }

}