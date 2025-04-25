using System.Collections.Generic;
using System.Linq;
using Logic;
using UnityEngine;

namespace LeonsGeheimeScripts
{
    public class FinishedQuestState : SaveState
    {
        private List<QuestState> _questStates = new();

        public override void OnStartGame()
        {
            _questStates = new List<QuestState>();
        }

        public override void OnEndGame()
        {
        }

        public void StartQuest(IQuest quest)
        {
            if (_questStates.Any(q => q.Quest.GetId() == quest.GetId()))
            {
                Debug.LogWarning($"Quest{quest.GetId()} already started - not starting it again");
                return;
            }
            
            var state = new QuestState()
            {
                Quest = quest,
                Status = QuestStatus.Started,
            };
            _questStates.Add(state);
            quest.OnQuestStart();
            
            Debug.Log("Quest " + quest.GetId() + " started");
        }
        
        public struct QuestState
        {
            public IQuest Quest;
            public QuestStatus Status;
        };

        public enum QuestStatus
        {
            Started = 0,
            Completed = 1
        }
    }

    public interface IQuest
    {
        string GetId();
        void OnQuestStart();
        public bool IsHidden();
        public string GetDescription();
    }
}