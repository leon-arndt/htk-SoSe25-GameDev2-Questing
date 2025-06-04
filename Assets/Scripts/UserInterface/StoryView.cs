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

        public static StoryView
            Instance { get; private set; } // this is a singleton which means the entire game can access this

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
            ShowStory();
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

        /// <summary>
        /// The story is shown when the player STARTS talking to an NPC
        /// </summary>
        private void ShowStory()
        {
            gameObject.SetActive(true);
            DestroyOldChoices();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

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

        private void ShowStoryChunk(string chunkText)
        {
            storyText.text = chunkText;
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

        private void CloseStory()
        {
            _story = null;
            gameObject.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnClickChoiceButton(Choice choice)
        {
            _story.ChooseChoiceIndex(choice.index);
            ShowStory();
        }

        private Button CreateChoiceView(string choiceText, int index)
        {
            var choice = Instantiate(choicePrefab, choiceHolder.transform, false);
            if (index == 0)
            {
                choice.Select();
            }

            choice.GetComponentInChildren<TextMeshProUGUI>().text = choiceText;
            return choice;
        }

        private void HandleTags()
        {
            // example tag: "# startQuest quest1"
            foreach (var storyTag in _story.currentTags)
            {
                var parts = storyTag.Split(' ');
                if (parts.Length < 2)
                {
                    Debug.LogWarning(
                        $"Tag '{storyTag}' is not valid. It should be in the format '# command argument'.");
                    continue;
                }

                ;

                // [command] [argument]
                var command = parts[0];
                var argument = parts[1];

                switch (command)
                {
                    case "startQuest":
                        GameState.Get<QuestsState>().StartQuest(argument);
                        break;
                    case "completeQuest":
                        GameState.Get<QuestsState>().CompleteQuest(argument);
                        break;
                    case "addItem":
                        var item = _itemTypes.First(i =>
                            string.Equals(i.name, argument, StringComparison.OrdinalIgnoreCase));
                        GameState.Get<InventoryState>().Add(item, 1);
                        break;
                }
            }
        }
    }
}