using UnityEngine;

namespace LeonsGeheimeScripts
{
    [CreateAssetMenu]
    public class FinishedItemType : ScriptableObject
    {
        [SerializeField] private string itemName = "Item Name";
        [SerializeField] private Sprite itemSprite;
        [SerializeField] private uint marketPrice = 1;

        public string ItemName => itemName;
        public Sprite ItemSprite => itemSprite;
        public uint MarketPrice => marketPrice;
    }
}