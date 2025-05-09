using System;
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
            
            foreach (var quest in GameState.Get<FinishedQuestState>().GetCompletableQuests())
            {
                var varName = "completable_" + quest.FinishedQuest.GetId().ToLower();
                if (_story.variablesState.Contains(varName))
                {
                    _story.variablesState[varName] = true;
                }
            }

            foreach (var quest in GameState.Get<FinishedQuestState>().GetCompletedQuests())
            {
                var varName = "completed_" + quest.FinishedQuest.GetId().ToLower();
                if (_story.variablesState.Contains(varName))
                {
                    _story.variablesState[varName] = true;
                }
            }

            foreach (var quest in GameState.Get<FinishedQuestState>().GetStartedQuests())
            {
                var varName = "started_" + quest.FinishedQuest.GetId().ToLower();
                if (_story.variablesState.Contains(varName))
                {
                    _story.variablesState[varName] = true;
                }
            }
            
            ShowStory();
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
            if (_story.currentTags.Count <= 0)
            {
                return;
            }

            foreach (var currentTag in _story.currentTags)
            {
                if (currentTag.Contains("addQuest"))
                {
                    var questName = currentTag.Split(' ')[1];
                    GameState.Get<FinishedQuestState>().StartQuest(questName);
                }

                if (currentTag.Contains("removeQuest"))
                {
                    var questName = currentTag.Split(' ')[1];
                    GameState.Get<FinishedQuestState>().RemoveQuest(questName);
                }

                if (currentTag.Contains("completeQuest"))
                {
                    var questName = currentTag.Split(' ')[1];
                    GameState.Get<FinishedQuestState>().CompleteQuest(questName);
                }

                if (currentTag.Contains("addItem"))
                {
                    var itemType = currentTag.Split(' ')[1];
                    var itemData = _itemTypes.First(i =>
                        string.Equals(i.name, itemType, StringComparison.OrdinalIgnoreCase));
                    GameState.Get<InventoryState>().Add(itemData, 1);
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
        
        public void CloseStory()
        {
            gameObject.SetActive(false);
            _story = null;
        }
    }
}