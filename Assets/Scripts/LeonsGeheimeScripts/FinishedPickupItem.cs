using UnityEngine;

namespace LeonsGeheimeScripts
{
    public class FinishedPickupItem : FinishedInteractable
    {
        [SerializeField] private string itemName = "Default Item";

        public override string GetInteractionPrompt()
        {
            return $"Pick up {itemName}";
        }

        public override void Interact(Transform _)
        {
            Debug.Log($"Picked up {itemName}");
            Destroy(gameObject); // Remove the item from the world
        }
    }
}