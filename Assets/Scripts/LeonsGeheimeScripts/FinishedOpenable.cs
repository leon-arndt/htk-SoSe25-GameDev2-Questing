using Data;
using Logic;
using UnityEngine;
using World;

namespace LeonsGeheimeScripts
{
    public class FinishedOpenable : Interactable
    {
        // requires
        [SerializeField] private ItemType requiredItem;
        [SerializeField] public uint requiredAmount = 1;
        [SerializeField] private bool shouldConsume = true;
        
        // gives
        [SerializeField] public ItemType givenItem;
        [SerializeField] private uint givenAmount;
        
        // state
        [SerializeField] private bool stayOpen = true;
        [SerializeField] private Animator animator;
        private bool _isOpen;
        
        public override void Interact(Transform _)
        {
            if (_isOpen)
            {
                return;
            }

            if (shouldConsume)
            {
                // 🍏🍏🍽️🍽️
                if (GameState.Get<InventoryState>().TryRemove(requiredItem, requiredAmount))
                {
                    Open();
                }
            }
            else
            {
                // 🔑🔑🗝️🔐
                if (GameState.Get<InventoryState>().Count(requiredItem) >= requiredAmount)
                {
                    Open();
                }
            }
        }

        public override string GetInteractionVerb()
        {
            return "Open";
        }

        private void Open()
        {
            _isOpen = true;
            if (animator != null)
            {
                animator.SetBool("isOpen", true);
            }

            if (givenAmount > 0)
            {
                GameState.Get<InventoryState>().Add(givenItem, givenAmount);
            }

            Debug.Log("Opened:" + gameObject.name);

            if (stayOpen)
            {
                Destroy(this);
            }
        }
    }
}