using UnityEngine;
using World;

namespace LeonsGeheimeScripts
{
    public class FinishedStoryCharacter : Interactable
    {
        [SerializeField] private string characterName;
        public string CharacterName => characterName;

        [SerializeField] private TextAsset story;
        public override void Interact(Transform interactor)
        {
            FinishedStoryView.Instance.StartStory(this, story);
        }

        public override string GetInteractionVerb()
        {
            return "Talk";
        }
    }
}