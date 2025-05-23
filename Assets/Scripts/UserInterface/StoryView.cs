using System;
using Data;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    }
}