using UnityEngine;

namespace LeonsGeheimeScripts
{
    public class FinishedDoor : FinishedInteractable
    {
        [SerializeField] private bool isOpen;
        [SerializeField] private Animator animator;

        private static readonly int IsOpen = Animator.StringToHash("IsOpen");

        public override void Interact(Transform interactor)
        {
            isOpen = !isOpen;
            animator.SetBool(IsOpen, isOpen);
        }
    }
}