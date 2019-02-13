using UnityEngine;

namespace Foursquare
{

    [CreateAssetMenu(fileName = "Location", menuName = "Pilgrim/Mock Current Location")]
    public class CurrentLocationMock : ScriptableObject
    {

#pragma warning disable 0649

        [SerializeField]
        private CurrentLocation _currentLocation;

        public CurrentLocation CurrentLocation { get { return _currentLocation; } }

#pragma warning restore 0649

    }

}