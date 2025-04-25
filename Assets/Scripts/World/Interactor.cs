using System.Collections.Generic;
using UnityEngine;
using UserInterface;

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
            if (_currentInteractable != null)
            {
                // es gibt ein interactable
                InteractionView.ShowPrompt("Press E: " + _currentInteractable.GetInteractionVerb());
            }
            else
            {
                InteractionView.HidePrompt();
            }
        }

        private void DetectNearbyInteractables()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, interactionDistance); // schauen was um mich herum ist: Wände, Interactables, etc.
            _nearbyInteractables.Clear(); // Interactable Liste leer machen
            foreach (var hit in hits)
            {
                // jeden "hit" prüfen ob es ein Interactable ist oder nicht. Eine normale Wand ist kein Interactable
                if (hit.TryGetComponent(out Interactable interactable))
                {
                    // dieser collider ist definitiv ein interactable
                    _nearbyInteractables.Add(interactable);
                }
            }

            _currentInteractable = GetClosestInteractable();
        }

        private Interactable GetClosestInteractable()
        {
            Interactable closest = null;
            float closestDistance = float.MaxValue;

            foreach (var interactable in _nearbyInteractables)
            {
                // schauen ob dieses GameObject näher ist als das bis-jetzt nächste
                // distance ist z.B. 3 meter, etc. - vom Spieler (transform.position) entfernt
                float distance = Vector3.Distance(transform.position, interactable.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = interactable;
                }
            }

            // entweder null (kein Interactable) oder das nächste Interactable
            return closest;
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