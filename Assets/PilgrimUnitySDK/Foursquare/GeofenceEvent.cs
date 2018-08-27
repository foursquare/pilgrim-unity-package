using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foursquare
{

	[Serializable]
    public class GeofenceEvent
	{

		public enum Type {
			Entrance,
			Dwell,
			Exit
		}

		[SerializeField]
		private string eventType;

		public Type EventType { 
			get {
				switch (eventType) {
				case "entrance":
					return Type.Entrance;
				case "dwell":
					return Type.Dwell;
				default:
					return Type.Exit;
				}
			}
		}

		[SerializeField]
		private string venueID;

		public string VenueID { get { return venueID; } }

		[SerializeField]
		private string[] categoryIDs = new string[0];

		public List<string> CategoryIDs { get { return new List<string>(categoryIDs); } }

		[SerializeField]
		private string[] chainIDs = new string[0];

		public List<string> ChainIDs { get { return new List<string>(chainIDs); } }

		[SerializeField]
		private string partnerVenueID;

		public string PartnerVenueID { get { return partnerVenueID; } }

		[SerializeField]
		private Venue venue;

		public Venue Venue { get { return venue; } }

		[SerializeField]
		private Location location;

		public Location Location { get { return location; } }

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