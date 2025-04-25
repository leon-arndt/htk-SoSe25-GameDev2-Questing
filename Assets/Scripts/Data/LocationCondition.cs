using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Data
{
    [Serializable]
    public class LocationCondition : IQuestCondition
    {
        [SerializeField] private LocationType locationType;
        [SerializeField] private float maxDistance = 5f;
        
        public bool IsFulfilled()
        {
            var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            
            var locationMatches = Object.FindObjectsByType<Location>(FindObjectsSortMode.None);
            foreach (var location in locationMatches)
            {
                if (location.Type == locationType)
                {
                    var distance = Vector3.Distance(playerPosition, location.transform.position);
                    if (distance <= maxDistance)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}