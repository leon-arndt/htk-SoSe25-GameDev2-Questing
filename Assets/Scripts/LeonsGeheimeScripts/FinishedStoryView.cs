using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Ink.Runtime;
using Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Cinematic;

namespace LeonsGeheimeScripts
{
    /// <summary>
    /// Show Ink stories
    /// </summary>
    public class FinishedStoryView : MonoBehaviour
    {
        [SerializeField] private RectTransform choiceHolder;
        [SerializeField] private TextMeshProUGUI storyText;
        [SerializeField] private TextMeshProUGUI speakerName;
        [SerializeField] private FinishedDialogChoiceView buttonPrefab;
        private Story _story;
        private ItemType[] _itemTypes;
        
        public static FinishedStoryView Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            DestroyOldChoices();
            gameObject.SetActive(false);
            
            _itemTypes = Resources.LoadAll<ItemType>("Items");
        }

        public void StartStory(FinishedStoryCharacter character, TextAsset textAsset)
        {
            _story = new Story(textAsset.text);
            speakerName.text = character.CharacterName;
            
            SetStoryVariables("completable", GameState.Get<FinishedQuestState>().GetCompletableQuests());
            SetStoryVariables("completed", GameState.Get<FinishedQuestState>().GetCompletedQuests());
            SetStoryVariables("started", GameState.Get<FinishedQuestState>().GetStartedQuests());
            ShowStory();
        }
        
        private void SetStoryVariables(string prefix, IReadOnlyList<FinishedQuestState.QuestState> quests)
        {
            foreach (var quest in quests)
            {
                var varName = $"{prefix}_{quest.FinishedQuest.GetId().ToLower()}";
                if (_story.variablesState.Contains(varName))
                {
                    _story.variablesState[varName] = true;
                }
            }
        }
        
        private void ShowStory()
        {
            DestroyOldChoices();

            // Read all the content until we can't continue any more
            while (_story.canContinue)
            {
                // Continue gets the next line of the story
                string chunkText = _story.Continue();
                // This removes any white space from the text.
                chunkText = chunkText.Trim();
                ShowStoryChunk(chunkText); // Display the text on screen!
                HandleTags(); // For example: give new quests
            }
        }
        
        private void ShowStoryChunk(string text)
        {
            storyText.text = text;
            DestroyOldChoices();
            if (_story.currentChoices.Count > 0)
            {
                for (int i = 0; i < _story.currentChoices.Count; i++)
                {
                    Choice choice = _story.currentChoices[i];
                    Button button = CreateChoiceView($"{choice.text.Trim()}", i);
                    // Tell the button what to do when we press it
                    button.onClick.AddListener(() => OnClickChoiceButton(choice));
                }
            }
            else
            {
                Button choice = CreateChoiceView("Continue", 0);
                choice.onClick.AddListener(CloseStory);
            }
        }
        
        private void OnClickChoiceButton(Choice choice)
        {
            _story.ChooseChoiceIndex(choice.index);
            ShowStory();
        }
        
        private void HandleTags()
        {
            foreach (var storyTag in _story.currentTags)
            {
                var parts = storyTag.Split(' ');
                if (parts.Length < 2) continue;

                var command = parts[0];
                var arg = parts[1];

                switch (command)
                {
                    case "addQuest":
                        GameState.Get<FinishedQuestState>().StartQuest(arg);
                        break;
                    case "removeQuest":
                        GameState.Get<FinishedQuestState>().RemoveQuest(arg);
                        break;
                    case "completeQuest":
                        GameState.Get<FinishedQuestState>().CompleteQuest(arg);
                        break;
                    case "addItem":
                        var item = _itemTypes.First(i =>
                            string.Equals(i.name, arg, StringComparison.OrdinalIgnoreCase));
                        GameState.Get<InventoryState>().Add(item, 1);
                        break;
                }
            }
        }
        
        private Button CreateChoiceView(string text, int index)
        {
            var choice = Instantiate(buttonPrefab, choiceHolder.transform, false);
            if (index == 0)
            {
                choice.Select();
            }

            choice.Set(text, index + 1);
            return choice.Button;
        }
        
        private void DestroyOldChoices()
        {
            foreach (Transform child in choiceHolder)
            {
                Destroy(child.gameObject);
            }
        }

        private void CloseStory()
        {
            gameObject.SetActive(false);
            _story = null;
        }
    }
}