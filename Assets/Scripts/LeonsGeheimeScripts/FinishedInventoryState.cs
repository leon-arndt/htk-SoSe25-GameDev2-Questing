using System.Collections.Generic;
using Logic;

namespace LeonsGeheimeScripts
{
    public class FinishedInventoryState : SaveState
    {
        private Dictionary<FinishedItemType, uint> _items = new();

        public override void OnStartGame()
        {
            _items = new();
        }

        public override void OnEndGame()
        {
        }

        public IReadOnlyDictionary<FinishedItemType, uint> Get()
        {
            return _items;
        }
        
        public uint Count(FinishedItemType type)
        {
            if (type == null)
            {
                return 0;
            }

            if (_items.TryGetValue(type, out var amount))
            {
                return amount;
            }

            return 0;
        }

        public void Add(FinishedItemType type, uint amount)
        {
            if (type == null || amount == 0)
            {
                return;
            }

            if (_items.TryGetValue(type, out _))
            {
                _items[type] += amount;
            }
            else
            {
                _items[type] = amount;
            }
        }

        private bool TryRemove(FinishedItemType type, uint amount)
        {
            if (type == null || amount == 0)
            {
                return true;
            }
            
            if (_items.TryGetValue(type, out var currentAmount))
            {
                if (currentAmount >= amount)
                {
                    _items[type] = currentAmount - amount;
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}