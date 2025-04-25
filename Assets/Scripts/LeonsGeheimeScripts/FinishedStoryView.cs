using System;
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

        public static FinishedStoryView Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            DestroyOldChoices();
            gameObject.SetActive(false);
        }

        public void Show()
        {
            
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