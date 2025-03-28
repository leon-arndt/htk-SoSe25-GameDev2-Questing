using UnityEngine;

namespace LeonsGeheimeScripts
{
    public class FinishedStoryNpc : FinishedInteractable
    {
        public override void Interact(Transform interactor)
        {
            Debug.Log($"talk to npc{gameObject.name}");
        }

        public override string GetInteractionPrompt()
        {
            return "Talk";
        }
    }
}