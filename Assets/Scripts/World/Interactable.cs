using UnityEngine;

namespace World
{
    /// <summary>
    /// Mit diesen Unity GameObjects wird interagiert, z.B. ein Item das vom Spieler aufgehoben wird.
    /// </summary>
    public abstract class Interactable : MonoBehaviour
    {
        public abstract void Interact(Transform interactor);
        
        /// <summary>
        /// Was ist das Verb? Open, Pickup, Buy, etc.
        /// </summary>
        /// <returns></returns>
        public abstract string GetInteractionVerb();
    }
}