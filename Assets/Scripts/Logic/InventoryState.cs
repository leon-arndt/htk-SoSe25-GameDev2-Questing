using System.Collections.Generic;
using Data;

namespace Logic
{
    /// <summary>
    /// This class manages all of the player's items
    /// </summary>
    public class InventoryState : SaveState
    {
        // {
        // Coins: 5,
        // HealthPotion: 5,
        // Sword: 1
        // }
        private Dictionary<ItemType, uint> _items = new();
        
        public override void OnStartGame()
        {
            _items.Clear();
        }
        
        public override void OnEndGame()
        {
            
        }

        /// <summary>
        /// IReadOnlyDictionaries cannot be changed because they are read-only
        /// </summary>
        /// <returns></returns>
        public IReadOnlyDictionary<ItemType, uint> Get()
        {
            return _items;
        }

        
        // Question: how many items of a certain type do we have?
        // E.g. how many coins do I have? -> Answer is uint, e.g. 5
        public uint Count(ItemType type)
        {
            if (type == null)
            {
                return 0;
            }
            
            if (_items.TryGetValue(type, out var amount))
            {
                return amount;
            }

            // the player does not have this item at all
            return 0;
        }

        public void Add(ItemType type, uint amount)
        {
            if (type == null)
            {
                return;
            }
            
            if (amount == 0)
            {
                return;
            }
            
            if (_items.TryGetValue(type, out _))
            {
                // the player already has this item
                // increase amount, e.g. 5 + 1 -> 6
                _items[type] += amount;
            }
            else
            {
                // the player does not have this item yet
                _items[type] = amount;
            }
        }

        // quests and NPC dialogs may remove items, but this won't always work
        public bool TryRemove(ItemType type, uint amount)
        {
            if (amount == 0)
            {
                return true;
            }

            if (type == null)
            {
                return true;
            }
            
            if (_items.TryGetValue(type, out var currentAmount))
            {
                if (currentAmount >= amount)
                {
                    // the player has enough items to remove. 7 - 2 => 5
                    _items[type] -= amount;
                    return true;
                }

                // the player has the item but not enough
                return false;
            }

            // the player does not have this item at all
            return false;
        }
    }
}