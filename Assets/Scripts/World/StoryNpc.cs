using UnityEngine;
using UserInterface;

namespace World
{
    /// <summary>
    /// This component is attached to different talking NPCs in the world.
    /// They give the player quests and information.
    /// </summary>
    public class StoryNpc : Interactable
    {
        [SerializeField] private TextAsset inkStory;

        public override void Interact(Transform interactor)
        {
            StoryView.Instance.StartStory(this, inkStory);
        }

        public override string GetInteractionVerb()
        {
            return "Talk";
        }
    }
}