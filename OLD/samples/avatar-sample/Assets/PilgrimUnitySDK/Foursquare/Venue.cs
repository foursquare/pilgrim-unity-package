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
		private Category category;

		public Category Category { get { return category; } }

	}

}