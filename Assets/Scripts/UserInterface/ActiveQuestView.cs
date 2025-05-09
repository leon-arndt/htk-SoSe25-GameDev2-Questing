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
        
        public void Refresh(Quest quest)
        {
            questNameText.text = quest.GetDescription();
        }
    }
}