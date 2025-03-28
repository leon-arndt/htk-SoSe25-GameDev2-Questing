using System.Collections.Generic;
using LeonsGeheimeScripts;

namespace Logic
{
    public class InventoryState : SaveState
    {
        private Dictionary<FinishedItemType, uint> _items = new();

        protected override void OnStartGame()
        {
            _items = new();
        }

        protected override void OnEndGame()
        {
        }
        
        private void Add(FinishedItemType type, uint amount)
        {
            // TODO: implement
        }

        private bool TryRemove(FinishedItemType type, uint amount)
        {
            // TODO: implement
            return false;
        }
    }
}