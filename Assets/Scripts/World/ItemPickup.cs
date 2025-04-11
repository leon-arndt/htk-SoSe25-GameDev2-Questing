using Data;
using Logic;
using UnityEngine;

namespace World
{
    /// <summary>
    /// GameObjects die vom Spieler aufgehoben werden, z.B. Äpfel, Münzen, etc.
    /// </summary>
    public class ItemPickup : Interactable
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private uint amount = 1;
        
        public override void Interact(Transform interactor)
        {
            Debug.Log("Item wurde aufgehoben" + gameObject.name);
            GameState.Get<InventoryState>().Add(itemType, amount);
            Destroy(gameObject);
        }

        public override string GetInteractionVerb()
        {
            return "Pickup" + gameObject.name;
        }
    }
}