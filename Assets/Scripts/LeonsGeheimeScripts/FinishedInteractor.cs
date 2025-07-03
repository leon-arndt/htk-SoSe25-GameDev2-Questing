using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LeonsGeheimeScripts
{
    public class FinishedInteractor : MonoBehaviour
    {
        [SerializeField] private InputActionReference interactionAction;
        [SerializeField] private float interactionRadius = 3f;
        [SerializeField] private LayerMask interactableLayer = 1;

        private FinishedInteractable _currentInteractable;
        private List<FinishedInteractable> _nearbyInteractables = new();

        private void Update()
        {
            DetectInteractables();
            CheckInteraction();
            UpdateUI();
        }

        private void CheckInteraction()
        {
            if (interactionAction.action.triggered && _currentInteractable != null)
            {
                GetComponent<Animator>().SetBool("Interact", true);
                Invoke("StopInteractionAnimation", 0.5f);
                _currentInteractable.Interact(transform);
            }
        }

        private void DetectInteractables()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, interactionRadius, interactableLayer);

            _nearbyInteractables.Clear();
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out FinishedInteractable interactable))
                {
                    _nearbyInteractables.Add(interactable);
                }
            }

            _currentInteractable = GetClosestInteractable();
        }

        private FinishedInteractable GetClosestInteractable()
        {
            FinishedInteractable closest = null;
            float closestDistance = float.MaxValue;

            foreach (var interactable in _nearbyInteractables)
            {
                float distance = Vector3.Distance(transform.position, ((MonoBehaviour)interactable).transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = interactable;
                }
            }

            return closest;
        }

        private void UpdateUI()
        {
            if (_currentInteractable != null)
            {
                // Display the interaction prompt
                FinishedInteractionUI.ShowPrompt(_currentInteractable.GetInteractionPrompt());
            }
            else
            {
                // Hide the interaction prompt
                FinishedInteractionUI.HidePrompt();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }
    }
}
