using System;
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
        [SerializeField] private Image speakerPortrait;
        private Story _story;

        public static FinishedStoryView Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            DestroyOldChoices();
            gameObject.SetActive(false);
        }

        private void ShowStory()
        {
            DestroyOldChoices();

            // Read all the content until we can't continue any more
            while (_story.canContinue)
            {
                // Continue gets the next line of the story
                string text = _story.Continue();
                // This removes any white space from the text.
                text = text.Trim();
                CreateContentView(text); // Display the text on screen!
                HandleTags(); // For example: give new quests
            }
        }
        
        private void CreateContentView(string text)
        {
            var speaker = _host.type.startingState.characterName;
            speakerName.text = speaker;
            storyText.color = Color.white;
            if (_host.type.dialogColor != default)
            {
                storyText.color = _host.type.dialogColor;
            }
            
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
                    var quest = _quests.First(q =>
                        string.Equals(q.name, questName, StringComparison.OrdinalIgnoreCase));
                    GameState.Get<FinishedQuestState>().StartQuest(quest);
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
    }
}