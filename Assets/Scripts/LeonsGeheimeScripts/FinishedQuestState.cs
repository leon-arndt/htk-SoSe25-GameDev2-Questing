using System;
using System.Collections.Generic;
using System.Linq;
using Logic;
using UnityEngine;

namespace LeonsGeheimeScripts
{
    public class FinishedQuestState : SaveState
    {
        private List<QuestState> _questStates = new();
        private List<FinishedFinishedQuest> _allPossibleQuests = new();

        public override void OnStartGame()
        {
            _questStates = new List<QuestState>();
            _allPossibleQuests = Resources.LoadAll<FinishedFinishedQuest>("Quests").ToList();
        }

        public override void OnEndGame()
        {
        }

        public void StartQuest(string questId)
        {
            if (_questStates.Any(q => q.FinishedQuest.GetId().Equals(questId, StringComparison.OrdinalIgnoreCase)))
            {
                Debug.LogWarning($"Quest{questId} already started - not starting it again");
                return;
            }
            
            var quest = _allPossibleQuests.FirstOrDefault(q => q.GetId().Equals(questId, StringComparison.OrdinalIgnoreCase));

            var state = new QuestState()
            {
                FinishedQuest = quest,
                Status = QuestStatus.Started,
            };
            _questStates.Add(state);
            quest.OnQuestStart();

            Debug.Log("Quest " + quest.GetId() + " started");
        }

        public struct QuestState
        {
            public IFinishedQuest FinishedQuest;
            public QuestStatus Status;
        };

        public enum QuestStatus
        {
            Started = 0,
            Completed = 1
        }

        public void CompleteQuest(string questId)
        {
            var match = _questStates.Find(q => q.FinishedQuest.GetId().Equals(questId, StringComparison.OrdinalIgnoreCase));
            if (match.Status == QuestStatus.Completed)
            {
                Debug.LogWarning($"Quest {questId} already completed - not completing it again");
                return;
            }

            if (!match.FinishedQuest.IsMet())
            {
                Debug.LogWarning($"Quest {questId} not met - not completing it");
                return;
            }

            match.Status = QuestStatus.Completed;
            Debug.Log("Quest " + questId + " completed");
        }

        public IReadOnlyList<QuestState> GetCompletableQuests()
        {
            return _questStates.Where(x => x.Status == QuestStatus.Started && x.FinishedQuest.IsMet()).ToList();
        }

        public IReadOnlyList<QuestState> GetStartedQuests()
        {
            return _questStates.Where(x => x.Status == QuestStatus.Started).ToList();
        }

        public IReadOnlyList<QuestState> GetCompletedQuests()
        {
            return _questStates.Where(q => q.Status == QuestStatus.Completed).ToList();
        }
    }

    public interface IFinishedQuest
    {
        string GetId();
        void OnQuestStart();
        public bool IsHidden();
        public string GetDescription();
        bool IsMet();
    }
}