using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Logic;
using UnityEngine;

namespace LeonsGeheimeScripts
{
    public class FinishedQuestState : SaveState
    {
        private List<QuestState> _questStates = new();
        private List<FinishedQuest> _allPossibleQuests = new();

        public override void OnStartGame()
        {
            _questStates = new List<QuestState>();
            _allPossibleQuests = Resources.LoadAll<FinishedQuest>("Quests").ToList();
        }

        public override void OnEndGame()
        {
        }

        public void StartQuest(string questId)
        {
            if (_questStates.Any(q => q.Quest.GetId().Equals(questId, StringComparison.OrdinalIgnoreCase)))
            {
                Debug.LogWarning($"Quest{questId} already started - not starting it again");
                return;
            }
            
            var quest = _allPossibleQuests.FirstOrDefault(q => q.GetId().Equals(questId, StringComparison.OrdinalIgnoreCase));

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

        public void RemoveQuest(string questId)
        {
            var match = _questStates.Find(q => q.Quest.GetId().Equals(questId, StringComparison.OrdinalIgnoreCase));
            _questStates.Remove(match);
        }

        public void CompleteQuest(string questName)
        {
            var match = _questStates.Find(q => q.Quest.GetId().Equals(questName, StringComparison.OrdinalIgnoreCase));
            if (match.Status == QuestStatus.Completed)
            {
                Debug.LogWarning($"Quest {questName} already completed - not completing it again");
                return;
            }

            if (!match.Quest.IsMet())
            {
                Debug.LogWarning($"Quest {questName} not met - not completing it");
                return;
            }

            match.Status = QuestStatus.Completed;
            Debug.Log("Quest " + questName + " completed");
        }

        public IReadOnlyList<QuestState> GetCompletableQuests()
        {
            return _questStates.Where(x => x.Status == QuestStatus.Started && x.Quest.IsMet()).ToList();
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

    public interface IQuest
    {
        string GetId();
        void OnQuestStart();
        public bool IsHidden();
        public string GetDescription();
        bool IsMet();
    }
}