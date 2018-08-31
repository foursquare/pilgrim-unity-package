using System;
using UnityEngine;

namespace Foursquare
{

    [Serializable]
    public class Visit
    {
        
        [SerializeField]
		private bool isArrival;

		public bool IsArrival { get { return isArrival; } }

        [SerializeField]
		private Venue venue;

		public Venue Venue { get { return venue; } }

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