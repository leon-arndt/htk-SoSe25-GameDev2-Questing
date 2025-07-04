using Data;
using Logic;
using UnityEngine;
using UnityEngine.Events;
using World;

namespace LeonsGeheimeScripts
{
    public class FinishedGenericInteractable : Interactable
    {
        [SerializeField] private UnityEvent onInteract;
        [SerializeField] private ItemType requiredItem;
        [SerializeField] private uint requiredAmount = 1;
        [SerializeField] private bool shouldConsume = true;

        public override void Interact(Transform interactor)
        {
            if (shouldConsume)
            {
                // Try to consume the required item
                if (GameState.Get<InventoryState>().TryRemove(requiredItem, requiredAmount))
                {
                    ExecuteInteraction();
                }
            }
            else
            {
                if (requiredItem == null)
                {
                    // If no item is required, just execute the interaction
                    ExecuteInteraction();
                    return;
                }

                // Check if the player has enough of the required item
                if (GameState.Get<InventoryState>().Count(requiredItem) >= requiredAmount)
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