using System;
using UnityEngine;

namespace Foursquare
{

	[Serializable]
    public class Location
	{

		[SerializeField]
		private double lat;

		public double Lat { get { return lat; } }

		[SerializeField]
		private double lng;

		public double Lng { get { return lng; } }

		[SerializeField]
		private double hacc;

		public double Hacc { get { return hacc; } }

	}

}