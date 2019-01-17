using UnityEngine;

namespace Foursquare
{

    [CreateAssetMenu(fileName = "Location", menuName = "Pilgrim/Mock Current Location")]
    public class CurrentLocationMock : ScriptableObject
    {

        [SerializeField]
        private CurrentLocation _currentLocation;

        public CurrentLocation CurrentLocation { get { return _currentLocation; } }

    }

}