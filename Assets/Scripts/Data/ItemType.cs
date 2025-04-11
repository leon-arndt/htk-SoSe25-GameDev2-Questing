using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class ItemType : ScriptableObject
    {
        // the item's name: useful for quests
        public string itemName = "Item Name Placeholder";
        
        // the icon used for the item, for example in the UI
        public Sprite icon;

        // the value of the item, e.g. for trading
        public uint marketPrice = 1;
    }
}