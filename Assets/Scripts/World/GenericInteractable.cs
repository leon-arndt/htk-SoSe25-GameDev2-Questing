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

        public override void Interact(Transform interactor)
        {
            onInteract?.Invoke();
        }

        public override string GetInteractionVerb()
        {
            return "Interact";
        }
    }
}