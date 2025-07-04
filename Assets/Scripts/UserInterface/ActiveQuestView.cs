using Data;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    /// <summary>
    /// This is the UI for just one quest
    /// </summary>
    public class ActiveQuestView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI questNameText;

        [SerializeField]
        private GameObject questConditionsMetIndicator;

        private Quest _quest;
        
        public void SetQuest(Quest quest)
        {
            _quest = quest;
        }
        
        public void Update()
        {
            if (_quest == null)
            {
                return;
            }

            questNameText.text = _quest.GetDescription();
            bool conditionsMet = _quest.AreConditionsMet();
            questConditionsMetIndicator.SetActive(conditionsMet);
        }
    }
}