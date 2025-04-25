using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class Quest : ScriptableObject, IQuest
    {
        // describes in a few words what the player has to do: e.g. collect 20 coins
        [SerializeField] private string description;

        // some quests should not be shown in the UI
        [SerializeField] private bool isHidden;
        
        [SerializeField, SubclassSelector]
        private List<IQuestCondition> conditions;

        public bool AreConditionsMet()
        {
            foreach (var condition in conditions)
            {
                if (condition == null)
                {
                    return true;
                }

                if (!condition.IsFulfilled())
                {
                    return false;
                }
            }

            return true;
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetId()
        {
            return name;
        }
    }
}