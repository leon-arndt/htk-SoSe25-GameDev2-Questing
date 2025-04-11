using Logic;
using UnityEngine;

namespace LeonsGeheimeScripts
{
    public class FinishedPickupItem : FinishedInteractable
    {
        [SerializeField] private FinishedItemType itemType;
        [SerializeField] private uint amount = 1;

        public override string GetInteractionPrompt()
        {
            return $"Pick up {itemType.name}";
        }

        public override void Interact(Transform _)
        {
            FinishedGameState.Get<FinishedInventoryState>().Add(itemType, amount);
            Destroy(gameObject);
        }
    }
}