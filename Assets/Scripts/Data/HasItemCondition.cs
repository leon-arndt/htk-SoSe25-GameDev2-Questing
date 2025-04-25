using System;
using Logic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class HasItemCondition : IQuestCondition
    {
        [SerializeField] private ItemType type;
        [SerializeField] private uint amount;
        
        public bool IsFulfilled()
        {
            uint count = GameState.Get<InventoryState>().Count(type); // the player has 5 apples
            
            // return if this is enough?
            return count >= amount;
        }
    }
}