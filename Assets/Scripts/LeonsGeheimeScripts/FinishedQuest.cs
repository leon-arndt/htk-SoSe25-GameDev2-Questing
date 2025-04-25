using System.Collections.Generic;
using UnityEngine;

namespace LeonsGeheimeScripts
{
    [CreateAssetMenu]
    public class FinishedFinishedQuest : ScriptableObject, IFinishedQuest
    {
        public bool isHidden;
        public string displayDescription;
        public List<IFinishedQuestCondition> conditions;
        
        public string GetId()
        {
            return name;
        }

        public void OnQuestStart()
        {
        }

        public bool IsHidden()
        {
            return isHidden;
        }

        public string GetDescription()
        {
            return displayDescription;
        }
        
        public IReadOnlyList<IFinishedQuestCondition> GetConditions()
        {
            return conditions;
        }

        public bool IsMet()
        {
            foreach (var condition in conditions)
            {
                if (condition == null)
                {
                    return true;
                }

                if (!condition.IsMet())
                {
                    return false;
                }
            }

            return true;
        }
    }

    public interface IFinishedQuestCondition
    {
        bool IsMet();
    }
}