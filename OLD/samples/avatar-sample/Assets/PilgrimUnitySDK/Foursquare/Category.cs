using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Category
    {
        
        [SerializeField]
        private string name;

        public string Name { get { return name; } }

        [SerializeField]
        private string icon;

        public string Icon { get { return icon; } }

        // TODO MOVE OUT OF FRAMEWORK HOST APP CAN EXTEND IF NEEDED
        int CompareTo(object obj)
        {
            return name.CompareTo(((Category)obj).name);
        }

    }

}