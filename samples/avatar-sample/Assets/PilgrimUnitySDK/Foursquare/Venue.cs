using System;
using UnityEngine;

namespace Foursquare
{

	[Serializable]
    public class Venue
	{

		[SerializeField]
		private string name;

		public string Name { get { return name; } }

		[SerializeField]
		private string category;

		public string Category { get { return category; } }

	}

}