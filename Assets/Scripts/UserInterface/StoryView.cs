using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Ink.Runtime;
using Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using World;

namespace UserInterface
{
    public class StoryView : MonoBehaviour
    {
        [SerializeField] private RectTransform choiceHolder;
        [SerializeField] private TextMeshProUGUI storyText;
        [SerializeField] private TextMeshProUGUI speakerName;
        [SerializeField] private Button choicePrefab;
        
        private Story _story; // this is the Ink story
        private ItemType[] _itemTypes; // we need an array of all item types so dialog can grant items
        
        public static StoryView Instance { get; private set; } // this is a singleton which means the entire game can access this

        private void Awake()
        {
            Instance = this;
            DestroyOldChoices(); 
            gameObject.SetActive(false); 
            _itemTypes = Resources.LoadAll<ItemType>("Items");
        }

        private void DestroyOldChoices()
        {
            foreach (Transform child in choiceHolder)
            {
                Destroy(child.gameObject);
            }
        }
        
        public void StartStory(StoryNpc character, TextAsset inkStory)
        {
            _story = new Story(inkStory.text);
            speakerName.text = character.gameObject.name;

            SetStoryVariables("completed", GameState.Get<QuestsState>().GetAllCompletedQuests());
            SetStoryVariables("started", GameState.Get<QuestsState>().GetAllStartedQuests());
            // TODO: ShowStory();
        }

        private void SetStoryVariables(string prefix, IReadOnlyList<QuestsState.QuestState> quests)
        {
            foreach (var quest in quests)
            {
                // for example: "completed_quest1" will be set to true or "started_quest1" will be set to true
                var varName = $"{prefix}_{quest.Quest.GetId().ToLower()}";
                if (_story.variablesState.Contains(varName))
                {
                    _story.variablesState[varName] = true;
                }
            }
        }
    }
}