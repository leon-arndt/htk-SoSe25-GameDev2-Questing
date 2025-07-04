using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Logic;
using UnityEngine;
using UserInterface;

public class QuestsState : SaveState
{
    /// <summary>
    /// Quests which are started or completed
    /// </summary>
    private Dictionary<string, QuestState> _activeQuests = new();

    /// <summary>
    /// this is all possible quests the player can start in the game.
    /// IMPORTANT: They must be found in the Resources folder 
    /// </summary>
    private List<Quest> _allPossibleQuests = new();

    public override void OnStartGame()
    {
        _activeQuests = new();
        _allPossibleQuests = Resources.LoadAll<Quest>("Quests").ToList();
    }

    public override void OnEndGame()
    {
    }

    public void StartQuest(string questId)
    {
        if (_activeQuests.ContainsKey(questId))
        {
            Debug.LogWarning($"Quest {questId} already started - not starting it again");
            return;
        }

        Quest foundQuest =
            _allPossibleQuests.FirstOrDefault(q => q.GetId().Equals(questId, StringComparison.OrdinalIgnoreCase));
        if (foundQuest == null)
        {
            Debug.LogError($"Quest {questId} not found");
            return;
        }

        var state = new QuestState()
        {
            Quest = foundQuest,
            Status = QuestStatus.Started,
        };

        _activeQuests.Add(questId, state);
        Debug.Log("Quest " + foundQuest.GetId() + " started");
        
        ActiveQuestsView.AddQuest(foundQuest);
    }

    public void CompleteQuest(string questId)
    {
        if (!_activeQuests.TryGetValue(questId, out QuestState questState))
        {
            Debug.LogWarning($"Quest {questId} not found - cannot complete it");
            return;
        }

        if (questState.Status == QuestStatus.Completed)
        {
            Debug.LogWarning($"Quest {questId} already completed - not completing it again");
            return;
        }

        if (!questState.Quest.AreConditionsMet())
        {
            Debug.LogWarning($"Quest {questId} conditions not met - not completing it");
            return;
        }

        questState.Status = QuestStatus.Completed;
        Debug.Log("Quest " + questId + " completed");
    }

    public IReadOnlyList<QuestState> GetAllActiveQuests()
    {
        // get all quests which are started or completed
        return _activeQuests.Values.ToList();
    }

    public IReadOnlyList<QuestState> GetAllStartedQuests()
    {
        // get all quests which are started but not completed
        return _activeQuests.Values.Where(q => q.Status == QuestStatus.Started).ToList();
    }

    public IReadOnlyList<QuestState> GetAllCompletedQuests()
    {
        // get all quests which were completed
        return _activeQuests.Values.Where(q => q.Status == QuestStatus.Completed).ToList();
    }
    
    public IReadOnlyList<QuestState> GetAllCompletableQuests()
    {
        return _activeQuests.Values.Where(q => q.Status == QuestStatus.Started && q.Quest.AreConditionsMet()).ToList();
    }

    public class QuestState
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