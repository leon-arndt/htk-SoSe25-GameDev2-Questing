using UnityEngine;
using UnityEngine.Events;

namespace FinishedScripts
{
    public class TriggerReaction : MonoBehaviour
    {
        public UnityEvent reaction;

        // diese funktion/Methode wird aufgerufen wenn ein anderes GameObject in diesen Collider hineinläuft
        private void OnTriggerEnter(Collider other)
        {
            reaction?.Invoke();
        }
    }
}