using UnityEngine;

namespace World
{
    /// <summary>
    /// GameObjects die vom Spieler aufgehoben werden, z.B. Äpfel, Münzen, etc.
    /// </summary>
    public class ItemPickup : Interactable
    {
        public override void Interact(Transform interactor)
        {
            Debug.Log("Item wurde aufgehoben" + gameObject.name);
        }
    }
}