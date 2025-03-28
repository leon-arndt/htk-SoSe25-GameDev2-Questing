using System.Collections.Generic;
using LeonsGeheimeScripts;

namespace Logic
{
    public class FinishedInventoryState : SaveState
    {
        private Dictionary<FinishedItemType, uint> _items = new();

        protected override void OnStartGame()
        {
            _items = new();
        }

        protected override void OnEndGame()
        {
        }

        public IReadOnlyDictionary<FinishedItemType, uint> Get()
        {
            return _items;
        }

        private void Add(FinishedItemType type, uint amount)
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