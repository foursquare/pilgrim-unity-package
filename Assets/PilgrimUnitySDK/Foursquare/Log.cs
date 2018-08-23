using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Log
    {

        public Log(string text) {
            this.text = text;
        }

        private string text;

        public string Text { get { return text; } }

    }

}