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

        [SerializeField]
		private double timestamp;

		public DateTime Timestamp { 
			get { 
				DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
				return epoch.AddSeconds(timestamp).ToLocalTime();
			} 
		}

    }

}