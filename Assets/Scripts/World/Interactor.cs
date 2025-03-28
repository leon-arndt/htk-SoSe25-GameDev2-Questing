using System;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private float interactionDistance = 3f;

        private Interactable _currentInteractable;
        private List<Interactable> _nearbyInteractables = new();

        private void Update()
        {
            DetectNearbyInteractables();
            CheckInteraction();
            UpdateUi();
        }

        private void UpdateUi()
        {
            
        }

        private void DetectNearbyInteractables()
        {
            
        }
        
        private void CheckInteraction()
        {
            // TODO: bessere Lösung für Controller finden (neues Inputsystem nutzen)
            if (Input.GetKeyDown(KeyCode.E) && _currentInteractable != null)
            {
                // interagier mit dem interactable in der Nähe
                _currentInteractable.Interact(transform);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, interactionDistance);
        }
    }
}