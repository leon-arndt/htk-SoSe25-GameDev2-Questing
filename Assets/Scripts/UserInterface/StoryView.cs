using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Ink.Runtime;
using Logic;
using StarterAssets;
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

        private Story _story;
        private ItemType[] _itemTypes;
        private string _lastChoiceText;
        private ThirdPersonController _playerController;

        public static StoryView Instance { get; private set; }
        public List<string> metSpeakers = new();

        private void Awake()
        {
            Instance = this;
            DestroyOldChoices();
            gameObject.SetActive(false);
            _itemTypes = Resources.LoadAll<ItemType>("Items");
            _playerController = FindAnyObjectByType<ThirdPersonController>();
        }

        private void Update()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _playerController.enabled = false;
        }

        public void StartStory(StoryNpc character, TextAsset inkStory)
        {
            _story = new Story(inkStory.text);
            speakerName.text = character.gameObject.name;
            metSpeakers.Add(character.gameObject.name);

            SetStoryVariables("completed", GameState.Get<QuestsState>().GetAllCompletedQuests());
            SetStoryVariables("started", GameState.Get<QuestsState>().GetAllStartedQuests());
            SetStoryVariables("completable", GameState.Get<QuestsState>().GetAllCompletableQuests());

            ShowNext();
        }

        private void SetStoryVariables(string prefix, IReadOnlyList<QuestsState.QuestState> quests)
        {
            foreach (var quest in quests)
            {
                var varName = $"{prefix}_{quest.Quest.GetId().ToLower()}";
                if (_story.variablesState.Contains(varName))
                {
                    _story.variablesState[varName] = true;
                }
            }
        }

        private void ShowNext()
        {
            gameObject.SetActive(true);
            DestroyOldChoices();

            if (_story.canContinue)
            {
                var text = _story.Continue().Trim();

                // Skip echo line that repeats choice
                if (!string.IsNullOrEmpty(_lastChoiceText) && text == _lastChoiceText)
                {
                    _lastChoiceText = null;
                    text = _story.canContinue ? _story.Continue().Trim() : "";
                }
                else
                {
                    _lastChoiceText = null;
                }

                ShowChunk(text);
                HandleTags();
            }
            else if (_story.currentChoices.Count > 0)
            {
                ShowChoices();
            }
            else
            {
                ShowCloseButton();
            }
        }

        private void ShowChunk(string text)
        {
            storyText.text = text;
            DestroyOldChoices();

            if (_story.canContinue)
            {
                CreateButton("Continue", ShowNext);
            }
            else if (_story.currentChoices.Count > 0)
            {
                ShowChoices();
            }
            else
            {
                ShowCloseButton();
            }
        }

        private void ShowChoices()
        {
            DestroyOldChoices();

            for (int i = 0; i < _story.currentChoices.Count; i++)
            {
                var choice = _story.currentChoices[i];
                CreateButton(choice.text.Trim(), () => OnChoiceSelected(choice), i == 0);
            }
        }

        private void OnChoiceSelected(Choice choice)
        {
            _lastChoiceText = choice.text.Trim();
            _story.ChooseChoiceIndex(choice.index);
            ShowNext();
        }

        private void ShowCloseButton()
        {
            DestroyOldChoices();
            CreateButton("Continue", CloseStory);
        }

        private void CloseStory()
        {
            _story = null;
            gameObject.SetActive(false);

            if (GameState.Get<QuestsState>().IsFinalQuestFinished())
            {
                GameState.EndGame();
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _playerController.enabled = true;
        }

        private void HandleTags()
        {
            foreach (var tag in _story.currentTags)
            {
                var parts = tag.Split(' ');
                if (parts.Length < 2)
                {
                    Debug.LogWarning($"Tag '{tag}' is invalid. Expected format '# command argument'.");
                    continue;
                }

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

        private void DestroyOldChoices()
        {
            foreach (Transform child in choiceHolder)
            {
                Destroy(child.gameObject);
            }
        }

        private void CreateButton(string text, Action onClick, bool select = false)
        {
            var button = Instantiate(choicePrefab, choiceHolder);
            button.GetComponentInChildren<TextMeshProUGUI>().text = text;
            button.onClick.AddListener(() => onClick());

            if (select)
            {
                button.Select();
            }
        }
    }
}