using UnityEngine;

namespace Data
{
    public class Location : MonoBehaviour
    {
        [SerializeField]
        private LocationType locationType;

        public LocationType Type => locationType;
    }
}