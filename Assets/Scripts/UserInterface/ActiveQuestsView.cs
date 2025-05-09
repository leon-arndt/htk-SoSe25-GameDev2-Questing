using Data;
using UnityEngine;

namespace UserInterface
{
    /// <summary>
    /// This UI will show ALL of the active quests the player has
    /// </summary>
    public class ActiveQuestsView : MonoBehaviour
    {
        /// <summary>
        /// This is a singleton which means the entire game can access this
        /// </summary>
        private static ActiveQuestsView _instance;
        
        [SerializeField]
        private RectTransform questsLayoutGroup;
        
        [SerializeField]
        private ActiveQuestView questViewPrefab;

        private void Awake()
        {
            _instance = this;
        }

        public static void AddQuest(Quest quest)
        {
            var questView = Instantiate(_instance.questViewPrefab, _instance.questsLayoutGroup);
            questView.Refresh(quest);
        }
    }
}