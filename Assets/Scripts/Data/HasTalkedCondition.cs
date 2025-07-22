using System;
using UnityEngine;
using UserInterface;

namespace Data
{
    [Serializable]
    public class HasTalkedCondition : IQuestCondition
    {
        [SerializeField] private string npcId;
        
        public bool IsFulfilled()
        {
            var instance = StoryView.Instance;
            if (instance == null)
            {
                Debug.LogWarning("StoryView instance is null, cannot check if talked to NPC.");
                return false;
            }

            return instance.metSpeakers.Contains(npcId);
        }
    }
}