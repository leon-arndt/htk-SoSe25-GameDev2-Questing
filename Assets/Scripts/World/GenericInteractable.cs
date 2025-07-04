using Data;
using Logic;
using UnityEngine;
using UnityEngine.Events;

namespace World
{
    /// <summary>
    /// This interactable will be used to offer many different types of interactions.
    /// For example:
    /// a button which opens a door, a lever which activates a trap, or a special gate which costs a key to open
    /// </summary>
    public class GenericInteractable : Interactable
    {
        [SerializeField] private UnityEvent onInteract;

        // Some interactables might require an item to be used
        [SerializeField] private ItemType requiredItem;
        [SerializeField] private uint requiredItemAmount;
        [SerializeField] private bool shouldConsume;

        public override void Interact(Transform interactor)
        {
            if (shouldConsume)
            {
                // Try to consume the required item
                if (GameState.Get<InventoryState>().TryRemove(requiredItem, requiredItemAmount))
                {
                    ExecuteInteraction();
                }
            }
            else
            {
                // item is checked but not consumed
                if (requiredItem == null)
                {
                    // If no item is required, just execute the interaction
                    ExecuteInteraction();
                    return;
                }

                // Check if the player has enough of the required item
                if (GameState.Get<InventoryState>().Count(requiredItem) >= requiredItemAmount)
                {
                    ExecuteInteraction();
                }
            }
        }

        private void ExecuteInteraction()
        {
            onInteract?.Invoke();
        }

        public override string GetInteractionVerb()
        {
            return "Interact";
        }
    }
}