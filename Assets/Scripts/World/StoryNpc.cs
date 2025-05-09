using Data;
using Logic;
using UnityEngine;

namespace World
{
    /// <summary>
    /// This component is attached to different talking NPCs in the world.
    /// They give the player quests and information.
    /// </summary>
    public class StoryNpc : Interactable
    {
        [SerializeField] private TextAsset inkStory;

        // TODO: delete this once we show our ink stories which will start the quest
        [SerializeField] private Quest placeholderQuest;

        public override void Interact(Transform interactor)
        {
            GameState.Get<QuestsState>().StartQuest(placeholderQuest.GetId());
        }

        public override string GetInteractionVerb()
        {
            return "Talk";
        }
    }
}